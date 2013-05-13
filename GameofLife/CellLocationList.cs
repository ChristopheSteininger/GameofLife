using System.Diagnostics;

namespace GameofLife
{
    public class CellLocationList
    {
        private CellLocation first = null;
        public CellLocation First { get { return first; } }

        private CellLocation last = null;
        public CellLocation Last { get { return last; } }

        public void Add(CellLocation newLocation)
        {
            if (first == null)
            {
                last = newLocation;
            }

            newLocation.LinkTo(first);
            first = newLocation;
        }

        public void CopyFrom(CellLocationList list)
        {
            if (last == null)
            {
                first = list.First;
            }

            else
            {
                last.LinkTo(list.First);
            }

            last = list.Last;
        }

        public void IncrementList(int x, int y)
        {
            for (CellLocation current = first; current != null;
                current = current.Next)
            {
                current.X += x;
                current.Y += y;
            }
        }
    }

    public class CellLocationTree
    {
        private readonly int offset;
        public int Offset { get { return offset; } }

        private readonly CellLocationTree topLeft;
        private readonly CellLocationTree topRight;
        private readonly CellLocationTree bottomLeft;
        private readonly CellLocationTree bottomRight;

        public CellLocationTree TopLeft { get { return topLeft; } }
        public CellLocationTree TopRight { get { return topRight; } }
        public CellLocationTree BottomLeft { get { return bottomLeft; } }
        public CellLocationTree BottomRight { get { return bottomRight; } }

        private readonly CellLocationList born;
        private readonly CellLocationList dies;

        public CellLocationList Born { get { return born; } }
        public CellLocationList Dies { get { return dies; } }

        // Node constructor.
        public CellLocationTree(int offset, CellLocationTree topLeft,
            CellLocationTree topRight, CellLocationTree bottomLeft,
            CellLocationTree bottomRight)
        {
            this.offset = offset;

            this.topLeft = topLeft;
            this.topRight = topRight;
            this.bottomLeft = bottomLeft;
            this.bottomRight = bottomRight;

            this.born = null;
            this.dies = null;
        }

        // Leaf constructor.
        public CellLocationTree(CellLocationList born, CellLocationList dies)
        {
            this.offset = 0;

            this.topLeft = null;
            this.topRight = null;
            this.bottomLeft = null;
            this.bottomRight = null;

            this.born = born;
            this.dies = dies;
        }
    }

    public class CellLocation
    {
        private int x;
        public int X
        {
            get { return x; }
            set { x = value; }
        }

        private int y;
        public int Y
        {
            get { return y; }
            set { y = value; }
        }

        private CellLocation next;
        public CellLocation Next { get {return next; } }

        public CellLocation(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public void LinkTo(CellLocation next)
        {
            Debug.Assert(this.next == null, "Can only link if next is null.");

            this.next = next;
        }
    }
}
