using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NimbleConsole
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
    internal sealed class FunctionInfoAttribute : Attribute
    {
        public string Name { get; }
        public string Action { get; }
        public Type[] ParameterTypes { get; }

        public FunctionInfoAttribute(string name, string action, params Type[] parameterTypes)
        {
            Name = name;
            Action = action;
            ParameterTypes = parameterTypes;
        }
    }
}