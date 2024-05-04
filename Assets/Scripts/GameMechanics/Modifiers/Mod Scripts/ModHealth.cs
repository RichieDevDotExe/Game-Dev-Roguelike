using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ModHealth", menuName = "Modifiers/Health")]
public class ModHealth : Modifers
{
    public override void applyMod(Entity target, float modifierStrength)
    {
        if (modifierStrength < 1)
        {
            target.EntityHealth = target.EntityHealth - (target.EntityMaxHealth * modifierStrength);
        }
        else
        {
            target.EntityHealth = target.EntityHealth + (target.EntityMaxHealth * (modifierStrength-1));
        }
            
    }

    public override string generateModDesc(float modifierStrength = 0)
    {
        if (modifierStrength < 1)
        {
            modDescription = "Take " + (modifierStrength*100).ToString() + "% damage";
        }
        else
        {
            modDescription = "Restore " + ((modifierStrength-1) * 100).ToString() + "% health";
        }
        Debug.Log("Gen Desc");
        return modDescription;
    }
}
