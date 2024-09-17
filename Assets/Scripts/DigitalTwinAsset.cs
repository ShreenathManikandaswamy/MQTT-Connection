using E2C;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Personal.UI;

namespace Personal.DigitalTwin
{
    public class DigitalTwinAsset : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI message;
        [SerializeField]
        private GameObject[] canvas;
        [SerializeField]
        private GameObject arCamera;
        [SerializeField]
        private DigitalTwinManager manager;

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

        private bool startLookat = false;
        private int _max = 50;

        private void Start()
        {
            startLookat = true;
            manager.dataEvent.AddListener(DTData);
        }

        private void DTData()
        {
            string data = manager.DTMessage;

            if(data.Contains("vibration"))
            {
                VibrationSensorData sensorData = JsonUtility.FromJson<VibrationSensorData>(data);

                SetupVibrationSensorDetails(sensorData);
            }else
            {
                EnergySensorData sensorData = JsonUtility.FromJson<EnergySensorData>(data);

                SetupEnergySensorDetails(sensorData);
            }
        }

        #region Vibration chart details

        public void SetupVibrationSensorDetails(VibrationSensorData sensorData)
        {
            if (sensorData.sensor == "vibration_sensor_1")
            {
                SetData(_VibrationSensors[0]._e2ChartGraphVib, _VibrationSensors[0]._e2ChartDataGraphVib, sensorData.data.vibration_intensity);
                SetData(_VibrationSensors[0]._e2ChartGraphTemp, _VibrationSensors[0]._e2ChartDataGraphTemp, sensorData.data.temperature);
                SetData(_VibrationSensors[0]._e2ChartGraphHum, _VibrationSensors[0]._e2ChartDataGraphHum, sensorData.data.humidity);
                SetData(_VibrationSensors[0]._e2ChartGraphRPM, _VibrationSensors[0]._e2ChartDataGraphRPM, sensorData.data.rpm);
            }
            else
            {
                SetData(_VibrationSensors[1]._e2ChartGraphVib, _VibrationSensors[1]._e2ChartDataGraphVib, sensorData.data.vibration_intensity);
                SetData(_VibrationSensors[1]._e2ChartGraphTemp, _VibrationSensors[1]._e2ChartDataGraphTemp, sensorData.data.temperature);
                SetData(_VibrationSensors[1]._e2ChartGraphHum, _VibrationSensors[1]._e2ChartDataGraphHum, sensorData.data.humidity);
                SetData(_VibrationSensors[1]._e2ChartGraphRPM, _VibrationSensors[1]._e2ChartDataGraphRPM, sensorData.data.rpm);
            }
        }

        void SetData(E2Chart e2Chart, E2ChartData e2ChartData, float value_)
        {
            if (e2ChartData.series[0].dataY.Count >= _max)
            {
                e2ChartData.series[0].dataY.Clear();
            }
            
            e2ChartData.series[0].dataY.Add(value_);
            e2Chart.UpdateChart();
        }

        #endregion

        #region Energy Chart details

        public void SetupEnergySensorDetails(EnergySensorData sensorData)
        {
            /*SetEnergyData(_e2ChartGraphPhase1, _e2ChartDataGraphPhase1, sensorData.data.phase_1_voltage, sensorData.data.phase_1_current, sensorData.data.phase_1_power);
            SetEnergyData(_e2ChartGraphPhase2, _e2ChartDataGraphPhase2, sensorData.data.phase_2_voltage, sensorData.data.phase_2_current, sensorData.data.phase_2_power);
            SetEnergyData(_e2ChartGraphPhase3, _e2ChartDataGraphPhase3, sensorData.data.phase_3_voltage, sensorData.data.phase_3_current, sensorData.data.phase_3_power);*/
            SetEnergyData(_e2ChartGraphPhase1, _e2ChartDataGraphPhase1, sensorData.data.phase_1_voltage, sensorData.data.phase_2_voltage, sensorData.data.phase_3_voltage);
            SetEnergyData(_e2ChartGraphPhase2, _e2ChartDataGraphPhase2, sensorData.data.phase_1_current, sensorData.data.phase_2_current, sensorData.data.phase_3_current);
            SetEnergyData(_e2ChartGraphPhase3, _e2ChartDataGraphPhase3, sensorData.data.phase_1_power, sensorData.data.phase_2_power, sensorData.data.phase_3_power);
            SetEnergyData(_e2ChartGraphTotal, _e2ChartDataGraphTotal, sensorData.data.total_power, sensorData.data.energy_consumption); ;
        }

        void SetEnergyData(E2Chart e2Chart, E2ChartData e2ChartData, float value_, float value1_)
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

        void SetEnergyData(E2Chart e2Chart, E2ChartData e2ChartData, float value_, float value1_, float value2_)
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

        private void Update()
        {
            if (startLookat)
            {
                foreach(GameObject go in canvas)
                    go.transform.LookAt(arCamera.transform);
            }
        }
    }
}
