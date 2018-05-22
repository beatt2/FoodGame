using System.Collections.Specialized;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [ExecuteInEditMode]
    public abstract class UIButtonAbstract : MonoBehaviour , IButtonInteractable
    {
        protected Button MyButton;
        public string ButtonText = "Button";
        public bool UseBestFit = true;

        private Text _childText;
        
        
        protected virtual void Awake()
        {
            _childText = GetComponentInChildren<Text>();
            MyButton = GetComponent<Button>();
            
            //TODO EVENT GETS CALLED TWICE BECAUSE OF BUG
            //MyButton.onClick.AddListener(TaskOnClick);
            
        }

        public virtual void TaskOnClick()
        {
           
        }

        protected void UpdateTextValue()
        {
            _childText.text = ButtonText;
        }

        private void OnValidate()
        {
            //TODO IMPROVE THIS
            Awake();
            _childText.resizeTextForBestFit = UseBestFit;
            _childText.text = ButtonText;
        }
    }
}
