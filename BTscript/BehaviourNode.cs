using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTree
{
    public enum NodeState
    {
        RUNNING,
        FAILURE,
        SUCCESS
    }


    public abstract class BehaviourNode
    {
        public BehaviourNode parent = null;
        protected NodeState state;
        public Dictionary<string,object> DataChecker = new Dictionary<string,object>();
        public BehaviourNode getroot()
        {
            BehaviourNode r;
            r = this;
            while (r.parent != null)
            {
                r = r.parent;
            }
            return r;
        }

        public List<BehaviourNode> BNchildren;
        public virtual NodeState Evaluate()
        {
            return NodeState.FAILURE;
        }

    }


}