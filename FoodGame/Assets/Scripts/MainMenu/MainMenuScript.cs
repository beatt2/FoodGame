using System.IO;
using Assets.SimpleAndroidNotifications;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MainMenu
{
    public class MainMenuScript : MonoBehaviour
    {
        private const string FilenameNode = "node";
        private const string ExtensionNode = "saveNode";

        private const string FilenameReview = "review";
        private const string ExtensionReview = "saveReview";

        private const string FilenameMessage = "messages";
        private const string ExtensionMessage = "saveMessage";

        private const string FilenameTime = "time";
        private const string ExtensionTime = "saveTime";

        private void Awake()
        {
            Screen.SetResolution(1920,1080,true);
        }

        public void OnReset()
        {
            File.Delete(GetPath(FilenameNode, ExtensionNode));
            File.Delete(GetPath(FilenameTime, ExtensionTime));
            File.Delete(GetPath(FilenameMessage, ExtensionMessage));
            File.Delete(GetPath(FilenameReview, ExtensionReview));
            NotificationManager.CancelAll();
        }
        private static string GetPath(string filename, string extension)
        {
            return Application.persistentDataPath + "/" + filename + "." + extension;
        }


        public void TaskOnClick()
        {
            SceneManager.LoadScene("koenssample");
        }
    }
}
