using E2C;
using Personal.UI;
using System.Collections.Generic;
using UnityEngine;

namespace Personal.DigitalTwin
{
    public class SensorDataHelper : MonoBehaviour
    {
        [SerializeField]
        private List<VibrationChart> _VibrationSensors = null;

        [SerializeField]
        private E2Chart _e2ChartGraphPhase1 = null;
        [SerializeField]
        private E2ChartData _e2ChartDataGraphPhase1 = null;

        [SerializeField]
        private E2Chart _e2ChartGraphPhase2 = null;
        [SerializeField]
        private E2ChartData _e2ChartDataGraphPhase2 = null;

        [SerializeField]
        private E2Chart _e2ChartGraphPhase3 = null;
        [SerializeField]
        private E2ChartData _e2ChartDataGraphPhase3 = null;

        [SerializeField]
        private E2Chart _e2ChartGraphTotal = null;
        [SerializeField]
        private E2ChartData _e2ChartDataGraphTotal = null;

        [SerializeField]
        private GameObject vibration = null;
        [SerializeField]
        private GameObject energy = null;

        private string sensorType = "";
        private string sensorName = "";
        private DigitalTwinManager digitalTwinManager;
        private int _max = 50;

        public void SetupSensorData(string type, string name, DigitalTwinManager manager)
        {
            sensorType = type;
            sensorName = name;
            digitalTwinManager = manager;
            digitalTwinManager.dataEvent.AddListener(ShowData);
        }

        private void ShowData()
        {
            if (this.gameObject.activeSelf)
            {
                if (sensorType == "vibrationSensor")
                {
                    VibrationSensorData sensorData = JsonUtility.FromJson<VibrationSensorData>(digitalTwinManager.DTMessage);

                    if (sensorName == sensorData.sensor)
                    {
                        energy.SetActive(false);
                        vibration.SetActive(true);

                        SetData(_VibrationSensors[0]._e2ChartGraphVib, _VibrationSensors[0]._e2ChartDataGraphVib, sensorData.data.vibration_intensity);
                        SetData(_VibrationSensors[0]._e2ChartGraphTemp, _VibrationSensors[0]._e2ChartDataGraphTemp, sensorData.data.temperature);
                        SetData(_VibrationSensors[0]._e2ChartGraphHum, _VibrationSensors[0]._e2ChartDataGraphHum, sensorData.data.humidity);
                        SetData(_VibrationSensors[0]._e2ChartGraphRPM, _VibrationSensors[0]._e2ChartDataGraphRPM, sensorData.data.rpm);
                    }
                }
                else if (sensorType == "energySensor")
                {
                    EnergySensorData sensorData = JsonUtility.FromJson<EnergySensorData>(digitalTwinManager.DTMessage);

                    if (sensorName == sensorData.device)
                    {
                        vibration.SetActive(false);
                        energy.SetActive(true);

                        /*SetEnergyData(_e2ChartGraphPhase1, _e2ChartDataGraphPhase1, sensorData.data.phase_1_voltage, sensorData.data.phase_1_current, sensorData.data.phase_1_power);
                        SetEnergyData(_e2ChartGraphPhase2, _e2ChartDataGraphPhase2, sensorData.data.phase_2_voltage, sensorData.data.phase_2_current, sensorData.data.phase_2_power);
                        SetEnergyData(_e2ChartGraphPhase3, _e2ChartDataGraphPhase3, sensorData.data.phase_3_voltage, sensorData.data.phase_3_current, sensorData.data.phase_3_power);*/
                        SetEnergyData(_e2ChartGraphPhase1, _e2ChartDataGraphPhase1, sensorData.data.phase_1_voltage, sensorData.data.phase_2_voltage, sensorData.data.phase_3_voltage);
                        SetEnergyData(_e2ChartGraphPhase2, _e2ChartDataGraphPhase2, sensorData.data.phase_1_current, sensorData.data.phase_2_current, sensorData.data.phase_3_current);
                        SetEnergyData(_e2ChartGraphPhase3, _e2ChartDataGraphPhase3, sensorData.data.phase_1_power, sensorData.data.phase_2_power, sensorData.data.phase_3_power);
                        SetEnergyData(_e2ChartGraphTotal, _e2ChartDataGraphTotal, sensorData.data.total_power, sensorData.data.energy_consumption);
                    }
                }
            }

        }

        #region Vibration Sensor Data

        private void SetData(E2Chart e2Chart, E2ChartData e2ChartData, float value_)
        {
            if (e2ChartData.series[0].dataY.Count >= _max)
            {
                e2ChartData.series[0].dataY.Clear();
            }

            e2ChartData.series[0].dataY.Add(value_);
            e2Chart.UpdateChart();
        }

        #endregion

        #region Energy Sensor Data

        private void SetEnergyData(E2Chart e2Chart, E2ChartData e2ChartData, float value_, float value1_)
        {
            if (e2ChartData.series[0].dataY.Count >= _max)
            {
                e2ChartData.series[0].dataY.Clear();
                e2ChartData.series[1].dataY.Clear();
            }

            e2ChartData.series[0].dataY.Add(value_);
            e2ChartData.series[1].dataY.Add(value1_);
            e2Chart.UpdateChart();
        }

        private void SetEnergyData(E2Chart e2Chart, E2ChartData e2ChartData, float value_, float value1_, float value2_)
        {
            if (e2ChartData.series[0].dataY.Count >= _max)
            {
                e2ChartData.series[0].dataY.Clear();
                e2ChartData.series[1].dataY.Clear();
                e2ChartData.series[2].dataY.Clear();
            }

            e2ChartData.series[0].dataY.Add(value_);
            e2ChartData.series[1].dataY.Add(value1_);
            e2ChartData.series[2].dataY.Add(value2_);
            e2Chart.UpdateChart();
        }

        #endregion
    }
}
