
using System.Collections.Generic;
using System.Linq;

public class minHeap
{
    public List<PathfindingNode> heap = Enumerable.Repeat(default(PathfindingNode), 2000).ToList();
    protected int IndexCount = -1;
    public int listMax
    {
        get { return IndexCount; }
    }
    void swap(int x, int y)
    {
        PathfindingNode temp = heap[x];
        heap[x] = heap[y];
        heap[y] = temp;

    }
    public void UpShift(int n)
    {
        while (n > 0 && (heap[n].Fcost < heap[(n - 1) / 2].Fcost || (heap[n].Fcost== heap[(n - 1) / 2].Fcost && heap[n].Hcost < heap[(n - 1) / 2].Hcost)))
        {
            swap(n, (n - 1) / 2);
            n = (n - 1) / 2;
        }
    }
    public void Insert(PathfindingNode n)
    {
        IndexCount += 1;
        heap[IndexCount] = n;
        UpShift(IndexCount);

    }
    void DownShift(int n)
    {
        int M = n;
        int left = (n * 2) + 1;
        int right = (n * 2) + 2;
        if (left <= IndexCount && heap[left].Fcost < heap[M].Fcost)
        {
            M = left;
        }
        if (right <= IndexCount && heap[right].Fcost < heap[M].Fcost)
        {
            M = right;
        }
        if (n != M)
        {
            swap(n, M);
            DownShift(M);
        }
    }
    public void Remove(int n)
    {
        heap[n] = heap[IndexCount];
        heap[IndexCount] = null;
        IndexCount -= 1;
        DownShift(n);
    }
    public PathfindingNode Takemin()
    {
        PathfindingNode min = heap[0];
        heap[0] = heap[IndexCount];
        heap[IndexCount] = null;
        IndexCount -= 1;
        DownShift(0);
        return min;
    }
}
