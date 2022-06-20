using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopdownRPG.Core
{
    public static partial class Simulation
    {
        public abstract class Event : System.IComparable<Event>
        {
            internal float tick;
            public int CompareTo(Event other)
            {
                return tick.CompareTo(other.tick);
            }

            public abstract void Execute();
            public virtual bool Precondition() => true;
            internal virtual void ExecuteEvent()
            {
                if (Precondition())
                    Execute();
            }
            internal virtual void Cleanup()
            {
                Debug.Log("Cleanup Event");
            }
        }
        public abstract class Event<T> : Event where T : Event<T>
        {
            public static System.Action<T> OnExecute;
            internal override void ExecuteEvent()
            {
                if (Precondition())
                {
                    Execute();
                    OnExecute?.Invoke((T)this);
                }
            }
        }
    }
}