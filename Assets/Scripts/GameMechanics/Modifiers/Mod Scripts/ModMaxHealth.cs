using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ModMaxHealth", menuName = "Modifiers/MaxHealth")]
public class ModMaxHealth : Modifers
{
    public override void applyMod(Entity target, float modifierStrength)
    {
        float currHealthPercent = target.EntityHealth / target.EntityMaxHealth;
        target.EntityMaxHealth = target.EntityMaxHealth * modifierStrength;
        target.EntityHealth = target.EntityMaxHealth * currHealthPercent;
    }

    public override string generateModDesc(float modifierStrength = 0)
    {
        if (modifierStrength > 1)
        {
            modDescription = "Max health is increased by " + ((modifierStrength-1)*100).ToString() + "%";
        }
        else
        {
            modDescription = "Max health is reduced by " + ((1-modifierStrength)*100).ToString() + "%";
        }
        return modDescription;
    }
}
