using Grid; 
using UnityEngine; 
using UnityEngine.UI; 
 
 
namespace UI 
{ 
    public class SelectionButton : UIButtonAbstract 
    { 
        private UnityEngine.UI.Image _image; 
        private Color _startColor; 
 
        public Button YesButton; 
        public Button NoButton;
        public Button ConfirmButton;
 
        protected override void Awake() 
        { 
            base.Awake(); 
            _image = GetComponent<UnityEngine.UI.Image>(); 
            _startColor = _image.color; 
        } 
         
         
         
        public override void TaskOnClick() 
        { 
             
            GridManager.Instance.SetSelectionSize(); 
            ChangeColor();
            if (GridManager.Instance.GetSelectedNode() == null) return;
            ToggleYesNoButtons();

        }

        public bool YesNoButtonActivated()
        {
            return YesButton.gameObject.activeSelf;
        }

        public void ToggleYesNoButtons()
        {
            YesButton.gameObject.SetActive(!YesButton.gameObject.activeSelf); 
            NoButton.gameObject.SetActive(!NoButton.gameObject.activeSelf);
        }
 
        public void ConfirmLocation(bool value) 
        { 
            GridManager.Instance.ConfirmLocation(value); 
        }

        public void SetActiveConfirmButton()
        {
            ConfirmButton.gameObject.SetActive(!ConfirmButton.gameObject.activeSelf);
        }

        public void ConfirmBuildButtonPressed()
        {
            GridManager.Instance.ConfirmBuildButtonPressed();
            
        }
 
        private void ChangeColor() 
        {
            _image.color = _image.color == _startColor ? Color.green : _startColor; 
             
        } 
    } 
} 