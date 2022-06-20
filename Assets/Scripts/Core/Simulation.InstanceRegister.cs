using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopdownRPG.Core
{
    public static partial class Simulation
    {
        static class InstanceRegister<T> where T : class, new()
        {
            public static T instance = new T();
        }
    }
}