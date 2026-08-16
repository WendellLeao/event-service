using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using WendellLeao.ServiceLocator;

namespace WendellLeao.Events
{
    /// <summary>
    /// The EventService provides the abstraction <see cref="IEventService"/> to dispatch, add or remove an event anywhere in the game.
    /// <seealso cref="Locator"/>
    /// </summary>
    [DisallowMultipleComponent]
    public sealed class EventService : MonoBehaviour, IEventService
    {
        private readonly Dictionary<Type, object> _eventsDictionary = new();

        public void AddEventListener<T>(UnityAction<T> listener) where T : GameEvent
        {
            Type type = typeof(T);

            if (_eventsDictionary.TryGetValue(type, out object existingEvent))
            {
                UnityEvent<T> unityEvent = (UnityEvent<T>)existingEvent;

                unityEvent.AddListener(listener);

                return;
            }

            UnityEvent<T> newEvent = new();

            newEvent.AddListener(listener);

            _eventsDictionary.Add(type, newEvent);
        }

        public void RemoveEventListener<T>(UnityAction<T> listener) where T : GameEvent
        {
            Type type = typeof(T);

            if (!_eventsDictionary.TryGetValue(type, out object existingEvent))
            {
                Debug.LogWarning($"There's no listener registered for the event '{type.Name}'!");
                return;
            }

            UnityEvent<T> unityEvent = (UnityEvent<T>)existingEvent;

            unityEvent.RemoveListener(listener);
        }

        public void DispatchEvent<T>(T eventToDispatch) where T : GameEvent
        {
            Type type = typeof(T);

            if (!_eventsDictionary.TryGetValue(type, out object existingEvent))
            {
                Debug.LogWarning($"There's no listener registered for the event '{type.Name}'!");
                return;
            }

            UnityEvent<T> unityEvent = (UnityEvent<T>)existingEvent;

            unityEvent.Invoke(eventToDispatch);
        }

        private void Awake()
        {
            Locator.Register<IEventService>(this);
        }

        private void OnDestroy()
        {
            Locator.Unregister<IEventService>();
        }
    }
}
