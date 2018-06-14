using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Tutorial
{



    public class TutorialScript : MonoBehaviour
    {
        private bool _tutorialFinished = false;

        public Text TutorialText;

        public GameObject TutorialBg;

        public Tutorials[] Tutorials;
        public GameObject[] Arrows;
        public int Index;

        public GameObject GridArrow;
        // Use this for initialization
        void Start()
        {
            
            if (!_tutorialFinished)
            {
                TutorialBg.SetActive(true);
                TutorialText.text = Tutorials[0].Text;
            }
            else
            {
                TutorialBg.SetActive(false);
            }

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void OnButtonClick()
        {
            Index++;
            switch (Index)
            {
                case 1:
                    TutorialText.text = Tutorials[Index].Text;
                    Arrows[Index - 1].SetActive(true);
                    
                    break;
                case 2:
                    TutorialText.text = Tutorials[Index].Text;
                    Arrows[Index -1].SetActive(true);
                    Arrows[Index - 2].SetActive(false);
                    break;
                case 3:
                    TutorialText.text = Tutorials[Index].Text;
                    Arrows[Index - 1].SetActive(true);
                    Arrows[Index - 2].SetActive(false);
                    break;
                case 4:
                    TutorialText.text = Tutorials[Index].Text;
                    Arrows[Index - 1].SetActive(true);
                    Arrows[Index - 2].SetActive(false);
                    break;
                case 5:
                    TutorialText.text = Tutorials[Index].Text;
                    Arrows[Index - 1].SetActive(true);
                    Arrows[Index - 2].SetActive(false);
                    break;
                case 6:
                    TutorialText.text = Tutorials[Index].Text;
                    Arrows[Index - 1].SetActive(true);
                    Arrows[Index - 2].SetActive(false);
                    break;
                case 7:
                    TutorialText.text = Tutorials[Index].Text;
                    Arrows[Index - 1].SetActive(true);
                    Arrows[Index - 2].SetActive(false);
                    break;
                case 8:
                    TutorialText.text = Tutorials[Index].Text;
                    Arrows[3].SetActive(true);
                    Arrows[Index - 2].SetActive(false);
                    break;
                case 9:
                    
                    TutorialText.text = Tutorials[Index].Text;
                    Arrows[3].SetActive(false);
                    break;
                case 10:                 
                    TutorialText.text = Tutorials[Index].Text;
                    break;
                case 11:
                    TutorialBg.SetActive(false);
                    _tutorialFinished = true;
                    //save
                    break;
                default:
                    break;
            }
        }

        public void SkipTutorial()
        {
            TutorialBg.SetActive(false);
            _tutorialFinished = true;
            //save
        }
    }
}
