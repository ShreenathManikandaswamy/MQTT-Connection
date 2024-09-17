using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

namespace Personal.Wayfinder
{
    public class WayfinderManager : MonoBehaviour
    {
        [SerializeField]
        private List<Targets> targetObjects = new List<Targets>();
        [SerializeField]
        private LineRenderer line;
        [SerializeField]
        private GameObject user;
        [SerializeField]
        private TextMeshProUGUI distanceToTarget;

        private NavMeshPath path;
        private Vector3 targetPosition = Vector3.zero;
        private bool showPath = true;

        private void Start()
        {
            SetCurrentNavigationTarget();
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
            path = new NavMeshPath();
            line.enabled = showPath;
        }

        private void Update()
        {
            if(showPath && targetPosition != Vector3.zero)
            {
                NavMesh.CalculatePath(user.transform.position, targetPosition, NavMesh.AllAreas, path);
                line.positionCount = path.corners.Length;
                line.SetPositions(path.corners);

                distanceToTarget.text = "Distance to Target : " +  Vector3.Distance(user.transform.position, targetPosition).ToString("F1");
            }
        }

        public void SetCurrentNavigationTarget()
        {
            targetPosition = Vector3.zero;
            string selectedText = PlayerPrefs.GetString("currentTarget");
            Targets currentTarget = targetObjects.Find(x => x.name.Equals(selectedText));
            if(currentTarget != null)
            {
                targetPosition = currentTarget.targetPosition.transform.position;
            }
        }

        public void ShowPath()
        {
            showPath = !showPath;
            line.enabled = showPath;
        }

        public void BackToHome()
        {
            PlayerPrefs.DeleteKey("errorCode");
            PlayerPrefs.SetInt("back", 1);
            SceneManager.LoadScene(0);
        }
    }
}