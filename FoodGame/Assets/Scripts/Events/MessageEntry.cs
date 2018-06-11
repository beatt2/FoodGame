using UnityEngine;
using UnityEngine.UI;

namespace Events
{
    public class MessageEntry
    {
        public string HeadlineText;
        public string ContentText;
        public string EffectText;

        public MessageEntry(string headline,string content, string effect)
        {
            HeadlineText = headline;
            ContentText = content;
            EffectText = effect;
        }

    }
}
