using UI;
using UnityEngine.SceneManagement;

namespace MainMenu
{
    public class SceneButton : UIButtonAbstract
    {
        public override void OnButtonClick()
        {
            SceneManager.LoadScene("SampleScene");
        }
    }
}
