using Save;
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
            for (int i = 0; i < Clouds.Length; i++)
            {
                Clouds[i].SetSpeed(Random.Range(MinSpeed,MaxSpeed));
            }
        }

        private void FixedUpdate()
        {
            if (!TimeManager.Instance.GamePaused())
            {
                for (int i = 0; i < Clouds.Length; i++)
                {
                    Clouds[i].transform.position = Clouds[i].GetVector3Speed();
                    if (Clouds[i].transform.localPosition.x > 8)
                    {

                        Clouds[i].transform.localPosition = new Vector3(-8, Clouds[i].transform.localPosition.y, Clouds[i].transform.localPosition.z);
                        Clouds[i].SetSpeed(Random.Range(MinSpeed, MaxSpeed));


                    }
                }
            }
        }
    }
}
