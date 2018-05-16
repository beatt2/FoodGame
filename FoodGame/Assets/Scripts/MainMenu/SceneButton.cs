using UI;
using UnityEngine.SceneManagement;

namespace MainMenu
{
    public class SceneButton : UIButtonAbstract
    {
        public override void TaskOnClick()
        {
            SceneManager.LoadScene("SampleScene");
        }
    }
}
