using Grid;

namespace UI
{
    public class BuildButton : UiButtonAbstract
    {
        public Selection MySelection;

        protected override void Awake()
        {
            base.Awake();
            MyButton.interactable = true;
        }

        public override void TaskOnClick()
        {
            MySelection.ToggleBuildPanel(true);
        }





    }
}
