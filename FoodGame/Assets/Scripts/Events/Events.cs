using UnityEngine;

namespace Events
{
    [CreateAssetMenu]

    public class Events : ScriptableObject
    {
        public string Headline;
        public string Content;
        // happenAt month/year
        public Vector2Int Starts;
        public Vector2Int Finishes;
    }
}
