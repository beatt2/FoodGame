using Tools;
using UnityEngine;
using UnityEngine.UI;

namespace Events
{
    public class EventManager : Singleton<EventManager>
    {
        public Events[] EventsArray;

        public Text Headline;
        public Text Content;
        public GameObject Ui;
        private int WhichEvent;
        public bool inEventMenu;
        private bool[] EventGoingOn = new bool[10];
        
        
        
        private void Start()
        {
            Ui.SetActive(inEventMenu);

        }



        public void CheckDate(int month,int year)
        {
            for (int i = 0; i < EventsArray.Length; i++)
            {
                if (EventsArray[i].Starts == new Vector2Int(month,year))
                {
                    inEventMenu = true;
                    EventGoingOn[i] = true;
                    Ui.SetActive(inEventMenu);
                    SetText(Headline.text, Content.text, i);
                }

                if (EventsArray[i].Finishes == new Vector2Int(month, year))
                {                   
                    EventGoingOn[i] = false;
                    inEventMenu = true;
                    Ui.SetActive(inEventMenu);
                    SetTextEnded(Headline.text, Content.text, i);
                }
            }
        }
        public void SetText(string headline,string content,int whichevent)
        {
            headline = EventsArray[whichevent].Headline;
            content = EventsArray[whichevent].Content;
            Headline.text = headline;
            Content.text = content;
       
        }
        public void SetTextEnded(string headline, string content, int whichevent)
        {
            headline = EventsArray[whichevent].Headline;
            content = EventsArray[whichevent].Content;
            Headline.text = headline + " has ended";
            Content.text = content;
          
        }

    }
}
