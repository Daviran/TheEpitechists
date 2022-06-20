using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopdownRPG.Core
{
    public static partial class Simulation
    {
        static HeapQueue<Event> eventQueue = new HeapQueue<Event>();
        static Dictionary<System.Type, Stack<Event>> eventPools = new Dictionary<System.Type, Stack<Event>>();

        static public T New<T>() where T : Event, new()
        {
            Stack<Event> pool;
            if (!eventPools.TryGetValue(typeof(T), out pool))
            {
                pool = new Stack<Event>(4);
                pool.Push(new T());
                eventPools[typeof(T)] = pool;
            }
            if (pool.Count > 0)
                return (T)pool.Pop();
            else
                return new T();
        }

        public static void Clear()
        {
            eventQueue.Clear();
        }

        static public T Schedule<T>(float tick = 0) where T : Event, new()
        {
            var ev = New<T>();
            ev.tick = Time.time + tick;
            eventQueue.Push(ev);
            return ev;
        }

        static public T GetModel<T>() where T : class, new()
        {
            return InstanceRegister<T>.instance;
        }

        static public int Tick()
        {
            var time = Time.time;
            var executedEventCount = 0;
            while (eventQueue.Count > 0 && eventQueue.Peek().tick <= time)
            {
                var ev = eventQueue.Pop();
                var tick = ev.tick;
                ev.ExecuteEvent();
                if(ev.tick > tick)
                {

                }
                else
                {
                    ev.Cleanup();
                    try
                    {
                        eventPools[ev.GetType()].Push(ev);
                    }
                    catch (KeyNotFoundException)
                    {

                        throw;
                    }
                }
                executedEventCount++;
            }
            return eventQueue.Count;
        }
    }
}
