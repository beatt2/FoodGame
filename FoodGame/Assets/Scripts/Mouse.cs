using UnityEngine;

public class Mouse : MonoBehaviour 
{

    private void OnMouseDown()
    {
        Destroy(gameObject);
    }
}
