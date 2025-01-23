using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTree
{
    public class SequenceNode : BehaviourNode
    {
        public SequenceNode(List<BehaviourNode> list)
        {
            BNchildren = list;
            foreach (BehaviourNode x in BNchildren)
            {
                x.parent = this;
            }
        }
        public override NodeState Evaluate()
        {
            foreach(BehaviourNode x in BNchildren)
            {
                state = x.Evaluate();
                if (state == NodeState.FAILURE || state == NodeState.RUNNING)
                {
                    return state;
                }
            }
            state = NodeState.SUCCESS;
            return state;
        }


    }

}
