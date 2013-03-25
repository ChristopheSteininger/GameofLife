using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace GameofLife
{
    static class NodeTable
    {
        private const int TABLE_SIZE = 10831;

        private static Node[] nodeTable = new Node[TABLE_SIZE];

        // Returns a pointer to an equivalent node in the hashtable. The node is
        // stored and returned if it is not stored in the hashtable.
        public static Node Find(Node node)
        {
            int index = node.HashValue % TABLE_SIZE;
            if (index < 0)
            {
                index = -index;
            }

            Node current = nodeTable[index];

            if (current == null)
            {
                nodeTable[index] = node;

                return node;
            }

            while (true)
            {
                // If the node is in the table, return the node.
                if (current.DeepEqualTo(node))
                {
                    return current;
                }

                // If the table does not contain the node, add it to the table
                // and return it.
                if (current.Next == null)
                {
                    current.LinkTo(node);

                    return node;
                }

                current = current.Next;
            }
        }

        public static Node Lookup(Node node)
        {
            int index = node.HashValue % TABLE_SIZE;
            if (index < 0)
            {
                index = -index;
            }

            Node current = nodeTable[index];
            if (current == null)
            {
                return null;
            }

            while (true)
            {
                // If the node is in the table, return the node.
                if (current.DeepEqualTo(node))
                {
                    return current;
                }

                if (current.Next == null)
                {
                    return null;
                }

                current = current.Next;
            }
        }

        public static int[] GetTableUse()
        {
            int[] result = new int[nodeTable.Length];
            for (int i = 0; i < nodeTable.Length; i++)
            {
                result[i] = 0;

                for (Node currentItem = nodeTable[i];
                    currentItem != null; currentItem = currentItem.Next)
                {
                    result[i]++;
                }
            }

            return result;
        }
    }
}
