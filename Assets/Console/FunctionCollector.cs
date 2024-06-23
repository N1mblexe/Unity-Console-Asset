using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace NimbleConsole
{
    public class FunctionCollector : MonoBehaviour
    {
        private Dictionary<string, FunctionInfo> functionDictionary = new Dictionary<string, FunctionInfo>();

        private void Awake()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var types = assembly.GetTypes();
            foreach (var type in types)
            {
                CollectFunctions(type);
                //Debug.Log(type.FullName);
            }
        }

        private void CollectFunctions(Type type)
        {
            MethodInfo[] methods = type.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);

            foreach (MethodInfo method in methods)
            {
                FunctionInfoAttribute attribute = (FunctionInfoAttribute)method.GetCustomAttribute(typeof(FunctionInfoAttribute), false);
                //Debug.Log("\t : " + method);

                if (attribute != null)
                {
                    FunctionInfo functionInfo = new FunctionInfo(attribute.Name, attribute.Action, attribute.ParameterTypes);
                    functionDictionary[attribute.Name] = functionInfo;

                    Debug.Log("Function Added : " + functionInfo.Name);
                }
            }
        }

        public FunctionInfo FindFunctionByName(string functionName)
        {
            if (functionDictionary.TryGetValue(functionName, out var functionInfo))
            {
                return functionInfo;
            }

            return null;
        }
    }
}