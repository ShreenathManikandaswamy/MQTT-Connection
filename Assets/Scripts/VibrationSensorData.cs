using System;

namespace Personal.DigitalTwin
{
    [Serializable]
    public class VibrationSensorData
    {
        public string sensor;
        public string location;
        public string sampling_rate;
        public Kips kips;
        public Temperature_range temperature_range;
        public Humidity_range humidity_range;
        public float mean_rpm;
        public VibrationData data;
    }

    [Serializable]
    public class Kips
    {
        public float mean_vibration_intensity;
        public float max_vibration_intensity;
        public float min_vibration_intensity;
    }

    [Serializable]
    public class Temperature_range
    {
        public float min;
        public float max;
    }

    [Serializable]
    public class Humidity_range
    {
        public float min;
        public float max;
    }

    [Serializable]
    public class VibrationData
    {
        public string timestamp;
        public float vibration_intensity;
        public float x_axis;
        public float y_axis;
        public float z_axis;
        public string unit;
        public float temperature;
        public float humidity;
        public float rpm;
    }
}
