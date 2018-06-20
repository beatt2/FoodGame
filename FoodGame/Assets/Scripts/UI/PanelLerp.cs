using System.Collections;
using UnityEngine;

namespace UI
{
    public class PanelLerp : MonoBehaviour
    {
        [SerializeField] private Vector3 _start;
        [SerializeField] private Vector3 _end;

        private RectTransform _rectTransform;
        public float Speed;
        private bool _active;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
        }


        public bool IsActive()
        {
            return _active;
        }

        public void ToggleLerp()
        {
            StartCoroutine(_active ? LerpBackFromTarget() : LerpToTarget());
        }


        private IEnumerator LerpToTarget()
        {
            bool moving = true;
            while (moving)
            {

                _rectTransform.localPosition = Vector3.Lerp(_start, _end, Speed);
                if (_rectTransform.localPosition.x < _end.x + 30)
                {
                    _active = true;
                    break;
                }
                yield return null;
            }
        }

        private IEnumerator LerpBackFromTarget()
        {
            bool moving = true;
            while (moving)
            {
                _rectTransform.localPosition = Vector3.Lerp(_end, _start, Speed);
                if (_rectTransform.localPosition.x > _start.x - 30)
                {
                    _active = false;
                    break;
                }
                yield return null;
            }
        }



    }
}
