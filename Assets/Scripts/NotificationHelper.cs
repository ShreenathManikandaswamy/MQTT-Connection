using TMPro;
using UnityEngine;
using Personal.Utilities;
using Personal.DigitalTwin;
using System.Collections.Generic;

namespace Personal.UI
{
    public class NotificationHelper : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI errorName;
        [SerializeField]
        private TextMeshProUGUI timeStamp;
        [SerializeField]
        private DigitalTwinAlarm[] alarmOptions;

        private Personal.CloudMessaging.CloudMessaging cloud;
        private NotificationData notification;

        private void Start()
        {
            cloud = FindObjectOfType<Personal.CloudMessaging.CloudMessaging>();
        }

        public void SetupNotification(NotificationData notificationData)
        {
            notification = notificationData;
            foreach(DigitalTwinAlarm alarm in alarmOptions)
            {
                if(alarm.errorCode == notificationData.errorCode)
                {
                    errorName.text = alarm.Name;
                }
            }

            VibrationSensorData sensorData = JsonUtility.FromJson<VibrationSensorData>(notificationData.errorData);
            timeStamp.text = GetFormattedTimestamp(sensorData.data.timestamp);
        }

        private string GetFormattedTimestamp(string timestamp)
        {
            string[] value = timestamp.Split("T");
            string[] output = value[1].Split("Z");

            string final = output[0] + "\n" +
                value[0];

            return final;
        }

        public void ShowNotification()
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("title", notification.title);
            dict.Add("message", notification.message);
            dict.Add("errorCode", notification.errorCode);
            dict.Add("deviceName", notification.deviceName);
            dict.Add("errorData", notification.errorData);
            dict.Add("deviceTag", notification.deviceTag);

            cloud.InstantiateAlarmPage(dict, false);
        }
    }
}
