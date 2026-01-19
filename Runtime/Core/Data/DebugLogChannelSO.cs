using System.IO;
using System.Runtime.CompilerServices;
using Core.Structs;
using UnityEngine;

namespace Birnchen.EventBus.Core.Data
{
    [CreateAssetMenu(menuName = "Birnchen/Events/Debug Log Channel")]
    public class DebugLogChannelSO : EventChannelSO<LogEvent>
    {
        // Die statische Instanz für Non-MonoBehaviours
        private static DebugLogChannelSO _instance;

        private void OnEnable()
        {
            // Wenn das Spiel startet (oder das Asset geladen wird), setzen wir die Instanz
            if (_instance == null)
                _instance = this;
        }
        
        public static void Log(string message, LogType type = LogType.Log, object sender = null, [CallerFilePath] string sourceFilePath = "")
        {
            if (_instance == null)
            {
                // Versuch, es aus Resources zu laden (Optional, wenn es im Resources Ordner liegt)
                _instance = Resources.Load<DebugLogChannelSO>($"System/DebugLogChannel");
            
                if(_instance == null)
                {
                    Debug.LogWarning($"<color=#FF5555>[DebugChannel Disconnected]</color> {message} \n");
                    return;
                }
            }

            string context;

            // Prio 1: Wenn 'this' explizit übergeben wurde, nutzen wir das (ist am genauesten)
            if (sender != null)
            {
                context = sender.GetType().Name;
            }
            // Prio 2: Wenn nicht, extrahieren wir den Dateinamen aus dem Pfad
            else if (!string.IsNullOrEmpty(sourceFilePath))
            {
                // Aus "C:/Projects/Assets/Scripts/Combat/DamageCalculator.cs" 
                // wird "DamageCalculator"
                context = Path.GetFileNameWithoutExtension(sourceFilePath);
            }
            // Fallback
            else
            {
                context = "Global";
            }

            LogEvent payload = new LogEvent(message, type, context);
            _instance.Raise(payload);
        }
    }
}