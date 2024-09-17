using System.Collections.Generic;
using Personal.DigitalTwin;
using TMPro;
using UnityEngine;
using Personal.Utilities;
using UnityEngine.SceneManagement;
using System.IO;

namespace Personal.UI
{
    public class AlarmPageHelper : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI notificationTitle;
        [SerializeField]
        private TextMeshProUGUI notificationBody;
        [SerializeField]
        private TextMeshProUGUI deviceName;
        [SerializeField]
        private DigitalTwinAlarm[] alarmOptions;
        [SerializeField]
        private MainMenuManager menuManager;

        [SerializeField]
        private Transform errorDetailParent;
        [SerializeField]
        private ErrorDetailHelper errorDetailPrefab;
        [SerializeField]
        private ResolutionStepsHelper resolutionStepsPrefab;

        private string path;
        private NotificationData notification;
        private DigitalTwinManager digitalTwinManager;

        private void Start()
        {
            digitalTwinManager = FindObjectOfType<DigitalTwinManager>();
        }

        public void ShowAlarmPage(IDictionary<string, string> data, bool store)
        {
            if(data.Count > 0)
            {
                notification = new NotificationData
                {
                    title = data["title"].ToString(),
                    message = data["message"].ToString(),
                    errorCode = data["errorCode"].ToString(),
                    deviceName = data["deviceName"].ToString(),
                    errorData = data["errorData"].ToString(),
                    deviceTag = data["deviceTag"].ToString()
                };

                if(store)
                    StoreNotificationHistory(notification);

                notificationTitle.text = notification.title;
                notificationBody.text = notification.message;
                deviceName.text = notification.deviceName;

                foreach(DigitalTwinAlarm alarm in alarmOptions)
                {
                    if(alarm.errorCode == notification.errorCode)
                    {
                        deviceName.text = notification.deviceName;

                        ErrorDetailHelper errorCode = Instantiate(errorDetailPrefab, errorDetailParent);
                        errorCode.ShowErrorDetail("Error Code : ", notification.errorCode);

                        ErrorDetailHelper errorTitle = Instantiate(errorDetailPrefab, errorDetailParent);
                        errorTitle.ShowErrorDetail("Error Title : ", alarm.Name);

                        ErrorDetailHelper errorDesc = Instantiate(errorDetailPrefab, errorDetailParent);
                        errorDesc.ShowErrorDetail("Error Description : ", alarm.description);

                        ErrorDetailHelper errorType = Instantiate(errorDetailPrefab, errorDetailParent);
                        errorType.ShowErrorDetail("Error Type : ", alarm.type);

                        ErrorDetailHelper errorSeverity = Instantiate(errorDetailPrefab, errorDetailParent);
                        errorSeverity.ShowErrorDetail("Error Severity : ", alarm.severity);

                        //assuming the issue is from the temperature component of vibration sensor
                        VibrationSensorData sensorData = JsonUtility.FromJson<VibrationSensorData>(notification.errorData);
                        ErrorDetailHelper errorData = Instantiate(errorDetailPrefab, errorDetailParent);
                        errorData.ShowErrorDetail("Temperature : ", sensorData.data.temperature.ToString() + " C");
                    }
                }
            }
        }

        public void Inspect()
        {
            digitalTwinManager.Disconnect();
            PlayerPrefs.SetString("errorCode", notification.errorCode);
            PlayerPrefs.SetString("currentTarget", notification.deviceTag);
            SceneManager.LoadScene(1);
        }

        public void Ignore()
        {
            Destroy(this.gameObject);
        }

        public void ResolutionSteps()
        {
            ResolutionStepsHelper instance = Instantiate(resolutionStepsPrefab, this.gameObject.transform);
            foreach (DigitalTwinAlarm alarm in alarmOptions)
            {
                if (alarm.errorCode == notification.errorCode)
                {
                    instance.ShowSteps(alarm.resolutionSteps);
                }
            }
        }

        public void CloseAlarm()
        {
            Destroy(this.gameObject);
        }

        private void StoreNotificationHistory(NotificationData notification)
        {
            path = Application.persistentDataPath + "/notifications.json";
            if (File.Exists(path))
            {
                string jsonString = File.ReadAllText(path);
                NotificationsHistory notifications = JsonUtility.FromJson<NotificationsHistory>(jsonString);
                if(notifications.notification == null)
                {
                    notifications.notification = new List<NotificationData>();
                }

                notifications.notification.Add(notification);

                jsonString = JsonUtility.ToJson(notifications);

                File.WriteAllText(path, jsonString);
            }
            else
            {
                NotificationsHistory notificationsHistory = new NotificationsHistory();
                notificationsHistory.notification = new List<NotificationData>
                {
                    notification
                };

                string jsonString = JsonUtility.ToJson(notificationsHistory);

                File.WriteAllText(path, jsonString);
            }

            menuManager = FindObjectOfType<MainMenuManager>();
            menuManager.SetupNotifications();
        }
    }
}
