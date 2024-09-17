using System.Collections.Generic;
using UnityEngine;
using uPLibrary.Networking.M2Mqtt.Messages;
using M2MqttUnity;

namespace Personal.DigitalTwin
{
    public class DigitalTwinManager : M2MqttUnityClient
    {
        [Header("Custom Variables")]
        public List<string> topic;

        private List<string> eventMessages = new List<string>();

        #region Connection/Subscription

        protected override void OnConnecting()
        {
            base.OnConnecting();
            AddUiMessage("Connecting..", false);
        }

        protected override void OnConnected()
        {
            base.OnConnected();
            AddUiMessage("Connected!!", false);
        }

        protected override void SubscribeTopics()
        {
            for (int i = 0; i < topic.Count; i++)
            {
                client.Subscribe(new string[] { topic[i] }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
            }
        }

        protected override void UnsubscribeTopics()
        {
            for (int i = 0; i < topic.Count; i++)
            {
                client.Unsubscribe(new string[] { topic[i] });
            }
        }

        protected override void OnConnectionFailed(string errorMessage)
        {
            AddUiMessage("CONNECTION FAILED! " + errorMessage, false);
        }

        protected override void OnDisconnected()
        {
            AddUiMessage("Disconnected.", false);
        }

        protected override void OnConnectionLost()
        {
            AddUiMessage("CONNECTION LOST!", false);
        }

        #endregion

        protected override void Start()
        {
            AddUiMessage("Ready.", false);
            base.Start();
        }
    }
}
