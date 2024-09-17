using TMPro;
using UnityEngine.UI;
using UnityEngine;
using Personal.DigitalTwin;

namespace Personal.UI
{
    public class DevicePrefabHelper : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI deviceName;
        [SerializeField]
        private Image deviceIcon;

        private MainMenuManager manager;
        public string Id;

        public void SetupDevice(string deviceNameValue, Sprite deviceSprite, string deviceId, MainMenuManager mainMenu)
        {
            manager = mainMenu;
            deviceName.text = deviceNameValue;
            deviceIcon.sprite = deviceSprite;
            Id = deviceId;
        }

        public void ButtonClick()
        {
            manager.ShowDeviceDetails(Id);
        }
    }
}
