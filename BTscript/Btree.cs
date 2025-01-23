using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTree
{
    public abstract class Btree : MonoBehaviour 
    {
        protected BehaviourNode root;

        public void Start()
        {
            root = CreateTree(); 
        }
        public void Update()
        {
            root.Evaluate();
        }

        public abstract BehaviourNode CreateTree();
      


    }
}
