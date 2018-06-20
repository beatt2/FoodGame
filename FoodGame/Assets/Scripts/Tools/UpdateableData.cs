using UnityEngine;

namespace Tools
{
    public class UpdateableData : ScriptableObject

    {

        public bool AutoUpdate;
#if UNITY_EDITOR

        private void OnValidate()
        {
            if (AutoUpdate)
            {

            }
        }



#endif
    }
}
