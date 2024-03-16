using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyBaseState
{
    public override void Enter()
    {
        Debug.Log("Attacking");
        AttackingLogic();
    }

    public override void Exit()
    {
    }

    public override void Perform()
    { 
    }

    protected void AttackingLogic()
    {
        Enemy.enemyAttack();
    }
}
