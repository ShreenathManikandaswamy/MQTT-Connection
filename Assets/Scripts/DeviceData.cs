using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Personal.DigitalTwin
{
    [CreateAssetMenu(menuName = "Personal/DeviceData",
        fileName = "DeviceData")]
    public class DeviceData : ScriptableObject
    {
        public string deviceId;
        public string deviceName;
        public Sprite deviceIcon;
        public GameObject deviceObject;
        public string[] topics;
        public string[] sensorTypes;
        public string[] sensorNames;
        public string targetName;
    }
}
