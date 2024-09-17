using UnityEngine;

namespace Personal.DigitalTwin
{
    [CreateAssetMenu(menuName = "Personal/DT Alarm",
        fileName = "DT Alarm")]
    public class DigitalTwinAlarm : ScriptableObject
    {
        public string Name;
        public string errorCode;
        public string description;
        public string type;
        public string threshold;
        public string severity;
        public string[] resolutionSteps;
    }
}
