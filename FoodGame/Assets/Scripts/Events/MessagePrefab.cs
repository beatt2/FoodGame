using UnityEngine;
using UnityEngine.UI;

namespace Events
{
    public class MessagePrefab : MonoBehaviour
    {
        public Text Headline;
        public Text Content;
        public Text Effect;


        public void ChangeText(string headline,string content,string effect)
        {
            Headline.text = headline;
            Content.text = content;
            Effect.text = effect;

        }
    }



}
