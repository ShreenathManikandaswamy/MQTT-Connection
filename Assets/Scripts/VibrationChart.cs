using System;
using E2C;

namespace Personal.UI
{
    [Serializable]
    public class VibrationChart
    {
        public E2Chart _e2ChartGraphVib;
        public E2ChartData _e2ChartDataGraphVib;
        public E2ChartOptions _e2ChartOptions;

        public E2Chart _e2ChartGraphTemp;
        public E2ChartData _e2ChartDataGraphTemp;

        public E2Chart _e2ChartGraphHum;
        public E2ChartData _e2ChartDataGraphHum;

        public E2Chart _e2ChartGraphRPM;
        public E2ChartData _e2ChartDataGraphRPM;
    }
}
