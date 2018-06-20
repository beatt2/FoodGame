using UnityEngine;

namespace UI
{
    public class TabCloser : MonoBehaviour
    {
        public GameObject Messages;
        public GameObject Reviews;
        public GameObject Stats;
        public GameObject Shop;

        public void SetOtherTabsToInActive(int index)
        {
            switch (index)
            {
                //Messages
                case 0:

                    Reviews.SetActive(false);
                    Stats.SetActive(false);
                    Shop.SetActive(false);
                    break;
                //Reviews
                case 1:
                    Messages.SetActive(false);
                    Stats.SetActive(false);
                    Shop.SetActive(false);
                    break;
                //Stats
                case 2:
                    Messages.SetActive(false);
                    Reviews.SetActive(false);
                    Shop.SetActive(false);

                    break;
                //Shop
                case 3:
                    Messages.SetActive(false);
                    Reviews.SetActive(false);
                    Stats.SetActive(false);
                    break;
            }
        }
    }
}
