using Cultivations;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class SidePanel : MonoBehaviour
    {
        public Text HeaderText;
        public Text UpgradeText;
        public Text MoneyTickText;


        private PanelLerp _panelLerp;
        private Sprite _defaultSprite;
        public Image MyImage;

        private void Awake()
        {
            _defaultSprite = MyImage.sprite;
            _panelLerp = GetComponent<PanelLerp>();
        }

        public void TogglePannel()
        {
            _panelLerp.ToggleLerp();
        }

        public bool SidePanelActive()
        {
            return _panelLerp.IsActive();
        }


        public void SetPanel(Cultivation cultivation)
        {
            if (!_panelLerp.IsActive())
            {
                _panelLerp.ToggleLerp();
            }

            HeaderText.text = cultivation.Name;
            MyImage.sprite = cultivation.Image;
 
            //TODO make a UpgradeCost variable in Cultivation
            UpgradeText.text = cultivation.BuildPrice.ToString();
            MoneyTickText.text = cultivation.MoneyTick.ToString();

        }
        
       


    }
}
