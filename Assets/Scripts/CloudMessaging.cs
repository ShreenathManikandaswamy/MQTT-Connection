using System.Collections.Generic;
using Firebase.Extensions;
using UnityEngine;
using Firebase;
using Firebase.Messaging;
using System.Threading.Tasks;
using System;
using Personal.UI;

namespace Personal.CloudMessaging
{
    public class CloudMessaging : MonoBehaviour
    {
        [SerializeField]
        private string topic = "TestTopic";
        [SerializeField]
        private GameObject landingPage;
        [SerializeField]
        private GameObject loginPage;
        [SerializeField]
        private AlarmPageHelper alarmPage;
        [SerializeField]
        private Transform homePage;

        private FirebaseApp app;
        private DependencyStatus dependencyStatus;

        private void Start()
        {
            Initialize();
        }

        private void Initialize()
        {
            Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task => {
                dependencyStatus = task.Result;
                if (dependencyStatus == DependencyStatus.Available)
                {
                    app = FirebaseApp.DefaultInstance;

                    FirebaseMessaging.TokenReceived += OnTokenReceived;
                    FirebaseMessaging.MessageReceived += OnMessageReceived;
                    FirebaseMessaging.SubscribeAsync(topic).ContinueWithOnMainThread(task => {
                        LogTaskCompletion(task, "SubscribeAsync");
                    });

                    FirebaseMessaging.RequestPermissionAsync().ContinueWithOnMainThread(
                    task => {
                        LogTaskCompletion(task, "RequestPermissionAsync");
                    });
                    DebugLog("Firebase cloud messaging ready to use!!");
                }
                else
                {
                    Debug.LogError(String.Format(
                      "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
                }
            });
        }

        private bool LogTaskCompletion(Task task, string operation)
        {
            bool complete = false;
            if (task.IsCanceled)
            {
                DebugLog(operation + " canceled.");
            }
            else if (task.IsFaulted)
            {
                DebugLog(operation + " encounted an error.");
                foreach (Exception exception in task.Exception.Flatten().InnerExceptions)
                {
                    string errorCode = "";
                    FirebaseException firebaseEx = exception as Firebase.FirebaseException;
                    if (firebaseEx != null)
                    {
                        errorCode = String.Format("Error.{0}: ",
                          ((Error)firebaseEx.ErrorCode).ToString());
                    }
                    DebugLog(errorCode + exception.ToString());
                }
            }
            else if (task.IsCompleted)
            {
                DebugLog(operation + " completed");
                complete = true;
            }
            return complete;
        }

        public virtual void OnMessageReceived(object sender, Firebase.Messaging.MessageReceivedEventArgs e)
        {
            DebugLog("Received a new message");
            var notification = e.Message.Notification;
            if (notification != null)
            {
                DebugLog("title: " + notification.Title);
                DebugLog("body: " + notification.Body);
                var android = notification.Android;
                if (android != null)
                {
                    DebugLog("android channel_id: " + android.ChannelId);
                }
            }
            if (e.Message.From.Length > 0)
                DebugLog("from: " + e.Message.From);
            if (e.Message.Link != null)
            {
                DebugLog("link: " + e.Message.Link.ToString());
            }
            if (e.Message.Data.Count > 0)
            {
                DebugLog("data:");
                ReceivedNotification(e);
                foreach (KeyValuePair<string, string> iter in
                         e.Message.Data)
                {
                    DebugLog("  " + iter.Key + ": " + iter.Value);
                }
            }
        }

        public virtual void OnTokenReceived(object sender, Firebase.Messaging.TokenReceivedEventArgs token)
        {
            //DebugLog("Received Registration Token: " + token.Token);
        }

        public void ToggleTokenOnInit()
        {
            bool newValue = !FirebaseMessaging.TokenRegistrationOnInitEnabled;
            FirebaseMessaging.TokenRegistrationOnInitEnabled = newValue;
            DebugLog("Set TokenRegistrationOnInitEnabled to " + newValue);
        }

        protected virtual void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }
        }

        public void OnDestroy()
        {
            FirebaseMessaging.MessageReceived -= OnMessageReceived;
            FirebaseMessaging.TokenReceived -= OnTokenReceived;
        }

        private void DebugLog(string value)
        {
            Debug.Log(value);
        }


        private void ReceivedNotification(MessageReceivedEventArgs e)
        {
            landingPage.SetActive(false);
            loginPage.SetActive(false);
            homePage.gameObject.SetActive(true);
            Dictionary<string, string> dict = new Dictionary<string, string>(e.Message.Data);
            InstantiateAlarmPage(dict, true);
            
        }

        public void InstantiateAlarmPage(Dictionary<string, string> dict, bool store)
        {
            AlarmPageHelper alarmPageInstance = Instantiate(alarmPage, homePage);
            alarmPageInstance.ShowAlarmPage(dict, store);
        }
    }
}
