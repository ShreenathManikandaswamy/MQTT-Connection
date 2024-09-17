using TMPro;
using UnityEngine.UI;
using UnityEngine;
using Personal.DigitalTwin;
using Personal.Utilities;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

namespace Personal.UI
{
    public class DeviceDetailsHelper : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI deviceName;
        [SerializeField]
        private Image deviceIcon;
        [SerializeField]
        private TextMeshProUGUI sensorName;
        [SerializeField]
        private Transform sensorNamesParent;
        [SerializeField]
        private SensorDataHelper sensorDataPrefab;
        [SerializeField]
        private Transform sensorDataParent;
        [SerializeField]
        private ToggleController toggleController;
        [SerializeField]
        private Transform deviceObjectParent;

        private DigitalTwinManager digitalTwinManager;
        private DeviceData data;

        public void SetupData(DeviceData deviceData, DigitalTwinManager manager)
        {
            data = deviceData;
            digitalTwinManager = manager;
            deviceName.text = deviceData.deviceName;
            deviceIcon.sprite = deviceData.deviceIcon;

            Instantiate(deviceData.deviceObject, deviceObjectParent);

            for(int i = 0; i< deviceData.topics.Length; i++)
            {
                TextMeshProUGUI sensorNameInstance = Instantiate(sensorName, sensorNamesParent);
                sensorNameInstance.text = deviceData.topics[i];

                ToggleHelper toggleHelper = sensorNameInstance.gameObject.GetComponent<ToggleHelper>();
                toggleHelper.toggleController = toggleController;
                toggleHelper.index = i;

                SensorDataHelper go = Instantiate(sensorDataPrefab, sensorDataParent);
                go.SetupSensorData(deviceData.sensorTypes[i], deviceData.sensorNames[i] , digitalTwinManager);
                go.gameObject.SetActive(false);

                toggleHelper.myPanel = go.gameObject;
                if(toggleController.toggleHelpers == null)
                {
                    toggleController.toggleHelpers = new List<ToggleHelper>();
                }
                toggleController.toggleHelpers.Add(toggleHelper);
            }

            toggleController.Init();
        }

        public void BackButton()
        {
            Destroy(this.gameObject);
        }

        public void InspectDevice()
        {
            digitalTwinManager.Disconnect();
            Debug.Log(data.targetName);
            PlayerPrefs.SetString("currentTarget", data.targetName);
            SceneManager.LoadScene(1);
        }
    }
}
