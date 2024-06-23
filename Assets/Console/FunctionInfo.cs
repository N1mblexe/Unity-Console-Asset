using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NimbleConsole
{
    public class FunctionInfo
    {
        public string Name { get; }
        public string Action { get; }
        public Type[] ParameterTypes { get; }

        public FunctionInfo(string name, string action, Type[] parameterTypes)
        {
            Name = name;
            Action = action;
            ParameterTypes = parameterTypes;
        }
    }
}