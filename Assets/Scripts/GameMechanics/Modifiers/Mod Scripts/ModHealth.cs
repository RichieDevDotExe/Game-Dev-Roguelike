using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ModHealth", menuName = "Modifiers/Health")]
public class ModHealth : Modifers
{
    //Changes health value based on a % of max health
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
            modDescription = "Take " + System.Math.Round(((1-modifierStrength)*100),2).ToString() + "% damage";
        }
        else
        {
            modDescription = "Restore " + System.Math.Round(((modifierStrength-1) * 100),2).ToString() + "% health";
        }
        Debug.Log("Gen Desc");
        return modDescription;
    }
}
