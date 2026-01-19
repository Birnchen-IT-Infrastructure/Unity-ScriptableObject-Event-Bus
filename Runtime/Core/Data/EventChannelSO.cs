using System.Collections.Generic;
using Birnchen.EventBus.Core.Utilities;
using UnityEngine;

namespace Birnchen.EventBus.Core.Data
{
    // T muss ein struct sein, das IEvent implementiert (für Performance und Sicherheit)
    public class EventChannelSO<T> : ScriptableObject where T: struct, IEvent
    {
        [TextArea] public string Description;
        
        // Wir speichern HashSet für Performance beim Lookup
        private readonly HashSet<IEventBinding<T>> _bindings = new();
        
        public void Register(EventBinding<T> binding) => _bindings.Add(binding);
        public void Deregister(EventBinding<T> binding) => _bindings.Remove(binding);

        public void Raise(T eventData)
        {
            // Erstellt eine temporäre Kopie für die Iteration -> Sicher gegen Deregister während Call
            // Da HashSet nur Referenzen speichert, ist das sehr günstig.
            foreach (IEventBinding<T> binding in new HashSet<IEventBinding<T>>(_bindings))
            {
                binding.OnEvent.Invoke(eventData);
                binding.OnEventNoArgs.Invoke();
            }
            
            #if UNITY_EDITOR
            // Kleines Logging für Debugging
            Debug.Log($"[Event] {name} raised: {eventData}");
            #endif
        }
    }
}