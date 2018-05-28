using Cultivations;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class BuildPanel : MonoBehaviour
    {
        public Text HeaderText;
        public Sprite MySprite;
        public Text UpgradeText;
        public Text MoneyTickText;


        private PanelLerp _panelLerp;
        private Sprite _defaultSprite;

        private void Awake()
        {
            _defaultSprite = GetComponent<SpriteRenderer>().sprite;
            _panelLerp = GetComponent<PanelLerp>();
        }

        public void TogglePannel()
        {
            
        }
        


        public void SetPanel(NodeState nodeState, Cultivation cultivation)
        {
            
        }
        
       


    }
}
