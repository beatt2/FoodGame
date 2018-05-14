using Grid;

namespace UI
{
    public class BuildButton : UIButtonAbstract
    {
        protected override void Awake()
        {
            base.Awake();
            MyButton.interactable = false;
        }

        public override void OnButtonClick()
        {
            GridManager.Instance.BuildButtonPressed();
        }

        public void SetButtonInteractable(bool value)
        {
            MyButton.interactable = value;
        }
        

    }
}
