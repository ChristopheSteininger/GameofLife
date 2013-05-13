using System.Diagnostics;
using System.Collections.Generic;

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

        private readonly int population;
        public int Population { get { return population; } }

        private readonly int hashValue;
        public int HashValue { get { return hashValue; } }

        private readonly int level;
        public int Level  { get { return level; } }

        private readonly bool childValue;

        private CellLocationTree tree;
        public CellLocationTree Tree { get { return tree; } }

        private readonly CellLocationList born;
        private readonly CellLocationList dies;

        public Node(Node topLeft, Node topRight, Node bottomLeft, Node bottomRight, int level)
        {
            Debug.Assert(level > 0, "Cannot create a child node with children.");

            this.level = level;

            this.topLeft = NodeTable.Find(topLeft);
            this.topRight = NodeTable.Find(topRight);
            this.bottomLeft = NodeTable.Find(bottomLeft);
            this.bottomRight = NodeTable.Find(bottomRight);

            this.population = topLeft.population + topRight.population
                + bottomLeft.population + bottomRight.population;

            this.hashValue = GetHashCode();

            if (level == 2)
            {
                this.born = new CellLocationList();
                this.dies = new CellLocationList();
            }

            else
            {
                this.born = null;
                this.dies = null;
            }
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

            this.population = childValue ? 1 : 0;

            this.hashValue = GetHashCode();

            this.born = null;
            this.dies = null;
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
        public Node NextGeneration(int x, int y)
        {
            Debug.Assert(level >= 2, "Can only be called on a node at level 2 or above.");

            // TODO: Check if this is really needed. Should be hashed by its parent.
            if (result == null)
            {
                // Check if the result is stored in the hash table.
                Node hashed = NodeTable.Lookup(this);
                if (hashed != null && hashed.result != null)
                {
                    result = hashed.result;
                    tree = hashed.tree;
                }
            }

            if (result != null)
            {
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
                // TODO: The born and dies list could be set in the IsAlive method.
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

                bool[] centerNodes = new bool[] { n03, n12, n21, n30 };
                bool[] resultNodes = new bool[] { resultTopLeft.childValue, resultTopRight.childValue,
                        resultBottomLeft.childValue, resultBottomRight.childValue };

                // Set born list.
                if (born.First == null)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        // If the cell was born, add to born list.
                        if (!centerNodes[i] && resultNodes[i])
                        {
                            born.Add(new CellLocation(i % 2, i / 2));
                        }
                    }
                }

                // Set dies list.
                if (dies.First == null)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        // If the cell died, add to dies list.
                        if (centerNodes[i] && !resultNodes[i])
                        {
                            dies.Add(new CellLocation(i % 2, i / 2));
                        }
                    }
                }

                if (born.First != null || dies.First != null)
                {
                }

                tree = new CellLocationTree(born, dies);

                return result;
            }

            int k3 = PowerTwos.Get(level - 3);
            int k23 = (k3 << 1) + k3;

            // Create the nine sub nodes.
            // Use the hash table?
            Node t00 = topLeft.NextGeneration(0, 0);
            Node t01 = CenteredHorizontalSubNodeForward(topLeft, topRight, 0, 0);
            Node t02 = topRight.NextGeneration(0, 0);

            Node t10 = CenteredVerticalSubNodeForward(topLeft, bottomLeft, 0, 0);
            Node t11 = CentredSubNodeForward(0, 0);
            Node t12 = CenteredVerticalSubNodeForward(topRight, bottomRight, 0, 0);

            Node t20 = bottomLeft.NextGeneration(0, 0);
            Node t21 = CenteredHorizontalSubNodeForward(bottomLeft, bottomRight, 0, 0);
            Node t22 = bottomRight.NextGeneration(0, 0);

            Node r00 = new Node(t00, t01, t10, t11, level - 1);
            Node r01 = new Node(t01, t02, t11, t12, level - 1);
            Node r10 = new Node(t10, t11, t20, t21, level - 1);
            Node r11 = new Node(t11, t12, t21, t22, level - 1);

            // Return a centered sub node 1 level down, 2^(level - 2) generations forward.
            result = new Node(r00.NextGeneration(x + k3, y + k3), r01.NextGeneration(x + k23, y + k3),
                r10.NextGeneration(x + k3, y + k23), r11.NextGeneration(x + k23, y + k23), level - 1);

            tree = new CellLocationTree(k3 << 1, r00.tree, r01.tree, r10.tree, r11.tree);

            return result;
        }

        private Node CentredSubNodeForward(int x, int y)
        {
            Debug.Assert(level > 1, "Level must be greater than 1.");

            return new Node(topLeft.bottomRight, topRight.bottomLeft,
                bottomLeft.topRight, bottomRight.topLeft, level - 1).NextGeneration(x, y);
        }

        private Node CenteredHorizontalSubNodeForward(Node left, Node right, int x, int y)
        {
            Debug.Assert(left.level == right.level, "Must have equal levels.");
            Debug.Assert(left.level > 1, "level must be greater than 1.");

            return new Node(left.topRight, right.topLeft,
                left.bottomRight, right.bottomLeft, left.level).NextGeneration(x, y);
        }

        private Node CenteredVerticalSubNodeForward(Node top, Node bottom, int x, int y)
        {
            Debug.Assert(top.level == bottom.level, "Must have equal levels.");
            Debug.Assert(top.level > 1, "Level must be greater than 1.");

            return new Node(top.bottomLeft, top.bottomRight,
                bottom.topLeft, bottom.topRight, top.level).NextGeneration(x, y);
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

                if (neighbourCount > 3)
                {
                    return false;
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

            return (bottomLeft.hashValue + 1) + 3 * (bottomRight.hashValue + 1)
                + 9 * (topRight.hashValue + 1) + 27 * (topLeft.hashValue + 1);
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
