using System;
using System.Collections.Generic;


namespace cwu.cs.TaAssignments
{
    class DijkstraHeap
    {
        // - - - - Private Variables - - - -

        // Array storing heap.
        private int[] verIds = null;

        // Values associated with vertices.
        private Distance[] values = null;

        // Index in heap of vertices.
        private int[] indices = null;

        private int size = 0;


        // - - - - Constructors - - - -

        public DijkstraHeap(int capacity)
        {
            verIds = new int[capacity];
            values = new Distance[capacity];
            indices = new int[capacity];

            for (int vId = 0; vId < capacity; vId++)
            {
                indices[vId] = -1;
            }
        }

        public DijkstraHeap(Distance[] vals)
        {
            int capacity = vals.Length;

            size = capacity;
            values = vals;
            verIds = new int[capacity];
            indices = new int[capacity];

            for (int vId = 0; vId < capacity; vId++)
            {
                verIds[vId] = vId;
                indices[vId] = vId;
            }

            // Build heap
            for (int i = size / 2 - 1; i >= 0; i--)
            {
                heapify(i);
            }
        }

        // - - - - Getter Functions - - - -

        public int getCapacity()
        {
            return verIds.Length;
        }

        public int getSize()
        {
            return size;
        }

        public Distance[] getValues()
        {
            return values;
        }

        // - - - - Public Heap-Operations - - - -

        public void add(int vId, Distance vVal)
        {
            if (indices[vId] >= 0)
            {
                throw new ArgumentException("Vertex is already in heap.");
            }

            if (getSize() >= getCapacity())
            {
                throw new InvalidOperationException("Heap is already full.");
            }

            verIds[size] = vId;
            values[vId] = vVal;
            indices[vId] = size;

            size++;

            moveUp(size - 1);
        }

        public void update(int vId, Distance vVal)
        {
            int index = indices[vId];

            if (index < 0)
            {
                throw new ArgumentException("Vertex is not in heap.");
            }

            values[vId] = vVal;

            if (index == 0)
            {
                heapify(index);
                return;
            }

            int parId = verIds[parent(index)];

            Distance parVal = values[parId];
            Distance indVal = values[verIds[index]];

            if (parVal <= indVal)
            {
                heapify(index);
            }
            else
            {
                moveUp(index);
            }
        }

        public int getMinId()
        {
            return verIds[0];
        }

        public Distance getMinValue()
        {
            return values[verIds[0]];
        }

        public int removeMin()
        {
            int minId = verIds[0];

            size--;

            verIds[0] = verIds[size];

            indices[minId] = -1;
            indices[verIds[0]] = 0;

            heapify(0);

            return minId;
        }


        // - - - - Private Heap-Operations - - - -

        private void heapify(int index)
        {
            while (true)
            {
                int l = left(index);
                int r = right(index);

                if (l >= getSize())
                {
                    return;
                }

                int smallInd = l;
                Distance smallVal = values[verIds[smallInd]];

                if (r < getSize())
                {
                    Distance rVal = values[verIds[r]];

                    if (rVal < smallVal)
                    {
                        smallInd = r;
                        smallVal = rVal;
                    }
                }

                Distance indVal = values[verIds[index]];

                if (indVal <= smallVal)
                {
                    return;
                }

                swapKeys(smallInd, index);
                index = smallInd;
            }
        }

        private void moveUp(int index)
        {
            while (index > 0)
            {
                int parInd = parent(index);

                Distance parVal = values[verIds[parInd]];
                Distance indVal = values[verIds[index]];

                if (parVal <= indVal) return;

                swapKeys(parInd, index);
                index = parInd;
            }
        }


        // - - - - Helper Functions - - - -

        private int left(int index)
        {
            return 2 * index + 1;
        }

        private int right(int index)
        {
            return 2 * index + 2;
        }

        private int parent(int index)
        {
            return (index - 1) / 2;
        }

        private void swapKeys(int ind1, int ind2)
        {
            int vId1 = verIds[ind1];
            int vId2 = verIds[ind2];

            verIds[ind1] = vId2;
            verIds[ind2] = vId1;

            indices[vId1] = ind2;
            indices[vId2] = ind1;
        }
    }
}
