using System;

namespace Percolation
{
    public class WeightedQuickUnion
    {
        private readonly int[] _parent;
        private readonly int[] _size;
        private int _count;
        public int Count { get { return _count; } }

        public WeightedQuickUnion(int numberOfElements)
        {
            _count = numberOfElements;
            _parent = new int[numberOfElements];
            _size = new int[numberOfElements];
            for (int i = 0; i < numberOfElements; i++)
            {
                _parent[i] = i;
                _size[i] = 1;
            }
        }

        public int Find(int p)
        {
            Validate(p);
            while (p != _parent[p])
                p = _parent[p];
            return p;
        }

        private void Validate(int p)
        {
            int length = _parent.Length;
            if (p < 0 || p >= length)
            {
                throw new IndexOutOfRangeException("index " + p + " is not between 0 and " + length);
            }
        }

        public bool AreConnected(int p, int q)
        {
            return Find(p) == Find(q);
        }

        public void Union(int p, int q)
        {
            int rootP = Find(p);
            int rootQ = Find(q);
            if (rootP == rootQ) return;

            if (_size[rootP] < _size[rootQ])
            {
                _parent[rootP] = rootQ;
                _size[rootQ] += _size[rootP];
            }
            else
            {
                _parent[rootQ] = rootP;
                _size[rootP] += _size[rootQ];
            }

            _count--;
        }
    }
}
