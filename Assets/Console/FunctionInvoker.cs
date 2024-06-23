using NimbleConsole;
using System;
using System.Reflection;
using UnityEngine;

namespace NimbleConsole
{
    public class FunctionInvoker
    {
        private readonly FunctionCollector functionCollector;

        public FunctionInvoker(FunctionCollector collector)
        {
            functionCollector = collector;
        }

        private void CollectError(string message, string newMessage)
        {
            if (message == string.Empty)
                message = newMessage;
        }

        public string InvokeStaticFunction(string functionName, object[] parameters)
        {
            string message = string.Empty;
            FunctionInfo functionInfo = functionCollector.FindFunctionByName(functionName);

            if (functionInfo != null)
            {
                MethodInfo method = FindMethod(functionInfo);
                if (!method.IsStatic)
                {
                    return "Use InvokeDynamicFunction function for non static functions";
                }
                if (method != null)
                {
                    if (CheckParameters(parameters, functionInfo))
                    {
                        if (method.IsStatic)
                        {
                            //Döndürülen deðeri ekrana yazdýr
                            message = method.Invoke(null, parameters).ToString();
                            Debug.Log("A: " + message);
                        }
                    }
                    else
                    {
                        CollectError(message, $"Parameter mismatch for function {functionName}");
                    }
                }
                else
                {
                    CollectError(message, $"Method not found for function {functionName}");
                }
            }
            else
            {
                CollectError(message, $"Function {functionName} not found");
            }

            return message;
        }

        public string InvokeDynamicFunction(GameObject gameObj, string functionName, object[] parameters)
        {
            string message = string.Empty;
            FunctionInfo functionInfo = functionCollector.FindFunctionByName(functionName);

            if (functionInfo != null)
            {
                MethodInfo method = FindMethod(functionInfo);
                Type type = FindMethodType(functionInfo);

                //Component comp = gameObj.GetComponent(type);
                if (method.IsPrivate)
                {
                    //todo hata mesaj yolla
                    return "Dynamic methods cannot be private or protected";
                }
                //if (comp == null)
                //{
                //    //todo Hata mesajý yolla
                //    Debug.Log($"Cannot find {type} in a {gameObj.name}");
                //    return;
                //}

                if (type.GetMethod(functionName) == null)
                {
                    //todo Hata mesajý yolla
                    return $"Cannot find {method} in a {type}";
                }

                if (method.IsStatic)
                {
                    //todo hata mesajý yolla
                    return "Use InvokeStaticFunction function for static functions";
                }
                if (method != null)
                {
                    if (CheckParameters(parameters, functionInfo))
                    {
                        //object instance = functionCollector.gameObject.GetComponent(typeof(FunctionCollector));
                        //obj.SendMessage(functionName, parameters);

                        //Todo döndürülern deðeri konsola yazdýr
                        method.Invoke(gameObj, parameters);
                    }
                    else
                    {
                        CollectError(message, $"Parameter mismatch for function {functionName}");
                    }
                }
                else
                {
                    CollectError(message, $"Method not found for function {functionName}");
                }
            }
            else
            {
                CollectError(message, $"Function {functionName} not found");
            }

            return message;
        }

        private MethodInfo FindMethod(FunctionInfo functionInfo)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var types = assembly.GetTypes();

            foreach (var type in types)
            {
                var method = type.GetMethod(functionInfo.Name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);

                if (method != null)
                    return method;
            }
            return null;
        }

        private Type FindMethodType(FunctionInfo info)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var types = assembly.GetTypes();

            foreach (var type in types)
            {
                var method = type.GetMethod(info.Name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);

                if (method != null)
                {
                    return type;
                }
            }
            return null;
        }

        private bool CheckParameters(object[] parameters, FunctionInfo functionInfo)
        {
            if (parameters.Length != functionInfo.ParameterTypes.Length)
            {
                Debug.Log("Parameter error");
                return false;
            }

            for (int i = 0; i < parameters.Length; i++)
            {
                if (parameters[i].GetType() != functionInfo.ParameterTypes[i])
                {
                    Debug.Log("Parameter error");
                    return false;
                }
            }

            return true;
        }
    }
}