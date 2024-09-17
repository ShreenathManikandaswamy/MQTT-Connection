using Personal.DigitalTwin;
using UnityEngine;
using System.IO;
using Personal.Utilities;

namespace Personal.UI
{
    public class MainMenuManager : MonoBehaviour
    {
        [Header("Reference Data")]
        [SerializeField]
        private DeviceData[] devices;
        [SerializeField]
        private DigitalTwinManager digitalTwinManager;

        [Space(10)]
        [Header("UI Components")]
        [SerializeField]
        private DeviceDetailsHelper deviceDetailsPrefab;
        [SerializeField]
        private Transform deviceDetailsParent;
        [SerializeField]
        private DevicePrefabHelper devicePrefab;
        [SerializeField]
        private Transform deviceParent;
        [SerializeField]
        private GameObject landingPage;
        [SerializeField]
        private GameObject homePage;

        [Space(10)]
        [Header("Notification panel")]
        [SerializeField]
        private NotificationHelper notificationPrefab;
        [SerializeField]
        private Transform notificationParent;

        private string path;

        private void Start()
        {
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
            path = Application.persistentDataPath + "/notifications.json";

            if (PlayerPrefs.HasKey("back"))
            {
                if(PlayerPrefs.GetInt("back") == 1)
                {
                    landingPage.SetActive(false);
                    homePage.SetActive(true);
                    PlayerPrefs.DeleteAll();
                }
            }

            foreach(DeviceData device in devices)
            {
                DevicePrefabHelper devicePrefabInstance = Instantiate(devicePrefab, deviceParent);
                devicePrefabInstance.SetupDevice(device.deviceName, device.deviceIcon, device.deviceId, this);
            }

            SetupNotifications();
        }

        public void ShowDeviceDetails(string deviceId)
        {
            foreach(DeviceData device in devices)
            {
                if(device.deviceId == deviceId)
                {
                    DeviceDetailsHelper deviceDetails = Instantiate(deviceDetailsPrefab, deviceDetailsParent);
                    deviceDetails.SetupData(device, digitalTwinManager);

                    digitalTwinManager.topic.Clear();

                    foreach(string topic in device.topics)
                    {
                        digitalTwinManager.topic.Add(topic);
                    }
                }
            }

            digitalTwinManager.Connect();
        }

        public void SetupNotifications()
        {
            if (File.Exists(path))
            {
                string jsonString = File.ReadAllText(path);
                NotificationsHistory notifications = JsonUtility.FromJson<NotificationsHistory>(jsonString);

                foreach(Transform child in notificationParent)
                {
                    Destroy(child.gameObject);
                }

                foreach(NotificationData notification in notifications.notification)
                {
                    NotificationHelper notificationInstance = Instantiate(notificationPrefab, notificationParent);
                    notificationInstance.SetupNotification(notification);
                }
            }
        }
    }
}
