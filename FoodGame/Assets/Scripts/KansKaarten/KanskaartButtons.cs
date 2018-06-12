using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KanskaartButtons : MonoBehaviour
{
    public GameObject Kanskaart;
    public void Confirm()
    {
        Kanskaart.SetActive(false);
    }

    public void Decline()
    {
        Kanskaart.SetActive(false);
    }
}
