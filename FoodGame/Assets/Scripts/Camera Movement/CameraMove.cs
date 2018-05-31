using UnityEditor.Experimental.UIElements;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

namespace Camera_Movement
{
    public class CameraMove : MonoBehaviour
    {
        public float OrthoZoomSpeed = 0.5f;
        public float Speed = 0.1f;

        private const int MinOrtographicSize = 5;
        private const int MaxOrtographicSize = 17;
        private const int OrtographicTotalSize = MaxOrtographicSize - MinOrtographicSize;

        private float _maxYSizeClamp = 6;
        private float _minYSizeClamp = 0;
        private float _maxXSizeClamp = 11.8f;
        private float _minXSizeClamp = 0;
        private float _percentageValue = 8.3f;


        private void Update()
        {
           
            if (Input.touchCount == 2)
            {
                Touch touchZero = Input.GetTouch(0);
                Touch touchOne = Input.GetTouch(1);
                Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
                Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;
                float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
                float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;
                float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;
                Camera.main.orthographicSize += deltaMagnitudeDiff * OrthoZoomSpeed;
                Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, MinOrtographicSize, MaxOrtographicSize);

            }
            else if (Input.touchCount == 1)
            {
                Touch touchZero = Input.GetTouch(0);
                if (touchZero.phase != TouchPhase.Moved) return;
                Vector2 touchDeltaPosition = touchZero.deltaPosition;
                transform.Translate(-touchDeltaPosition.x * Speed, -touchDeltaPosition.y * Speed, 0);

                float xClampPercentage = Camera.main.orthographicSize - 5 * _percentageValue;
                float xClampValue = _maxXSizeClamp / 100 * xClampPercentage;
                float yClampPerecentage = Camera.main.orthographicSize - 5 * _percentageValue;
                float yClampValue = _maxYSizeClamp / 100 * yClampPerecentage;
                

                transform.position = 
                    new Vector3(
                        Mathf.Clamp(transform.position.x, -xClampValue, xClampValue),
                        Mathf.Clamp(transform.position.y, -yClampValue, yClampValue),
                        -10
                        );
            }

        }


    }
}