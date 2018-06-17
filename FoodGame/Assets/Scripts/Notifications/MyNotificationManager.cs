using System;
using Assets.SimpleAndroidNotifications;
using Cultivations;
using TimeSystem;
using Tools;
using UnityEngine;

namespace Notifications
{
    public class MyNotificationManager : Singleton<MyNotificationManager>
    {
        public void AddUpgradeEvent(int upgradeDuration)
        {
            var waitForSeconds = TimeManager.Instance.WaitForSeconds;
            var notificationParams = new NotificationParams
            {
                Id = UnityEngine.Random.Range(0, int.MaxValue),
                Delay = TimeSpan.FromSeconds(5),
                Title = "Upgrade has ended",
                MessageEntry = "Your upgrade has ended",
                Ticker = "geld",
                Sound = true,
                Vibrate = true,
                Light = true,
                SmallIcon = NotificationIcon.Message,
                SmallIconColor = new Color(0, 0.5f, 0),
                LargeIcon = "app_icon"
            };

            NotificationManager.SendCustom(notificationParams);
        }

    
        private void OnApplicationPause(bool value)
        {
    
            int outOfControlCount = 5;
            var activeCultivationUpgradeList = CultivationManager.Instance.GetActiveCultivationPrefabLists();
            for (int i = 0; i < activeCultivationUpgradeList.Count; i++)
            {
                if (!activeCultivationUpgradeList[i].HasRequestNotication)
                {
                    AddUpgradeEvent(activeCultivationUpgradeList[i].MyCultivationPrefab.UpgradeDuration);
                    activeCultivationUpgradeList[i].HasRequestNotication = true;
                    outOfControlCount--;

                    if (outOfControlCount == 0)
                    {
                        break;
                    }
                }
            }
        }


        private void CalculateToRealWorldTime()
        {
        }
    }
}