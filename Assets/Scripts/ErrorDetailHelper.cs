using TMPro;
using UnityEngine;

namespace Personal.UI
{
    public class ErrorDetailHelper : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI title;
        [SerializeField]
        private TextMeshProUGUI value;

        public void ShowErrorDetail(string titleValue, string valueValue)
        {
            title.text = titleValue;
            value.text = valueValue;
        }
    }
}
