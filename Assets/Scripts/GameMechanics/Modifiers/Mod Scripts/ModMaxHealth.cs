using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ModMaxHealth", menuName = "Modifiers/MaxHealth")]
public class ModMaxHealth : Modifers
{
    public override void applyMod(Entity target, float modifierStrength)
    {
        target.EntityMaxHealth = target.EntityMaxHealth * modifierStrength;
    }

    public override string generateModDesc(float modifierStrength = 0)
    {
        if (modifierStrength > 1)
        {
            modDescription = "Max health is increased by " + modifierStrength.ToString() + "%";
        }
        else
        {
            modDescription = "Max health is reduced by " + modifierStrength.ToString() + "%";
        }
        return modDescription;
    }
}
