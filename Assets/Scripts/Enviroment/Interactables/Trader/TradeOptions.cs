using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

[CreateAssetMenu(menuName = "TraderOptions")]
public class TraderOptions : ScriptableObject
{
    //list of all options that can be given by trader
    public Modifers[] ModiferList;

}
