using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Modifers : ScriptableObject
{
	protected string modDescription;

	public string ModDescription
	{
		get { return modDescription; }
		set { modDescription = value; }
	}

    public abstract string generateModDesc(float modifierStrength = 0);
    public abstract void applyMod(Entity target, float modifierStrength = 0);

}
