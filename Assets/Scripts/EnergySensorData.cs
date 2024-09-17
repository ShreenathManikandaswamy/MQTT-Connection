using System;

namespace Personal.DigitalTwin
{
    [Serializable]
    public class EnergySensorData
    {
        public string device;
        public string location;
        public EnergyData data;
    }

    [Serializable]
    public class EnergyData
    {
        public string timestamp;
        public float phase_1_voltage;
        public float phase_1_current;
        public float phase_1_power;
        public float phase_2_voltage;
        public float phase_2_current;
        public float phase_2_power;
        public float phase_3_voltage;
        public float phase_3_current;
        public float phase_3_power;
        public float total_power;
        public float energy_consumption;
        public Unit unit;
    }

    [Serializable]
    public class Unit
    {
        public string voltage;
        public string current;
        public string power;
        public string energy_consumption;
    }
}
