using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
public class pathfinder : MonoBehaviour
{
    GridNav grid;
    public List<PathfindingNode> pointstowalk;
    public bool sneakymode = false;
    void Awake()
    {
        grid = GetComponent<GridNav>();
    }
    static int FindDif(PathfindingNode a, PathfindingNode b)
    {
        int ydif = a.ypos - b.ypos;
        if (ydif < 0)
        {
            ydif = -ydif;
        }
        int xdif = a.xpos - b.xpos;
        if (xdif < 0)
        {
            xdif = -xdif;
        }
        float Fanswer = Mathf.Sqrt((xdif * xdif) + (ydif * ydif));
        return (Mathf.FloorToInt(10 * Fanswer));
    }
    public List<PathfindingNode> FindPath(Vector3 start, Vector3 end, PathfindingNode[,] PathGrid)
    {
        
        Stopwatch tchck = new Stopwatch();
        tchck.Start();
        minHeap Openlist = new minHeap();
        HashSet<PathfindingNode> ClosedList = new HashSet<PathfindingNode>();
        PathfindingNode firstnode = grid.nodefromworldpoint(start,PathGrid);
        PathfindingNode endnode = grid.nodefromworldpoint(end,PathGrid);
        if(endnode.passable == false)
        {
           foreach(PathfindingNode alt in endnode.neighbours)
            {
                if(alt.passable == true)
                {
                    endnode = alt;
                }
            }
        }
        firstnode.parent = null;
        Openlist.Insert(firstnode);
        while (Openlist.listMax >= 0)
        {

            PathfindingNode currentnode = Openlist.Takemin();
            ClosedList.Add(currentnode);
            if (currentnode.xpos == endnode.xpos && currentnode.ypos == endnode.ypos)
            {
                List<PathfindingNode> foundpath = new List<PathfindingNode>();
                PathfindingNode x = currentnode;
                while (x.parent != null)
                {
                    foundpath.Add(x);
                    x = x.parent;
                }
                foundpath.Add(firstnode);
                foundpath.Reverse();
                tchck.Stop();
                List<PathfindingNode> waypoints = simplifypath(foundpath);
                ClosedList = null;
                Openlist = null;
                return waypoints;
            }
            else
            {
                foreach (PathfindingNode posbeingchecked in currentnode.neighbours)
                {
                    bool alreadyinopen = false;

                    if (posbeingchecked.passable == false || ClosedList.Contains(posbeingchecked)) // skips if neighbour is inpassable
                    {
                        continue;
                    }
                    for (int isino = 0; isino <= Openlist.listMax; isino++)
                    {

                        if (Openlist.heap[isino].xpos == posbeingchecked.xpos && Openlist.heap[isino].ypos == posbeingchecked.ypos) //checks if item is already in heap
                        {

                            alreadyinopen = true;
                            int ngcost = currentnode.Gcost + FindDif(posbeingchecked, currentnode);
                            if (Openlist.heap[isino].Gcost > ngcost)
                            {

                                Openlist.heap[isino].Gcost = ngcost;
                                Openlist.heap[isino].parent = currentnode;
                                Openlist.UpShift(isino); // update node and mantain heap


                            }
                            break;
                        }
                    }
                    if (alreadyinopen == false)
                    {

                        posbeingchecked.parent = currentnode;
                        posbeingchecked.Gcost = currentnode.Gcost + FindDif(currentnode, posbeingchecked);
                        posbeingchecked.Hcost = FindDif(posbeingchecked, endnode);
                        if (sneakymode == true)
                        {
                            posbeingchecked.Hcost += posbeingchecked.Weight;
                        }
                        Openlist.Insert(posbeingchecked);      // add to open
                    }
                }
            }

        }
        ClosedList = null;
        Openlist = null;
        List<PathfindingNode> empty = new List<PathfindingNode>();
        return empty;
        
    }

    List<PathfindingNode> simplifypath(List<PathfindingNode> path)
    {
        List<PathfindingNode> waypoints = new List<PathfindingNode>();
        Vector2 lastdirection = new Vector2(0, 0);
        for (int n = 1; n < path.Count; n++)
        {
            Vector2 newdirection = new Vector2(path[n - 1].xpos - path[n].xpos, path[n - 1].ypos - path[n].ypos);
            if (lastdirection != newdirection)
            {
                waypoints.Add(path[n - 1]);
            }
            lastdirection = newdirection;
        }
        waypoints.Add(path[path.Count - 1]);
        return waypoints;
    }

}


