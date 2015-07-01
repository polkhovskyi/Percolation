using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Percolation
{
    class Percolation
    {
        private int row = 0;
        private bool[] _openFlags;
        private WeightedQuickUnion _topConnections;
        private WeightedQuickUnion _bottomConnections;
        private int _numberOfTopVirtualNode = 1;
        private int _numberOfBottomVirtualNode = 1;

        public Percolation(int n)
        {
            row = n;
            var lengthOfMatrix = GetMatrixLength(n, n);
            _openFlags = Enumerable.Repeat(false, lengthOfMatrix).ToArray();
            _topConnections = new WeightedQuickUnion(n * n + _numberOfTopVirtualNode + _numberOfBottomVirtualNode);
            _bottomConnections = new WeightedQuickUnion(n * n + _numberOfTopVirtualNode);
            UnionVirtualTopToTopRow();
            UnionVirtualBottomToBottomRow();
        }

        public bool IsOpen(int i, int j)
        {
            CheckBound(i);
            CheckBound(j);
            var current = (i - 1) * row + j - 1;
            return _openFlags[current];
        }

        public bool IsFull(int i, int j)
        {
            CheckBound(i);
            CheckBound(j);
            var current = (i - 1) * row + j - 1;
            return _bottomConnections.AreConnected(current, row * row) && IsOpen(i, j);
        }

        public bool CheckIfPercolates()
        {
            if (row == 1)
            {
                return IsOpen(1, 1);
            }

            return _topConnections.AreConnected(row * row + 1, row * row);
        }

        public void OpenByIndex(int i, int j)
        {
            CheckBound(i);
            CheckBound(j);
            var current = (i - 1) * row + j - 1;
            var left = (i - 1) * row + j - 2;
            var right = (i - 1) * row + j;
            var up = (i - 1) * row + j - 1 - row;
            var down = (i - 1) * row + j - 1 + row;
            _openFlags[current] = true;
            if (row == 1)
            {
                _topConnections.Union(current, row * row);
                _bottomConnections.Union(current, row * row);
            }
            else
            {
                if ((j - 1) > 0 && IsOpen(i, j - 1))
                {
                    _topConnections.Union(current, left);
                    _bottomConnections.Union(current, left);
                }

                if ((j + 1) <= row && IsOpen(i, j + 1))
                {
                    _topConnections.Union(current, right);
                    _bottomConnections.Union(current, right);
                }

                if ((i + 1) <= row && IsOpen(i + 1, j))
                {
                    _topConnections.Union(current, down);
                    _bottomConnections.Union(current, down);
                }

                if ((i - 1) > 0 && IsOpen(i - 1, j))
                {
                    _topConnections.Union(current, up);
                    _bottomConnections.Union(current, up);
                }
            }
        }

        private void UnionVirtualTopToTopRow()
        {
            for (var i = 0; i < row; i++)
            {
                _topConnections.Union(row * row, i);
                _bottomConnections.Union(row * row, i);
            }
        }

        private void UnionVirtualBottomToBottomRow()
        {
            for (var i = row * (row - 1); i < row * row; i++)
            {
                _topConnections.Union(row * row + 1, i);
            }
        }

        private int GetMatrixLength(int x, int y)
        {
            CheckBound(x);
            CheckBound(y);
            return x * y;
        }

        private void CheckBound(int i)
        {
            if (i <= 0 || i > row)
            {
                throw new IndexOutOfRangeException("row index is out of bounds");
            }
        }
    }
}
