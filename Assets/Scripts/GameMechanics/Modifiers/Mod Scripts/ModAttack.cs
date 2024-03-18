using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ModAttack", menuName = "Modifiers/Attack")]
public class ModAttack : Modifers
{
    public override void applyMod(Entity target, float modifierStrength)
    {
        target.EntityDamage = target.EntityDamage * modifierStrength;
    }

    public override string generateModDesc(float modifierStrength = 0)
    {
        if (modifierStrength > 1)
        {
            modDescription = "Attack is increased by " + modifierStrength.ToString() + "%";
        }
        else
        {
            modDescription = "Attack is reduced by " + modifierStrength.ToString()+"%";
        }
        return modDescription;
    }

}
