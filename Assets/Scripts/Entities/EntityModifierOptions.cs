using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EntityOptions")]
public class EntityModifierOptions : ScriptableObject
{
    [SerializeField]public Modifers health;
}
