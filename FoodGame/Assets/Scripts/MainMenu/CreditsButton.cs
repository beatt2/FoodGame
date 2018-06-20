using UnityEngine;
using UnityEngine.SceneManagement;

namespace MainMenu
{
    public class CreditsButton : MonoBehaviour
    {

        public void OpenCredits()
        {
            SceneManager.LoadScene("Credits");
        }

        public void BackToMainMenu()
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
