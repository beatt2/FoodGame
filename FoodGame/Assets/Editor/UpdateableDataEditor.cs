

using Grid;
using Tools;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(GridMaker), true)]
    public class UpdateableDataEditor :UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            GridMaker data = (GridMaker) target;
            if (GUILayout.Button("Update"))
            {
                data.OnButtonPressed();
            }
        }
    }
}
