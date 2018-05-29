using UnityEngine;
using UnityEngine.UI;

public class UpgradeEntry : MonoBehaviour
{
    private Sprite _mySprite;
    //public Text Stats;

    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    public void SetSpriteImage(Sprite sprite)
    {
        _mySprite = sprite;
    }
}
