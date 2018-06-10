using UnityEngine;

namespace BackGround
{
    public class Cloud : MonoBehaviour
    {
        private float _speed = 0.003f;

        public Vector3 GetVector3Speed()
        {
            return new Vector3(transform.position.x + _speed, transform.position.y, transform.position.z);
        }

        public void SetSpeed(float speed)
        {
            _speed = speed;
        }

    }
}
