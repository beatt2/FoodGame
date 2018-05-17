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
            YesButton.gameObject.SetActive(!YesButton.gameObject.activeSelf); 
            NoButton.gameObject.SetActive(!NoButton.gameObject.activeSelf);

        } 
 
        public void Confirm(bool value) 
        { 
            GridManager.Instance.ConfirmBuilding(value); 
        } 
 
        private void ChangeColor() 
        {
            _image.color = _image.color == _startColor ? Color.green : _startColor; 
             
        } 
    } 
} 