using UI;
using UnityEngine.SceneManagement;

namespace MainMenu
{
    public class SceneButton : UIButtonAbstract
    {

        // Use this for initialization
        void Start ()
        {
		
        }
	
        // Update is called once per frame
        void Update ()
        {
		
        }

        public override void OnButtonClick()
        {
            SceneManager.LoadScene("SampleScene");
        }
    }
}
