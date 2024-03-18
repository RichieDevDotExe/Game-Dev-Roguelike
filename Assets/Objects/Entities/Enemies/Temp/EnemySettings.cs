using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "TempEnemy", menuName = "EnemySettings/TempEnemy")]
public class EnemySettings : ScriptableObject
{
    [SerializeField] public float MaxHealth;
    [SerializeField] public float Damage;
    [SerializeField] public float Speed;
    [SerializeField] public float AttackRange;
    [SerializeField] public float detectionRange;
    [SerializeField] public float fieldOfView;
    [SerializeField] public float enemyCooldown;
    [SerializeField] public float chargeStrength;
    //[SerializeField] public GameObject player;
}
