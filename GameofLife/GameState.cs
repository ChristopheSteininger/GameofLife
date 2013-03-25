using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace GameofLife
{
    public class GameState
    {
        private Node root;

        private bool[,] state;
        public bool[,] State { get { return state; } }
        public bool this[int y, int x]
        {
            get { return state[y, x]; }
            set { state[y, x] = value; }
        }

        private int generation = 0;
        public int Generation { get { return generation; } }

        public GameState()
        {
            CreateGosperGliderGun();
            //CreateGlider();
        }

        private void CreateEmptyGrid()
        {
            state = new bool[16, 16];
            int top = 6;
            int left = 6;

            state[top, left] = true;
            state[top + 1, left] = true;
            state[top, left + 1] = true;
            state[top + 1, left + 1] = false;

            root = CreateNode(state);
            generation = 0;
        }

        private void CreateGosperGliderGun()
        {
            state = new bool[128, 128];

            int top = 30;
            int left = 30;

            state[top + 4, left + 0] = true;
            state[top + 4, left + 1] = true;
            state[top + 5, left + 0] = true;
            state[top + 5, left + 1] = true;

            state[top + 2, left + 12] = true;
            state[top + 2, left + 13] = true;
            state[top + 3, left + 11] = true;
            state[top + 4, left + 10] = true;
            state[top + 5, left + 10] = true;
            state[top + 6, left + 10] = true;
            state[top + 7, left + 11] = true;
            state[top + 8, left + 12] = true;
            state[top + 8, left + 13] = true;

            state[top + 5, left + 14] = true;
            state[top + 3, left + 15] = true;
            state[top + 4, left + 16] = true;
            state[top + 5, left + 16] = true;
            state[top + 5, left + 17] = true;
            state[top + 6, left + 16] = true;
            state[top + 7, left + 15] = true;

            state[top + 0, left + 24] = true;
            state[top + 1, left + 24] = true;
            state[top + 1, left + 22] = true;
            state[top + 2, left + 20] = true;
            state[top + 2, left + 21] = true;
            state[top + 3, left + 20] = true;
            state[top + 3, left + 21] = true;
            state[top + 4, left + 20] = true;
            state[top + 4, left + 21] = true;
            state[top + 5, left + 22] = true;
            state[top + 5, left + 24] = true;
            state[top + 6, left + 24] = true;

            state[top + 2, left + 34] = true;
            state[top + 2, left + 35] = true;
            state[top + 3, left + 34] = true;
            state[top + 3, left + 35] = true;

            root = CreateNode(state);
            generation = 0;
        }

        private void CreateSwitchEngine()
        {
            state = new bool[256, 256];

            int top = 130;
            int left = 130;
            state[top + 5, left] = true;
            state[top + 5, left + 2] = true;
            state[top + 4, left + 2] = true;
            state[top + 1, left + 4] = true;
            state[top + 2, left + 4] = true;
            state[top + 3, left + 4] = true;
            state[top, left + 6] = true;
            state[top + 1, left + 6] = true;
            state[top + 2, left + 6] = true;
            state[top + 1, left + 7] = true;

            root = CreateNode(state);
            generation = 0;
        }

        private void CreateGlider()
        {
            int l = 7;
            int size = PowerTwos.Get(l);

            state = new bool[size, size];

            int top = 1;
            int left = 16;
            state[top + 0, left + 1] = true;
            state[top + 1, left + 2] = true;
            state[top + 2, left + 0] = true;
            state[top + 2, left + 1] = true;
            state[top + 2, left + 2] = true;

            top = 1;
            left = 1;
            state[top + 0, left + 1] = true;
            state[top + 1, left + 2] = true;
            state[top + 2, left + 0] = true;
            state[top + 2, left + 1] = true;
            state[top + 2, left + 2] = true;

            top = 1;
            left = 10;
            state[top + 0, left + 1] = true;
            state[top + 1, left + 2] = true;
            state[top + 2, left + 0] = true;
            state[top + 2, left + 1] = true;
            state[top + 2, left + 2] = true;

            root = CreateNode(state);
            generation = 0;
        }

        public void Iterate()
        {
            int start = -state.GetLength(0) / 2;
            root = root.NextGeneration(state, start, start).Expand();

            generation += PowerTwos.Get(root.Level - 2);
        }

        private Node CreateNode(bool[,] array)
        {
            Debug.Assert(array.GetLength(0) == array.GetLength(1),
                "Input array must be square.");
            // TODO: Assert for power of 2 array length.

            int level = (int)Math.Log(array.GetLength(0), 2) + 1;
            int start = -array.GetLength(0) / 2;

            return CreateNodeHelper(array, start, start, level);
        }

        private Node CreateNodeHelper(bool[,] array, int x, int y, int level)
        {
            if (level == 0)
            {
                if (0 <= x && x < array.GetLength(0) && 0 <= y && y < array.GetLength(0))
                {
                    return new Node(array[y, x]);
                }

                return new Node(false);
            }

            int offset = PowerTwos.Get(level - 1);

            return new Node(
                CreateNodeHelper(array, x, y, level - 1),
                CreateNodeHelper(array, x + offset, y, level - 1),
                CreateNodeHelper(array, x, y + offset, level - 1),
                CreateNodeHelper(array, x + offset, y + offset, level - 1), level);
        }
    }
}
