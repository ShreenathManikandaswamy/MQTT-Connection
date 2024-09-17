using Personal.DigitalTwin;
using TMPro;
using UnityEngine;

namespace Personal.UI
{
    public class ARSceneUiManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject[] arSceneUiElements;
        [SerializeField]
        private TextMeshProUGUI buttonText;
        [SerializeField]
        private DigitalTwinAlarm[] alarmOptions;
        [SerializeField]
        private ResolutionStepsHelper resolutionStepsPrefab;
        [SerializeField]
        private Transform resolutionParent;
        [SerializeField]
        private GameObject resolveButton;
        [SerializeField]
        private ARResolutionSteps resolutionSteps;
        [SerializeField]
        private GameObject user;
        [SerializeField]
        private GameObject target;

        private string errorCode;
        private bool isShowing = true;
        private bool hasErrorCode = false;
        private ResolutionStepsHelper instance;

        private void Start()
        {
            CheckResolutionStatus();
            buttonText.text = "Hide Data";
        }

        private void CheckResolutionStatus()
        {
            if (PlayerPrefs.HasKey("errorCode"))
            {
                errorCode = PlayerPrefs.GetString("errorCode");
                hasErrorCode = true;
            }
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.R))
            {
                errorCode = "ER005";
                hasErrorCode = true;
            }

            if(hasErrorCode)
            {
                if (Vector3.Distance(user.transform.position, target.transform.position) < 3)
                {
                    resolveButton.SetActive(true);
                }
                else
                    resolveButton.SetActive(false);
            }
        }

        public void ShowHideUIElements()
        {
            if(isShowing)
            {
                buttonText.text = "Show Data";
            }else
            {
                buttonText.text = "Hide Data";
            }

            foreach (GameObject go in arSceneUiElements)
            {
                go.SetActive(!isShowing);
            }
            isShowing = !isShowing;
        }

        public void ShowResolutionSteps()
        {
            instance = Instantiate(resolutionStepsPrefab, resolutionParent);

            foreach(DigitalTwinAlarm alarm in alarmOptions)
            {
                if(alarm.errorCode == errorCode)
                    instance.ShowSteps(alarm.resolutionSteps);
            }
        }

        public void StartResolution()
        {
            if(isShowing)
            {
                ShowHideUIElements();
            }

            foreach (DigitalTwinAlarm alarm in alarmOptions)
            {
                if (alarm.errorCode == errorCode)
                {
                    Destroy(instance.gameObject);
                    resolutionSteps.gameObject.SetActive(true);
                    resolutionSteps.SetupSteps(alarm.resolutionSteps);
                }
            }
        }
    }
}
