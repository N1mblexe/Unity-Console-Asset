using System; 
using System.Diagnostics;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    /// <summary>
    /// Calls the parametre function
    /// </summary>
    /// <param name="function">Function to test</param>
    /// <returns>Time it takes to run a function (Type: TimeSpan)</returns>
    public static TimeSpan Measure(Action function)
    {
        Stopwatch sw = new Stopwatch();

        sw.Start();

        function();

        sw.Stop();

        TimeSpan elapsedTime = sw.Elapsed;

        sw.Reset();

        return elapsedTime;
    }
}
