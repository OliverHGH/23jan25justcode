using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;

public class SpinToLook : BehaviourNode
{
    private Transform enemy;
    private Vector3 rotate = new Vector3(0, 1, 0);
    public float dtime;
    private BehaviourNode p;
    
    public float timer;
    public SpinToLook(Transform e)
    {
        enemy = e;
        timer = 0;
    }

    public override NodeState Evaluate()
    {
        p = getroot();
        timer += dtime;
        if (timer > 2)
        {
            p.DataChecker["NeedToSpin"] = false;
            timer = 0;
            return NodeState.SUCCESS;
        }
        enemy.Rotate(rotate * dtime*180);
        return NodeState.SUCCESS;
    }

}
