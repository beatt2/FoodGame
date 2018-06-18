using Grid;
using Money;
using UnityEngine;
using UnityEngine.SceneManagement;

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

        public float MinMagnitudeSpeed;


        private void Start()
        {
            _boxCollider2D = GetComponent<BoxCollider2D>();
            _totalSize = MaxOrthoSize - MinOrthoSize;
        }





        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadScene("MainMenu");
                //save
            }


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
                if (touchZero.deltaPosition.magnitude < MinMagnitudeSpeed) return;
                Vector2 touchDeltaPosition = touchZero.deltaPosition;
                transform.Translate(-touchDeltaPosition.x * Speed, -touchDeltaPosition.y * Speed, 0);
                transform.position = GetBounds();
            }

        }
        private Vector3 GetBounds()
        {
            float localSize = Camera.main.orthographicSize  ;
            Vector3 localPosition = transform.position;
            float calcSize = MaxOrthoSize - localSize;
            float percentage = calcSize / (_totalSize / 100);
            float xValue = percentage * (MinSizeX / 100);
            float yValue = percentage * (MinSizeY / 100);
            localPosition.x = Mathf.Clamp(localPosition.x, -xValue, xValue);
            localPosition.y = Mathf.Clamp(localPosition.y, -yValue, yValue);
            return localPosition;
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
