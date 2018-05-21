using System;
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
        
        public enum CurrentStateEnum {Field, Upgrade, BuildFarm}

        public CurrentStateEnum CurrentState;
 
        protected override void Awake() 
        { 
            base.Awake(); 
            _image = GetComponent<UnityEngine.UI.Image>(); 
            _startColor = _image.color;
            CurrentState = CurrentStateEnum.BuildFarm;
        } 
         
         
         
        public override void TaskOnClick() 
        {
            if (CurrentState == CurrentStateEnum.BuildFarm)
            {
                GridManager.Instance.SetSelectionSize(); 
                ChangeColor();
                if (GridManager.Instance.GetSelectedNode() == null) return;
                ToggleYesNoButtons();
            } 
            else if (CurrentState == CurrentStateEnum.Field)
            {
                if (GridManager.Instance.GetSelectedNode() == null) return;
                GridManager.Instance.ConfirmBuildFieldButtonPressed();
            }
            else if (CurrentState == CurrentStateEnum.Upgrade)
            {
                throw new NotImplementedException();
            }
        

        }

        public void SetSelectionButtonText(string text)
        {
            MyButton.GetComponentInChildren<Text>().text = text;
        }
        
        
        //TODO refactor
        public void SetCurrentState(CurrentStateEnum currentState)
        {
            CurrentState = currentState;
            switch (currentState)
            {
                    case CurrentStateEnum.BuildFarm:
                        MyButton.GetComponentInChildren<Text>().text = "Build Farm";
                        break;
                    case CurrentStateEnum.Field:
                        MyButton.GetComponentInChildren<Text>().text = "Build Field";
                        break;
                    case CurrentStateEnum.Upgrade:
                        MyButton.GetComponentInChildren<Text>().text = "Upgrade Farm";
                        break;
                default:
                    throw new ArgumentOutOfRangeException("currentState", currentState, null);
                    
            }
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
 
        
        //Set by button
        public void ConfirmLocation(bool value) 
        { 
            GridManager.Instance.ConfirmLocation(value);
            
        }

        public void SetActiveConfirmButton()
        {
            ConfirmButton.gameObject.SetActive(true);
        }

        public void SetAllActive(bool active)
        {
            ConfirmButton.gameObject.SetActive(active);
            YesButton.gameObject.SetActive(active);
            NoButton.gameObject.SetActive(active);
            ChangeColor();
        }

        
        //Set by button
        public void ConfirmBuildButtonPressed()
        {
            GridManager.Instance.ConfirmBuildFarmButtonPressed();
       
            
            
        }
 
        private void ChangeColor() 
        {
            _image.color = _image.color == _startColor ? Color.green : _startColor; 
             
        } 
    } 
} 