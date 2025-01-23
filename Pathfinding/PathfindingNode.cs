using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathfindingNode
{

    public bool passable;
    public bool inopen = false;
    public Vector3 worldcoord;
    public int xpos;
    public int ypos;
    public int Gcost;
    public int Hcost;
    public int Weight, HeapPos;
    public List<PathfindingNode> neighbours;
    public int Fcost { get { return Hcost + Gcost; } }
    public PathfindingNode parent = null;
    public  PathfindingNode(bool _passable, Vector3 _worldcoord, int _xpos,int _ypos)
    {
        passable = _passable;
        worldcoord = _worldcoord;
        xpos = _xpos;
        ypos = _ypos;
    }
   
}
