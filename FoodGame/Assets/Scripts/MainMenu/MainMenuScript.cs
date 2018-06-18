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

        private const string _filenameReview = "review";
        private string _extensionReview = "saveReview";

        private string _filenameMessage = "messages";
        private string _extensionMessage = "saveMessage";

        private string filenameTime = "time";
        private string extensionTime = "saveTime";

        private void Awake()
        {
            Screen.SetResolution(1920,1080,true);
        }
        
        public void OnReset()
        {
            File.Delete(GetPath(FilenameNode, ExtensionNode));
            File.Delete(GetPath(filenameTime, extensionTime));
            File.Delete(GetPath(_filenameMessage, _extensionMessage));
            File.Delete(GetPath(_filenameReview, _extensionReview));
            NotificationManager.CancelAll();
        }
        private static string GetPath(string filename, string extension)
        {
            return Application.persistentDataPath + "/" + filename + "." + extension;
        }

        public void OnCredits()
        {
            
        }

        public void TaskOnClick()
        {
            SceneManager.LoadScene("SampleScene");
        }
    }
}
