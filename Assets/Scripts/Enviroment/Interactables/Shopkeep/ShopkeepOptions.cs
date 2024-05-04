using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "ShopkeeperOptions")]
public class ShopkeeperOptions : ScriptableObject
{
    //list of all options that can be given by trader
    [SerializeField]public Modifers[] ModiferList;

}
