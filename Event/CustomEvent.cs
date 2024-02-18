using System;

namespace My.Event
{
    public interface IEventTrigger<T1, T2, T3, T4>
    {
        void Invoke(T1 e1, T2 e2, T3 e3, T4 e4);
    }
    public interface IEventTrigger<T1, T2, T3>
    {
        void Invoke(T1 e1, T2 e2, T3 e3);
    }

    public interface IEventTrigger<T1, T2>
    {
        void Invoke(T1 e1, T2 e2);
    }

    public interface IEventTrigger<T1>
    {
        void Invoke(T1 e1);
    }

    public interface IEventTrigger
    {
        void Invoke();
    }

    public interface IEventSubscription<T1, T2, T3, T4>
    {
        void AddListener(Action<T1, T2, T3, T4> listener, bool invokeFirst = true);
        void RemoveListener(Action<T1, T2, T3, T4> listener);
        void ClearListener();

    }

    public interface IEventSubscription<T1, T2, T3>
    {
        void AddListener(Action<T1, T2, T3> listener, bool invokeFirst = true);
        void RemoveListener(Action<T1, T2, T3> listener);
        void ClearListener();

    }

    public interface IEventSubscription<T1, T2>
    {
        void AddListener(Action<T1, T2> listener, bool invokeFirst = true);
        void RemoveListener(Action<T1, T2> listener);
        void ClearListener();
    }

    public interface IEventSubscription<T1>
    {
        void AddListener(Action<T1> listener, bool invokeFirst = true);
        void RemoveListener(Action<T1> listener);
        void ClearListener();
    }

    public interface IEventSubscription
    {
        void AddListener(Action listener, bool invokeFirst = true);
        void RemoveListener(Action listener);
        void ClearListener();
    }

    public class CustomEvent<T1, T2, T3, T4> : IEventTrigger<T1, T2, T3, T4>, IEventSubscription<T1, T2, T3, T4>
    {
        private event Action<T1, T2, T3, T4> OnEventFirst;
        private event Action<T1, T2, T3, T4> OnEventLast;

        public void AddListener(Action<T1, T2, T3, T4> listener, bool invokeFirst = true)
        {
            if (invokeFirst)
            {
                OnEventFirst += listener;
            }
            else
            {
                OnEventLast += listener;
            }
        }
        public void RemoveListener(Action<T1, T2, T3, T4> listener)
        {
            OnEventFirst -= listener;
            OnEventLast -= listener;
        }

        public void ClearListener()
        {
            OnEventFirst = null;
            OnEventLast = null;
        }

        public void Invoke(T1 e1, T2 e2, T3 e3, T4 e4)
        {
            OnEventFirst?.Invoke(e1, e2, e3, e4);
            OnEventLast?.Invoke(e1, e2, e3, e4);
        }
    }


    public class CustomEvent<T1, T2, T3> : IEventTrigger<T1, T2, T3>, IEventSubscription<T1, T2, T3>
    {
        private event Action<T1, T2, T3> OnEventFirst;
        private event Action<T1, T2, T3> OnEventLast;

        public void AddListener(Action<T1, T2, T3> listener, bool invokeFirst = true)
        {
            if (invokeFirst)
            {
                OnEventFirst += listener;
            }
            else
            {
                OnEventLast += listener;
            }
        }
        public void RemoveListener(Action<T1, T2, T3> listener)
        {
            OnEventFirst -= listener;
            OnEventLast -= listener;
        }

        public void ClearListener()
        {
            OnEventFirst = null;
            OnEventLast = null;
        }

        public void Invoke(T1 e1, T2 e2, T3 e3)
        {
            OnEventFirst?.Invoke(e1, e2, e3);
            OnEventLast?.Invoke(e1, e2, e3);
        }
    }

    public class CustomEvent<T1, T2> : IEventTrigger<T1, T2>, IEventSubscription<T1, T2>
    {
        private event Action<T1, T2> OnEventFirst;
        private event Action<T1, T2> OnEventLast;

        public void AddListener(Action<T1, T2> listener, bool invokeFirst = true)
        {
            if (invokeFirst)
            {
                OnEventFirst += listener;
            }
            else
            {
                OnEventLast += listener;
            }
        }
        public void RemoveListener(Action<T1, T2> listener)
        {
            OnEventFirst -= listener;
            OnEventLast -= listener;
        }

        public void ClearListener()
        {
            OnEventFirst = null;
            OnEventLast = null;
        }

        public void Invoke(T1 e1, T2 e2)
        {
            OnEventFirst?.Invoke(e1, e2);
            OnEventLast?.Invoke(e1, e2);
        }
    }

    public class CustomEvent<T1> : IEventTrigger<T1>, IEventSubscription<T1>
    {
        private event Action<T1> OnEventFirst;
        private event Action<T1> OnEventLast;

        public void AddListener(Action<T1> listener, bool invokeFirst = true)
        {
            if (invokeFirst)
            {
                OnEventFirst += listener;
            }
            else
            {
                OnEventLast += listener;
            }
        }
        public void RemoveListener(Action<T1> listener)
        {
            OnEventFirst -= listener;
            OnEventLast -= listener;
        }

        public void ClearListener()
        {
            OnEventFirst = null;
            OnEventLast = null;
        }

        public void Invoke(T1 e1)
        {
            OnEventFirst?.Invoke(e1);
            OnEventLast?.Invoke(e1);
        }
    }

    public class CustomEvent : IEventTrigger, IEventSubscription
    {
        private event Action OnEventFirst;
        private event Action OnEventLast;

        public void AddListener(Action listener, bool invokeFirst = true)
        {
            if (invokeFirst)
            {
                OnEventFirst += listener;
            }
            else
            {
                OnEventLast += listener;
            }
        }
        public void RemoveListener(Action listener)
        {
            OnEventFirst -= listener;
            OnEventLast -= listener;
        }

        public void ClearListener()
        {
            OnEventFirst = null;
            OnEventLast = null;
        }

        public void Invoke()
        {
            OnEventFirst?.Invoke();
            OnEventLast?.Invoke();
        }
    }
}