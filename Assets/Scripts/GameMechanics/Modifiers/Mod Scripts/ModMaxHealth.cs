using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ModMaxHealth", menuName = "Modifiers/MaxHealth")]
public class ModMaxHealth : Modifers
{
    //Changes MaxHealth of entity and changes the entity's current help to the same percentage of max health based on the previous %
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
            modDescription = "Max health is increased by " + System.Math.Round(((modifierStrength-1)*100),2).ToString() + "%";
        }
        else
        {
            modDescription = "Max health is reduced by " + System.Math.Round(((1-modifierStrength)*100),2).ToString() + "%";
        }
        return modDescription;
    }
}
