using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;
using System.IO;
public class NewPathToPlayer : BehaviourNode
{
    pathfinder finder;
    Transform player, enemy;
    List<PathfindingNode> pathplayer= null;
    public float timer;
    public float dtime;
    int Lpos =0;
    Vector3 nextpos;
    PathfindingNode[,] PathGrid;
    public bool besneaky = false;
    Transform sidepos;
    GridNav gclass;
    List<PathfindingNode>weightednodes = new List<PathfindingNode>();
  public NewPathToPlayer(pathfinder find,Transform e, Transform p, GridNav g,Transform side)
    {
        enemy = e;
        player = p;
        finder = find;
        PathGrid = g.creategrid();
        gclass = g;
        sidepos = side;
        
    }

    public override NodeState Evaluate()
    {
        BehaviourNode p = getroot();

        foreach (PathfindingNode x in weightednodes)
        {
            x.Weight = 0; //resets weight of pathfinding nodes
        }
        weightednodes = gclass.squaremaker(gclass.nodefromworldpoint(player.position + player.forward*5, PathGrid), PathGrid);//gets nodes in front of player
        foreach (PathfindingNode x in weightednodes)
        {
            x.Weight = 50;
        }//weights the node so path sneaks up


        if (timer > .25 || pathplayer == null)
        {
            p = getroot();
            timer = 0;
            if (besneaky)
            {
                for (float i = 0; i < 10; i++)
                {
                    if (gclass.nodefromworldpoint(player.position - player.right * i, PathGrid) != null)
                    {
                        sidepos.position = player.position - player.right * i;
                    }
                }
                Debug.Log("timetobesneaky");
                pathplayer = finder.FindPath(enemy.position, sidepos.position, PathGrid);
            }
            else
            {
                pathplayer = finder.FindPath(enemy.position, player.position, PathGrid);
            }
            Lpos = 0;
        }
        if (besneaky)
        {
            Debug.Log("sneak");
        }
        enemy.LookAt(player.position);
        nextpos = new Vector3(pathplayer[Lpos].worldcoord.x, enemy.position.y, pathplayer[Lpos].worldcoord.z);
        enemy.position = Vector3.MoveTowards(enemy.position, nextpos, 30f * dtime);
        float xdif = Mathf.Abs(enemy.position.x - nextpos.x);
        float zdif = Mathf.Abs(enemy.position.z - nextpos.z);
        if (xdif + zdif < 0.05) //checks if close to next point
        {
            Lpos += 1;
        }
        if (xdif + zdif < 0.05 && Lpos == (pathplayer.Count - 1))
        {
            pathplayer = null;
        }
        p.DataChecker["lastposvisited"] = false;
        return NodeState.SUCCESS;
    }
}
