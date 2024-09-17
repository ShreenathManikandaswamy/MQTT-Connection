using TMPro;
using UnityEngine;

namespace Personal.UI
{
    public class StepsDataHelper : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI stepNumber;
        [SerializeField]
        private TextMeshProUGUI stepValue;

        public void ShowStep(string number, string value)
        {
            stepNumber.text = number;
            stepValue.text = value;
        }
    }
}
