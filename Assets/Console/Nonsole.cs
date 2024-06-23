using NimbleConsole;
using System.Reflection;
using TMPro;
using UnityEditor.Timeline.Actions;
using UnityEngine;

/// <summary>
/// Console Application manager
/// </summary>
public class Nonsole : MonoBehaviour
{
    [SerializeField] private GameObject textArea;
    [SerializeField] private GameObject inputField;

    private static TextMeshProUGUI textAreaGUI;

    private static FunctionInvoker invoker;
    private static FunctionCollector collector;

    private void Awake()
    {
        collector = new FunctionCollector();
        invoker = new FunctionInvoker(collector);

        textAreaGUI = textArea.GetComponent<TextMeshProUGUI>();
    }

    public static void Log(string message)
    {
        Debug.Log("NONSOLE: " + message);
        textAreaGUI.text += "\n" + message;
    }

    public static void Execute(string name, object[] parameters, BindingFlags flag, GameObject obj = null)
    {
        string msg;

        if (flag.Equals(BindingFlags.Static))
        {
            msg = invoker.InvokeStaticFunction(name, parameters);
        }
        else
        {
            msg = invoker.InvokeDynamicFunction(obj, name, parameters);
        }

        if (msg != null)
            Log(msg);
        else
            Log("Function does not return anything");
    }
}