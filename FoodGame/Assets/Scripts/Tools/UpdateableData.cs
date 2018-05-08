using System.Collections;
using UnityEditor;
using UnityEngine;
using Debug = System.Diagnostics.Debug;

namespace Tools
{
    public class UpdateableData : ScriptableObject

    {
        public event System.Action OnValueUpdated;
        public bool AutoUpdate;
#if UNITY_EDITOR

        private void OnValidate()
        {
            if (AutoUpdate)
            {
                EditorApplication.update += NotifyOfUpdatedValues;
            }
        }


        public void NotifyOfUpdatedValues()
        {
         
            
//            Debug.Assert(EditorApplication.update != null, "EditorApplication.update != null");
//            // ReSharper disable once DelegateSubtraction
//            EditorApplication.update -= NotifyOfUpdatedValues;
//            if (OnValueUpdated == null) return;
//            OnValueUpdated();
//            EditorUtility.SetDirty(this);
        }
#endif
    }
}