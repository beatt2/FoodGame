using UnityEditor.Experimental.UIElements;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

namespace Camera_Movement
{
    public class CameraMove : MonoBehaviour
    {
        public float OrthoZoomSpeed = 0.5f;
        public float Speed = 0.1f;

        public float MaxOrthoSize = 17;
        public float MinOrthoSize = 5;

        public float MinSizeX = 12.78f;
        public float MinSizeY = 6.77f;

        private BoxCollider2D _boxCollider2D;

        private float _totalSize;


        private void Start()
        {
            _boxCollider2D = GetComponent<BoxCollider2D>();
            _totalSize = MaxOrthoSize - MinOrthoSize;
        }


     


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
                Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, 5f, 17f);

            }
            else if (Input.touchCount == 1)
            {
                Touch touchZero = Input.GetTouch(0);
                if (touchZero.phase != TouchPhase.Moved) return;
                Vector2 touchDeltaPosition = touchZero.deltaPosition;
                transform.Translate(-touchDeltaPosition.x * Speed, -touchDeltaPosition.y * Speed, 0);

                transform.position = 
                    new Vector3(Mathf.Clamp(transform.position.x, -5f, 5f),
                        Mathf.Clamp(transform.position.y, -0.82f,3f),
                        -10
                        
                        );
            }

        }

        private void CalculateBounds()
        {
            float localSize = Camera.main.orthographicSize  ;
            float calcSize = _totalSize - localSize;
            float percentage = calcSize / (_totalSize / 100);
            float clampX = percentage * (MinSizeX / 100);
            float clampY = percentage * (MinSizeY / 100);

        }

        private void LateUpdate()
        {
//            Vector3 v3 = transform.position;
//            v3.x = Mathf.Clamp(v3.x, _left, _right);
//            v3.y = Mathf.Clamp(v3.y, _bottom, _top);
//            transform.position = v3;
        }
    }
}