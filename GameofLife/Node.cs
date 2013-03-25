using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace GameofLife
{
    class Node
    {
        private Node next;
        public Node Next { get { return next; } }

        private readonly Node topLeft;
        private readonly Node topRight;
        private readonly Node bottomLeft;
        private readonly Node bottomRight;
        private Node result;

        //private bool disableSuperSpeed = false;
        //public bool DisableSuperSpeed
        //{
        //    get { return disableSuperSpeed; }
        //    set { disableSuperSpeed = value; }
        //}

        private readonly int hashValue;
        public int HashValue { get { return hashValue; } }

        private readonly int level;
        public int Level  { get { return level; } }

        private readonly bool childValue;

        public Node(Node topLeft, Node topRight, Node bottomLeft, Node bottomRight, int level)
        {
            Debug.Assert(level > 0, "Cannot create a child node with children.");

            this.level = level;

            this.topLeft = NodeTable.Find(topLeft);
            this.topRight = NodeTable.Find(topRight);
            this.bottomLeft = NodeTable.Find(bottomLeft);
            this.bottomRight = NodeTable.Find(bottomRight);

            this.hashValue = GetHashCode();
        }

        // Level 0 constructor.
        public Node(bool childValue)
        {
            this.level = 0;
            this.childValue = childValue;

            this.topLeft = null;
            this.topRight = null;
            this.bottomLeft = null;
            this.bottomRight = null;

            this.hashValue = GetHashCode();
        }

        public Node Expand()
        {
            Node emptyTree = NodeTable.Find(CreateEmptyTree(level - 1));

            return NodeTable.Find(new Node(
                new Node(emptyTree, emptyTree, emptyTree, topLeft, level),
                new Node(emptyTree, emptyTree, topRight, emptyTree, level),
                new Node(emptyTree, bottomLeft, emptyTree, emptyTree, level),
                new Node(bottomRight, emptyTree, emptyTree, emptyTree, level), level + 1));
        }

        private Node CreateEmptyTree(int level)
        {
            if (level == 0)
            {
                return new Node(false);
            }

            Node emptyTree = NodeTable.Find(CreateEmptyTree(level - 1));

            return new Node(emptyTree, emptyTree, emptyTree, emptyTree, level);
        }

        public void Flatten(bool[,] state, int x, int y)
        {
            if (level == 0)
            {
                state[y, x] = childValue;
                return;
            }

            int offset = PowerTwos.Get(level - 1);

            topLeft.Flatten(state, x, y);
            topRight.Flatten(state, x + offset, y);
            bottomLeft.Flatten(state, x, y + offset);
            bottomRight.Flatten(state, x + offset, y + offset);
        }

        public void LinkTo(Node next)
        {
            Debug.Assert(this.next == null, "This node item is already linked.");

            this.next = next;
        }

        #region Next Generation
        // Return a new node 1 level down, centered at this node
        // and 2^(k - 2) generations forward if super speed is enabled.
        // x and y are the coordinates of state grid of this node.
        public Node NextGeneration(bool[,] state, int x, int y)
        {
            Debug.Assert(level >= 2, "Can only be called on a node at level 2 or above.");

            if (result != null)
            {
                return result;
            }

            // Check if the result is stored in the hash table.
            Node hashed = NodeTable.Lookup(this);
            if (hashed != null && hashed.result != null)
            {
                // Save the result if it is hashed.
                result = hashed.result;

                int offset = PowerTwos.Get(level - 2);

                // Recurse into the children to set the state.
                if (state != null)
                {
                    result.Flatten(state, x + offset, y + offset);
                }

                return result;
            }

            if (level == 2)
            {
                bool n00 = topLeft.topLeft.childValue;
                bool n01 = topLeft.topRight.childValue;
                bool n02 = topLeft.bottomLeft.childValue;
                bool n03 = topLeft.bottomRight.childValue;

                bool n10 = topRight.topLeft.childValue;
                bool n11 = topRight.topRight.childValue;
                bool n12 = topRight.bottomLeft.childValue;
                bool n13 = topRight.bottomRight.childValue;

                bool n20 = bottomLeft.topLeft.childValue;
                bool n21 = bottomLeft.topRight.childValue;
                bool n22 = bottomLeft.bottomLeft.childValue;
                bool n23 = bottomLeft.bottomRight.childValue;

                bool n30 = bottomRight.topLeft.childValue;
                bool n31 = bottomRight.topRight.childValue;
                bool n32 = bottomRight.bottomLeft.childValue;
                bool n33 = bottomRight.bottomRight.childValue;

                // Create four nodes at level 0, 1 generation forward.
                Node resultTopLeft = new Node(IsAlive(n03,
                    new bool[] { n00, n01, n02, n10, n12, n20, n21, n30 }));

                Node resultTopRight = new Node(IsAlive(n12,
                    new bool[] { n01, n03, n10, n11, n13, n21, n30, n31 }));

                Node resultBottomLeft = new Node(IsAlive(n21,
                    new bool[] { n02, n03, n12, n20, n22, n23, n30, n32 }));

                Node resultBottomRight = new Node(IsAlive(n30,
                    new bool[] { n03, n12, n13, n21, n23, n31, n32, n33 }));

                // Return a new node at level 1 which is the center 2 by 2 of this level 2 node
                // 1 generation forward.
                result = new Node(resultTopLeft, resultTopRight,
                    resultBottomLeft, resultBottomRight, 1);

                // Set state.
                if (state != null)
                {
                    state[y + 1, x + 1] = result.topLeft.childValue;
                    state[y + 1, x + 2] = result.topRight.childValue;

                    state[y + 2, x + 1] = result.bottomLeft.childValue;
                    state[y + 2, x + 2] = result.bottomRight.childValue;
                }

                return result;
            }

            #region Single step iteration code
            //if (disableSuperSpeed)
            //{
            //    // Create the nine sub nodes.
            //    Node s00 = topLeft.CentredSubNode();
            //    Node s01 = CenteredHorizontalSubNode(topLeft, topRight);
            //    Node s02 = topRight.CentredSubNode();

            //    Node s10 = CenteredVerticalSubNode(topLeft, bottomLeft);
            //    Node s11 = CentredSubSubNode();
            //    Node s12 = CenteredVerticalSubNode(topRight, bottomRight);

            //    Node s20 = bottomLeft.CentredSubNode();
            //    Node s21 = CenteredHorizontalSubNode(bottomLeft, bottomRight);
            //    Node s22 = bottomRight.CentredSubNode();

            //    // Return a centered sub node 1 level down, 1 generation forward.
            //    return new Node(
            //        new Node(s00, s01, s10, s11, level - 1).NextGeneration(),
            //        new Node(s01, s02, s11, s12, level - 1).NextGeneration(),
            //        new Node(s10, s11, s20, s21, level - 1).NextGeneration(),
            //        new Node(s11, s12, s21, s22, level - 1).NextGeneration(), level - 1);
            //}
            #endregion
            // Create the nine sub nodes.
            int k3 = PowerTwos.Get(level - 3);
            int k23 = (k3 << 1) + k3;

            Node t00 = topLeft.NextGeneration(null, 0, 0);
            Node t01 = CenteredHorizontalSubNodeForward(topLeft, topRight, null, 0, 0);
            Node t02 = topRight.NextGeneration(null, 0, 0);

            Node t10 = CenteredVerticalSubNodeForward(topLeft, bottomLeft, null, 0, 0);
            Node t11 = CentredSubNode().NextGeneration(null, 0, 0);
            Node t12 = CenteredVerticalSubNodeForward(topRight, bottomRight, null, 0, 0);

            Node t20 = bottomLeft.NextGeneration(null, 0, 0);
            Node t21 = CenteredHorizontalSubNodeForward(bottomLeft, bottomRight, null, 0, 0);
            Node t22 = bottomRight.NextGeneration(null, 0, 0);

            // Return a centered sub node 1 level down, 2^(level - 2) generations forward.
            result = new Node(
                new Node(t00, t01, t10, t11, level - 1).NextGeneration(state, x + k3, y + k3),
                new Node(t01, t02, t11, t12, level - 1).NextGeneration(state, x + k23, y + k3),
                new Node(t10, t11, t20, t21, level - 1).NextGeneration(state, x + k3, y + k23),
                new Node(t11, t12, t21, t22, level - 1).NextGeneration(state, x + k23, y + k23),
                level - 1);

            return result;
        }

        #region Single step iteration helper methods
        //private Node CenteredHorizontalSubNode(Node left, Node right)
        //{
        //    Debug.Assert(left.level == right.level, "Must have equal levels.");
        //    Debug.Assert(left.level > 1, "level must be greater than 1.");

        //    return new Node(left.topRight.bottomRight, right.topLeft.bottomLeft,
        //        left.bottomRight.topRight, right.bottomLeft.topLeft, left.level - 1);
        //}

        //private Node CenteredVerticalSubNode(Node top, Node bottom)
        //{
        //    Debug.Assert(top.level == bottom.level, "Must have equal levels.");
        //    Debug.Assert(top.level > 1, "level must be greater than 1.");

        //    return new Node(top.bottomLeft.bottomRight, top.bottomRight.bottomLeft,
        //        bottom.topLeft.topRight, bottom.topRight.topLeft, top.level - 1);
        //}

        //private Node CentredSubSubNode()
        //{
        //    Debug.Assert(level > 2, "level must be greater than 2.");

        //    return new Node(topLeft.bottomRight.bottomRight, topRight.bottomLeft.bottomLeft,
        //        bottomLeft.topRight.topRight, bottomRight.topLeft.topLeft, level - 2);
        //}
        #endregion

        private Node CentredSubNode()
        {
            Debug.Assert(level > 1, "Level must be greater than 1.");

            return new Node(topLeft.bottomRight, topRight.bottomLeft,
                bottomLeft.topRight, bottomRight.topLeft, level - 1);
        }

        private Node CenteredHorizontalSubNodeForward(Node left, Node right, bool[,] state, int x, int y)
        {
            Debug.Assert(left.level == right.level, "Must have equal levels.");
            Debug.Assert(left.level > 1, "level must be greater than 1.");

            return new Node(left.topRight, right.topLeft,
                left.bottomRight, right.bottomLeft, left.level).NextGeneration(state, x, y);
        }

        private Node CenteredVerticalSubNodeForward(Node top, Node bottom, bool[,] state, int x, int y)
        {
            Debug.Assert(top.level == bottom.level, "Must have equal levels.");
            Debug.Assert(top.level > 1, "Level must be greater than 1.");

            return new Node(top.bottomLeft, top.bottomRight,
                bottom.topLeft, bottom.topRight, top.level).NextGeneration(state, x, y);
        }

        private bool IsAlive(bool isAlive, bool[] neighbours)
        {
            Debug.Assert(neighbours.Length == 8, "Must have 8 neighbours.");
            Debug.Assert(level == 2, "Must be at level 2.");

            int neighbourCount = 0;
            for (int i = 0; i < neighbours.Length; i++)
            {
                if (neighbours[i])
                {
                    neighbourCount++;
                }
            }

            return neighbourCount == 3 || (isAlive && neighbourCount == 2);
        }
        #endregion

        #region Overrides
        public override string ToString()
        {
            if (level == 0)
            {
                return "Child: " + childValue;
            }

            return "Node: " + level;
        }

        public override int GetHashCode()
        {
            if (level == 0)
            {
                return childValue.GetHashCode();
            }

            return bottomLeft.hashValue + 3 * bottomRight.hashValue
                + 9 * topRight.hashValue + 27 * topLeft.hashValue;
        }

        public bool DeepEqualTo(Node a)
        {
            if (level != a.level)
            {
                return false;
            }

            if (level == 0)
            {
                return childValue == a.childValue;
            }

            return topLeft == a.topLeft && topRight == a.topRight
                && bottomLeft == a.bottomLeft && bottomRight == a.bottomRight;
        }
        #endregion
    }
}
