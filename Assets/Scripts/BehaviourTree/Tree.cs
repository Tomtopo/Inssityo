using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace BehaviourTree
{
    public abstract class Tree : MonoBehaviour
    {
        private Node _root;

        protected void Start()
        {
            _root = SetupTree();
        }

        private void Update()
        {
            if (_root != null) 
                _root.Evaluate();
        }

        protected abstract Node SetupTree();

        public Node GetRoot()
        {
            return _root;
        }

        //private void OnGUI()
        //{
        //    GUI.Label(new Rect(10, 40, 400, 20), "Current state: " + _root.);
        //}
    }


}

