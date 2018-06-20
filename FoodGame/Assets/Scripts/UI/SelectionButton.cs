using System;
using Grid;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class SelectionButton : UiButtonAbstract
    {
        private Image _image;


        private Color _startColor;

        public Button YesButton;
        public Button NoButton;
        public Button ConfirmCornButton;
        public Button ConfirmCarrotButton;

        public enum CurrentStateEnum {Field, Upgrade, BuildFarm}

        public CurrentStateEnum CurrentState;

        protected override void Awake()
        {
            base.Awake();
            _image = GetComponent<Image>();
            _startColor = _image.color;
            CurrentState = CurrentStateEnum.BuildFarm;
        }


        public override void TaskOnClick()
        {
            if (CurrentState == CurrentStateEnum.BuildFarm)
            {
                GridManager.Instance.SetSelectionSize(4);
                ChangeColor();
                if (GridManager.Instance.GetSelectedNode() == null) return;
                ToggleYesNoButtons();
            }
            else switch (CurrentState)
            {
                case CurrentStateEnum.Upgrade:
                    throw new NotImplementedException();
                case CurrentStateEnum.BuildFarm:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }


        }

        public void SetSelectionButtonText(string text)
        {
            MyButton.GetComponentInChildren<Text>().text = text;
        }


        public bool YesNoButtonActivated()
        {
            return YesButton.gameObject.activeSelf;
        }

        private void ToggleYesNoButtons()
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
            ConfirmCornButton.gameObject.SetActive(true);
            ConfirmCarrotButton.gameObject.SetActive(true);
        }

        public void SetAllActive(bool active)
        {
            ConfirmCornButton.gameObject.SetActive(active);
            ConfirmCarrotButton.gameObject.SetActive(active);
            YesButton.gameObject.SetActive(active);
            NoButton.gameObject.SetActive(active);
            ChangeColor();
        }


        //Set by button
        public void ConfirmBuildButtonPressed(int index)
        {
            GridManager.Instance.ConfirmBuildFarmButtonPressed(index);

        }

        private void ChangeColor()
        {
            _image.color = _image.color == _startColor ? Color.green : _startColor;

        }
    }
}
