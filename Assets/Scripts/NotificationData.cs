using System;
using System.Collections.Generic;

namespace Personal.Utilities
{
    [Serializable]
    public class NotificationData
    {
        public string title;
        public string message;
        public string errorCode;
        public string deviceName;
        public string deviceTag;
        public string errorData;
    }

    [Serializable]
    public class NotificationsHistory
    {
        public List<NotificationData> notification;
    }
}
