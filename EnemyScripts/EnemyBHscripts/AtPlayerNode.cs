﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;

public class AtPlayerNode : BehaviourNode
{
    Transform player;
    Transform enemy;
    LayerMask walls;
    public AtPlayerNode(Transform e, Transform p,LayerMask wall)
    {
        player = p;
        enemy = e;
        walls = wall;
    }
    public override NodeState Evaluate()
    {
        float xdif = Mathf.Abs(enemy.position.x - player.position.x);
        float zdif = Mathf.Abs(enemy.position.z - player.position.z);
     
        if (xdif + zdif < 2.5 && !Physics.Linecast(player.position,enemy.position,walls)) //checks if close to next point
        {
        
            return NodeState.SUCCESS;
        }
        else
        {
            return NodeState.FAILURE;
        }

    }
}
