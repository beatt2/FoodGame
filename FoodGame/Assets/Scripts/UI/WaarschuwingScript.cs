using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaarschuwingScript : MonoBehaviour
{


    public Text WarningText;
    public GameObject Ui;
    public void ChangeText(string text)
    {
        WarningText.text = text;
        Ui.SetActive(true);
        StopCoroutine("Timer");
        StartCoroutine("Timer");
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(2);
        Ui.SetActive(false);
    }
}
