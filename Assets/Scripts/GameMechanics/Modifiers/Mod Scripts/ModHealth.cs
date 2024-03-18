using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ModHealth", menuName = "Modifiers/Health")]
public class ModHealth : Modifers
{
    Entity entity;

    public override void applyMod(Entity target, float modifierStrength)
    {
        if (modifierStrength < 1)
        {
            target.EntityHealth = target.EntityHealth - (entity.EntityMaxHealth * modifierStrength);
        }
        else
        {
            target.EntityHealth = target.EntityHealth + (entity.EntityMaxHealth * (modifierStrength-1));
        }
            
    }

    public override string generateModDesc(float modifierStrength = 0)
    {
        if (modifierStrength < 1)
        {
            modDescription = "Take " + (entity.EntityHealth * modifierStrength).ToString() + " damage";
        }
        else
        {
            modDescription = "Restore " + (entity.EntityHealth * modifierStrength).ToString() + " health";
        }
        Debug.Log("Gen Desc");
        return modDescription;
    }
}
