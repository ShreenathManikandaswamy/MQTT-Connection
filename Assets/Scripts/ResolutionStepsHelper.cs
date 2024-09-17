using UnityEngine;

namespace Personal.UI
{
    public class ResolutionStepsHelper : MonoBehaviour
    {
        [SerializeField]
        private Transform stepsParent;
        [SerializeField]
        private StepsDataHelper stepsDataPrefab;

        private ARSceneUiManager arUiManager;

        public void ShowSteps(string[] steps)
        {
            for(int i = 0; i < steps.Length;  i++)
            {
                StepsDataHelper instance = Instantiate(stepsDataPrefab, stepsParent);
                instance.ShowStep("Step " + (i + 1) + " : ", steps[i]);
            }
        }

        public void CloseSteps()
        {
            Destroy(this.gameObject);
        }

        public void StartResolutionAR()
        {
            arUiManager = GameObject.FindObjectOfType<ARSceneUiManager>();
            arUiManager.StartResolution();
        }
    }
}
