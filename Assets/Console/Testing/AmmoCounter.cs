using NimbleConsole;
using UnityEngine;

public class AmmoCounter : MonoBehaviour
{
    private static int ammoAmount = 10;

    [FunctionInfo("AddAmmo", "Adding ammo", typeof(int))]
    public string AddAmmo(int amount)
    {
        ammoAmount += amount;

        return "Ammo Added: " + amount;
    }
}