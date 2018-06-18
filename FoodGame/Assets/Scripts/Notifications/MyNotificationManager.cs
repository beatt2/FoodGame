using System;
using Assets.SimpleAndroidNotifications;
using Cultivations;
using Events;
using TimeSystem;
using Tools;
using UnityEditor;
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
                Delay = TimeSpan.FromSeconds(waitForSeconds),
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

        public void SetTutorialNotifications()
        {
            var waitForSeconds = TimeManager.Instance.WaitForSeconds;
            var year = TimeManager.Instance.Year;
            var month = TimeManager.Instance.Month;
            var events = EventManager.Instance.EventsArray;
            

            for (int i = 0; i < EventManager.Instance.EventsArray.Length; i++)
            {
                int monthCount = month;
                int targetYear = events[i].Starts.y;
                Debug.Log(targetYear);
                for (int j = year; j < targetYear; j++)
                {
                    for (int k = month; k < 13; k++)
                    {
                        monthCount++;
                    }

                    month = 0;
                }
                Debug.Log(monthCount * waitForSeconds);

                var notificationParams = new NotificationParams
                {
                    Id = UnityEngine.Random.Range(0, int.MaxValue),
                    Delay = TimeSpan.FromSeconds(monthCount * waitForSeconds),
                    Title = events[i].Headline,
                    MessageEntry = "New event started " + events[i].Headline,
                    Ticker = "",
                    Sound = true,
                    Vibrate = true,
                    Light = true,
                    SmallIcon = NotificationIcon.Message,
                    SmallIconColor = new Color(0, 0.5f, 0),
                    LargeIcon = "app_icon"
                };
                NotificationManager.SendCustom(notificationParams);
                
            }
  
            
        }


#if UNITY_EDITOR
        private void OnApplicationQuit()
#elif!UNITY_EDITOR
        private void OnApplicationPause(bool value)
#endif
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