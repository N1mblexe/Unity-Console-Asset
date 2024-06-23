using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class NonsoleInputManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField field;

    private string inputText = string.Empty;

    private void Awake()
    {
        field = GetComponent<TMP_InputField>();
    }

    public void SetInput(string inputText)
    {
        this.inputText = inputText;
    }

    private string CheckSyntax(string msg)
    {
        if (msg.Contains("  "))
            return "Multiple space error!!";

        if (msg.Split('.').Length > 1)
            return "Multiple dot error";

        return string.Empty;
    }

    public void HandleInput()
    {
        string text = field.text;

        string checker = CheckSyntax(text);

        if (checker != string.Empty)
        {
            Debug.Log(checker);
            return;
        }
        List<string> splittedText = text.Split(" ").ToList<string>();

        //Non-Static
        if (splittedText[0].Contains("."))
        {
            string[] temp = splittedText[0].Split(".");

            string objName = temp[0];
            string funcName = temp[1];

            GameObject obj = GameObject.Find(objName);
            splittedText.RemoveAt(0);

            Nonsole.Execute(funcName, splittedText.Cast<object>().ToArray(), System.Reflection.BindingFlags.Default, obj);
        }
        //Static
        else
        {
            string funcName = splittedText[0];
            splittedText.RemoveAt(0);

            Nonsole.Execute(funcName, splittedText.Cast<object>().ToArray(), System.Reflection.BindingFlags.Static);
        }
        field.text = " ";

        //TODO Send the command to the Nonsole but first check for parameters or this kinda stuff
    }
}