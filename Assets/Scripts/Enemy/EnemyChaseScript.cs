using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaseState : EnemyBaseState
{
    public override void Enter()
    {
        Debug.Log("Chasing");
    }

    public override void Exit()
    {
    }

    public override void Perform()
    {
        ChaseLogic();
    }

    private void ChaseLogic()
    {
        Enemy.Agent.destination = Enemy.EnemyTarget.transform.position;
        Enemy.Agent.speed = Enemy.EnemySpeed;
        if (Enemy.CanSeePlayer() != true)
        {
            StateMachine.changeState(new EnemyIdleState());
        }
        if (Vector3.Distance(Enemy.transform.position, Enemy.EnemyTarget.transform.position) <= Enemy.EnemyAttackRange)
        {
            StateMachine.changeState(new EnemyAttackState());
        }
    }

}
