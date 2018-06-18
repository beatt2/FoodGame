﻿using System.Collections;
using System.Collections.Generic;
using Save;
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

        public GameObject ShopButton;

        public Button ConfirmButton;

        public GameObject BuildButton;
        // Use this for initialization
        private void Start()
        {
            ShopButton.SetActive(false);
            if (!SaveManager.Instance.GetTutorialBool())
            {
                TutorialBg.SetActive(true);
                TutorialText.text = Tutorials[0].Text;
            }
            else
            {
                TutorialBg.SetActive(false);
            }

        }

        
        //REFACTOR
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
                    Arrows[3].SetActive(true);
                    Arrows[Index - 2].SetActive(false);
                    BuildButton.SetActive(false);
                    ConfirmButton.enabled = false;
                    ShopButton.SetActive(true);
                    break;
                default:
                    break;
            }
        }
        public void OnClickShop()
        {
            if (Index == 7)
            {
                Index++;
                TutorialText.text = Tutorials[Index].Text;

                Arrows[3].SetActive(false);
                ShopButton.SetActive(false);
                BuildButton.SetActive(true);
            }
            
        }

        public void OnClickBuilding()
        {
            if (Index == 8)
            {
                Index++;
                TutorialText.text = Tutorials[Index].Text;
            }
            

            
        }

        public void OnClickConfirmPlacement()
        {
            if (Index == 9)

            {
                Index++;
                TutorialText.text = Tutorials[Index].Text;
            }       
        }

        public void OnClickBuildingPlacement()
        {
            if (Index == 10)
            {
                Index++;
                TutorialText.text = Tutorials[Index].Text;
            }
        }

        public void OnClickField()
        {
            if (Index == 11)
            {
                Index++;
                
                Arrows[6].SetActive(true);
            }
        }

        

        public void OnClickBuyField()
        {
            if (Index == 12)
            {
                Index++;
                TutorialText.text = Tutorials[Index].Text;
                


            }
        }
        public void OnUpgrade()
        {
            Arrows[6].SetActive(false);
            TutorialBg.SetActive(false);
            _tutorialFinished = true;
            SaveManager.Instance.SetTutorialBool(true);
        }
        public void SkipTutorial()
        {
            foreach (var t in Arrows)
            {
                t.SetActive(false);
            }
            TutorialBg.SetActive(false);
            ShopButton.SetActive(false);
            BuildButton.SetActive(true);
            _tutorialFinished = true;
            SaveManager.Instance.SetTutorialBool(true);
        }


    }
}
