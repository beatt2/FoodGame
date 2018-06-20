using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [ExecuteInEditMode]
    public abstract class UiButtonAbstract : MonoBehaviour , IButtonInteractable
    {
        protected Button MyButton;
        public bool UseBestFit = true;

        private Text _childText;


        protected virtual void Awake()
        {
            _childText = GetComponentInChildren<Text>();
            MyButton = GetComponent<Button>();

        }

        public virtual void TaskOnClick()
        {

        }

    }
}
