using System;
using Assets.SimpleAndroidNotifications;
using Cultivations;
using Events;
using TimeSystem;
using Tools;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Notifications
{
    public class MyNotificationManager : Singleton<MyNotificationManager>
    {
        private static void AddUpgradeEvent(int upgradeDuration)
        {
            var waitForSeconds = TimeManager.Instance.WaitForSeconds;
            var notificationParams = new NotificationParams
            {
                Id = Random.Range(0, int.MaxValue),
                Delay = TimeSpan.FromSeconds(waitForSeconds * upgradeDuration),
                Title = "Upgrade has ended",
                MessageEntry = "Your upgrade has ended",
                Ticker = "geld",
                Sound = true,
                Vibrate = true,
                Light = true,
                SmallIcon = NotificationIcon.Event,
                SmallIconColor = new Color(0, 0.5f, 0),
                LargeIcon = "app_icon"
            };

            NotificationManager.SendCustom(notificationParams);
        }

        public static void SetTutorialNotifications()
        {
            var waitForSeconds = TimeManager.Instance.WaitForSeconds;
            var year = TimeManager.Instance.Year;
            var month = TimeManager.Instance.Month;
            var events = EventManager.Instance.EventsArray;

            foreach (var eventsio in events)
            {
                int monthCount = 0;
                int yearGap = eventsio.Starts.y - year;
                monthCount += (yearGap * 12);
                if (yearGap > 0)
                {
                    monthCount += eventsio.Starts.x;
                    monthCount += (12 - month);
                }
                else
                {
                    monthCount += (eventsio.Starts.x - month);
                }



                Debug.Log(monthCount);



                var notificationParams = new NotificationParams
                {
                    Id = Random.Range(0, int.MaxValue),
                    Delay = TimeSpan.FromSeconds(monthCount * waitForSeconds),
                    Title = eventsio.Headline,
                    MessageEntry = "New event started " + eventsio.Headline,
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


        private void OnApplicationPause(bool value)

        {
            int outOfControlCount = 5;
            var activeCultivationUpgradeList = CultivationManager.Instance.GetActiveCultivationPrefabLists();
            foreach (var cultUpgrade in activeCultivationUpgradeList)
            {
                if (cultUpgrade.HasRequestNotication) continue;
                AddUpgradeEvent(cultUpgrade.MyCultivationPrefab.UpgradeDuration);
                cultUpgrade.HasRequestNotication = true;
                outOfControlCount--;

                if (outOfControlCount == 0)
                {
                    break;
                }
            }
        }
    }
}
