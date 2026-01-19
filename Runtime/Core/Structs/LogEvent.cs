using System;
using Birnchen.EventBus.Core.Utilities;
using UnityEngine;

namespace Core.Structs
{
    [Serializable]
    public struct LogEvent : IEvent
    {
        public string Message;
        public LogType Type; // Error, Warning, Log
        public DateTime Timestamp;
        public string Context; // Z.B. Klassenname

        public LogEvent(string message, LogType type, string context = "")
        {
            Message = message;
            Type = type;
            Timestamp = DateTime.Now;
            Context = context;
        }
    }
}