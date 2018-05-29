using Cultivations;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeEntry : MonoBehaviour
{
    private Sprite _mySprite;
    //public Text Stats;

    private Image _image;
    private Button _button;

    private UpgradeTab _upgradeTab;

    public Cultivation Cultivation;

    private void Awake()
    {
        _image= GetComponentInChildren<Image>();
        _button = GetComponentInChildren<Button>();
        _button.onClick.AddListener(OnButtonClick);
        _upgradeTab = GameObject.FindGameObjectWithTag("UpgradeTab").GetComponent<UpgradeTab>();

    }

    public void SetSpriteImage(Sprite sprite)
    {
        _mySprite = sprite;
        _image.sprite = _mySprite;
    }



    private void OnButtonClick()
    {
        _upgradeTab.UpdateButtonClicked(Cultivation);
    }
}
