using TMPro;
using UnityEngine;

namespace Personal.UI
{
    public class ARResolutionSteps : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI currentStep;

        private string[] steps;
        private int count = 0;
        private ARSceneUiManager manager;

        private void Start()
        {
            manager = FindObjectOfType<ARSceneUiManager>();
        }

        public void SetupSteps(string[] stepsValue)
        {
            steps = stepsValue;
            currentStep.text = steps[count];
        }

        public void NextStep()
        {
            if(count < steps.Length -1)
            {
                count += 1;
                currentStep.text = steps[count];
            }else
            {
                manager.ShowHideUIElements();
                this.gameObject.SetActive(false);
            }
        }

        public void PreviosStep()
        {
            if (count > 0)
            {
                count -= 1;
                currentStep.text = steps[count];
            }
        }
    }
}
