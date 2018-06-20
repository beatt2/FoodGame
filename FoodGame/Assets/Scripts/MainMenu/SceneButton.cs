using UI;
using UnityEngine.SceneManagement;

namespace MainMenu
{
    public class SceneButton : UiButtonAbstract
    {
        public override void TaskOnClick()
        {
            SceneManager.LoadScene("SampleScene");
        }
    }
}
