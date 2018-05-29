using Cultivations;
using UnityEngine;

public class UpgradeTab : MonoBehaviour
{
    public GameObject Entry;
    public RectTransform Content;
    public RectTransform StartingPosition;
    public float Gap;

    private RectTransform _currentPosition;



    public void ActivateTab(Cultivation cultivation)
    {
        _currentPosition = StartingPosition;
        
        if (cultivation.UpgradeOptions == null)
        {
            Debug.LogError("No upgrade options on cultivation");
        }
        else
        {
            foreach (var t in cultivation.UpgradeOptions)
            {
                 
                GameObject go = Instantiate(Entry, _currentPosition.position, Quaternion.identity, Content.transform);
                go.GetComponent<UpgradeEntry>().Cultivation = cultivation;
                t.GetComponent<BuildingPrefab>().CustomAwake();
                var tempSprite = t.GetComponent<BuildingPrefab>().MyBuilding.Image;
                go.GetComponent<UpgradeEntry>().SetSpriteImage(tempSprite);
                _currentPosition.position = new Vector3(_currentPosition.position.x + Gap, _currentPosition.position.y, _currentPosition.position.z);
               
            }
        }
    }

    public void UpdateButtonClicked(Cultivation cultivation)
    {
        Debug.Log("Button has been pressed");
    }


}
