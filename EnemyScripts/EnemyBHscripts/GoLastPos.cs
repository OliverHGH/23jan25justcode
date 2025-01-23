using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;

public class GoLastPos : BehaviourNode
{
    private pathfinder PathFinder;
    private Transform enemy, lastpos;
    private List<PathfindingNode> path;
    BehaviourNode rootretrive;
    bool pathexists = false;
    int n = 0;
    Vector3 positionlast,nextpos;
    public float dtime;
    PathfindingNode[,] gridpath;
    public GoLastPos(Transform e, pathfinder Path, GridNav g)
    {
        enemy = e;
        PathFinder = Path;
        gridpath = g.creategrid();
    }
    public override NodeState Evaluate()
    {
        if (pathexists == false)
        {
            rootretrive = getroot();
            if ((rootretrive.DataChecker.TryGetValue("lastseenpos", out object pos)) && pos is Vector3)
            {
                path = PathFinder.FindPath(enemy.position, (Vector3)pos,gridpath);
                pathexists = true;
                positionlast = (Vector3)pos;
                n = 0;
            }
        }
        enemy.LookAt(positionlast);
        nextpos = new Vector3(path[n].worldcoord.x, enemy.position.y, path[n].worldcoord.z);
        enemy.position = Vector3.MoveTowards(enemy.position, nextpos, 15f * dtime);
        float xdif = Mathf.Abs(enemy.position.x - nextpos.x);
        float zdif = Mathf.Abs(enemy.position.z - nextpos.z);
        if (xdif + zdif < 0.05) //checks if close to next point
        {
           n += 1;
        }
        if (xdif + zdif < 0.05 && n == (path.Count - 1))
        {
            
            pathexists = false;
            rootretrive.DataChecker["lastposvisited"] = true;
        }
        return NodeState.SUCCESS;
    }
    
}
