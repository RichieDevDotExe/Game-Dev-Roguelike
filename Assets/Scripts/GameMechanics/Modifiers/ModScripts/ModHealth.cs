using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Modifiers/Health")]
public class ModHealth : Modifers
{


    public override void applyMod(Entity target, float modifierStrength)
    {
        target.EntityHealth = target.EntityHealth + modifierStrength;
    }

    public override string generateModDesc(float modifierStrength = 0)
    {
        if (modifierStrength > 1)
        {
            modDescription = "Take " + modifierStrength.ToString() + " damage";
        }
        else
        {
            modDescription = "Restore " + modifierStrength.ToString() + " health";
        }
        return modDescription;
    }
}
