using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;

public class PatrolScript : BehaviourNode
{
    pathfinder finder;
    GridNav grid;
    Transform enemy;
    List<PathfindingNode> pathpatrol;
    public float timer;
    public float dtime;
    int Lpos = 0;
    Vector3 nextpos;
    PathfindingNode[,] pathgrid;
    public PatrolScript(pathfinder find,GridNav g, Transform e)
    {
        enemy = e;
        finder = find;
        grid = g;
        pathgrid = g.creategrid();
    }
    public override NodeState Evaluate()
    {
        BehaviourNode p = getroot();
        
        if (pathpatrol == null)
        {
   
            Lpos = 0;
            int x = Random.Range(-49, 50);
            int z = Random.Range(-49, 50);
            Vector3 pos = new Vector3(x, 0, z);
            while (grid.nodefromworldpoint(pos,pathgrid).passable == false)
            {
                x = Random.Range(-49, 50);
                z = Random.Range(-49, 50);
                pos = new Vector3(x, 0, z);

            }
           
            pathpatrol = finder.FindPath(enemy.position,pos,pathgrid);
        }
       
        nextpos = new Vector3(pathpatrol[Lpos].worldcoord.x, enemy.position.y, pathpatrol[Lpos].worldcoord.z);
        enemy.LookAt(nextpos);
        enemy.position = Vector3.MoveTowards(enemy.position, nextpos, 6f * dtime);
        float xdif = Mathf.Abs(enemy.position.x - nextpos.x);
        float zdif = Mathf.Abs(enemy.position.z - nextpos.z);
        if (xdif + zdif < 0.05) //checks if close to next point
        {
            Lpos += 1;
        }
        if (xdif + zdif < 0.05 && Lpos == (pathpatrol.Count - 1))
        {
            pathpatrol = null;
        }
        return NodeState.SUCCESS;
    }
}
