using TimeSystem;
using UnityEngine;

namespace BackGround
{
    public class BackGroundMovement : MonoBehaviour
    {
        public Cloud[] Clouds;

        public float MinSpeed = .0003f;
        public float MaxSpeed = .0009f;

        private void Start()
        {
            foreach (var cloud in Clouds)
            {
                cloud.SetSpeed(Random.Range(MinSpeed,MaxSpeed));
            }
        }

        private void FixedUpdate()
        {
            if (TimeManager.Instance.GamePaused()) return;
            foreach (var cloud in Clouds)
            {
                cloud.transform.position = cloud.GetVector3Speed();
                if (!(cloud.transform.localPosition.x > 8)) continue;
                cloud.transform.localPosition = new Vector3(-8, cloud.transform.localPosition.y, cloud.transform.localPosition.z);
                cloud.SetSpeed(Random.Range(MinSpeed, MaxSpeed));
            }
        }
    }
}
