using UnityEngine;
using UnityEngine.Analytics;

namespace Events
{
    [CreateAssetMenu]

    public class Events : ScriptableObject
    {
        public string Headline = "This is a Headline!";
        public string Content = "This is supposed to be the content of the event";
        public int Month = 3;
        public int Year = 1;
        // happenAt month/year
        private Vector2 happensAt;


        public void OnButtonPressed()
        {
            happensAt = new Vector2(Month, Year);
        }
    }
}
