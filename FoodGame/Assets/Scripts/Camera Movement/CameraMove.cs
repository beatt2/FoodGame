using UnityEditor.Experimental.UIElements;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

namespace Camera_Movement
{
    public class CameraMove : MonoBehaviour
    {
        public float OrthoZoomSpeed = 0.5f;
        public float Speed = 0.1f;
  
        public Transform Area;

        private Sprite _sprite;
        private float _pixelUnits;
        private Vector2 _size;
        private Vector2 _offset;
        
        
        //Bounds
        private float _left;
        private float _right;
        private float _top;
        private float _bottom;
        private float _maxZoom;
        private float _minZoom;

        private void Start()
        {
            _sprite = Area.transform.GetComponent<SpriteRenderer>().sprite;

            CalculatePixelUnit();
            CalculateSize();
            Refresh();
            Center();
        }

        private void CalculatePixelUnit()
        {
            _pixelUnits = _sprite.rect.width / _sprite.bounds.size.x;
        }

        private void CalculateSize()
        {
            _size = new Vector2(Area.transform.localScale.x * _sprite.texture.width / _pixelUnits,
                    Area.transform.localScale.y * _sprite.texture.height / _pixelUnits);

            _offset = Area.transform.position;
        }

        private void Refresh()
        {
            float w = Screen.width / _size.x;
            float h = Screen.height / _size.y;
            float ratio1 = w / h;
            float ratio2 = h / w;
            if (ratio2 > ratio1)
            {
                _maxZoom = (_size.y / 2);

            }
            else
            {
                _maxZoom = (_size.y / 2);
                _maxZoom /= ratio1;
                
            }

            _minZoom = 1;
            //Camera.main.orthographicSize = _maxZoom;
            RefreshBounds();

        }

        private void Center()
        {
            Vector2 position = Area.transform.position;
            Vector3 camPosition = position;
            Vector3 point = Camera.main.WorldToViewportPoint(camPosition);
            Vector3 delta =  Camera.main.WorldToViewportPoint(new Vector3(.5f, .5f, point.z));
            Vector3 destination = transform.position + delta;
            transform.position = destination;
        }

        private void RefreshBounds()
        {
            var vertExtent = Camera.main.orthographicSize;
            var horzExtent = vertExtent * Screen.width / Screen.height;

            _left = horzExtent - _size.x / 2.0f / 2.0f + _offset.x;
            _right = _size.x / 2.0f - vertExtent + _offset.y;
            _bottom = vertExtent - _size.y / 2.0f + _offset.y;
            _top = _size.y / 2.0f - vertExtent + _offset.y;
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
            Refresh();
        }

        private void LateUpdate()
        {
            Vector3 v3 = transform.position;
            v3.x = Mathf.Clamp(v3.x, _left, _right);
            v3.y = Mathf.Clamp(v3.y, _bottom, _top);
            transform.position = v3;
        }
    }
}