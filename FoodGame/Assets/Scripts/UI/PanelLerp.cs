using System.Collections;
using UnityEngine;
using UnityScript.Macros;

public class PanelLerp : MonoBehaviour
{
    [SerializeField] private Vector3 _start;
    [SerializeField] private Vector3 _end;

    private RectTransform _rectTransform;
    public float Speed;
    

    private void Awake()
    {
        
    }

    private void Start()
    {
        //StartCoroutine(LerpToTarget());     
    }



    public IEnumerator LerpToTarget()
    {
        bool moving = true;
        while (moving)
        {
            _rectTransform.localPosition = Vector3.Lerp(_start, _end, Speed);
            if (_rectTransform.localPosition.x < 811)
            {
                break;
            }
            yield return null;
        }   
    }
    
    public IEnumerator LerpBackFromTarget()
    {
        bool moving = true;
        while (moving)
        {
            _rectTransform.localPosition = Vector3.Lerp(_end, _start, Speed);
            if (_rectTransform.localPosition.x > 1110)
            {
                break;
            }
            yield return null;
        }   
    }
    
    

}
