using System.Collections.Generic;
using MathExt;
using Node;
using Tools;
using UnityEngine;

namespace Grid
{
    public class GridManager : Singleton<GridManager>
    {
        private List<NodeBehaviour> _nodeBehaviours = new List<NodeBehaviour>();

        private NodeBehaviour _selectedNode;

        private bool sort = false;
        
        
        public void AddNode(NodeBehaviour node)
        {
            _nodeBehaviours.Add(node);
        }

        public NodeBehaviour GetSelectedNode()
        {
            return _selectedNode != null ? _selectedNode : null;
        }
        
       

        public void SetSelectedNode(NodeBehaviour nodeBehaviour)
        {
            if (_selectedNode == null)
            {
                _selectedNode = nodeBehaviour;
                _selectedNode.HighLight.ChangeColor();
            }
            else
            {
                _selectedNode.HighLight.ChangeColor();
                _selectedNode = nodeBehaviour;
                _selectedNode.HighLight .ChangeColor();
            }
        }

        
    }
}


