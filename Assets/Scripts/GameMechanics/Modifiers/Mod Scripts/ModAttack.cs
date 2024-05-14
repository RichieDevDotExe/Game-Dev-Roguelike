using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ModAttack", menuName = "Modifiers/Attack")]
public class ModAttack : Modifers
{
    //Modifies Entity's attack value

    public override void applyMod(Entity target, float modifierStrength)
    {
        target.EntityDamage = target.EntityDamage * modifierStrength;
    }

    public override string generateModDesc(float modifierStrength = 0)
    {
        if (modifierStrength > 1)
        {
            modDescription = "Attack is increased by " + System.Math.Round(((modifierStrength - 1) * 100),2).ToString() + "%";
        }
        else
        {
            modDescription = "Attack is reduced by " + System.Math.Round(((1 - modifierStrength) * 100),2).ToString() + "%";
        }
        return modDescription;
    }

}
