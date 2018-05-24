using Grid;
using UnityEngine;

namespace UI
{
    public class BuildButton : UIButtonAbstract
    {
        public GameObject BuildLayer;
        
        protected override void Awake()
        {
            base.Awake();
            MyButton.interactable = true;
        }

        public override void TaskOnClick()
        {
            BuildLayer.SetActive(true);
        }

     
        

    }
}
