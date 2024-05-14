using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Modifers : ScriptableObject
{
	protected string modDescription;
	[SerializeField] protected string modName;

    public string ModName
    {
        get { return modName; }
        set { modName = value; }
    }

    public string ModDescription
	{
		get { return modDescription; }
		set { modDescription = value; }
	}

    //This is used to generate what effect the mod will apply
    public abstract string generateModDesc(float modifierStrength = 0);

    //applies mod effect to entity
    public abstract void applyMod(Entity target, float modifierStrength = 0);

}
