using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : EnemyBaseState
{
    private int waypointIndex;
    public override void Enter()
    {
        Debug.Log("Idle");
    }

    public override void Exit()
    {
    }

    public override void Perform()
    {
        IdleState();
        if (Enemy.CanSeePlayer() == true)
        {
            StateMachine.changeState(new EnemyChaseState());
        }
    }

    public void IdleState()
    {
        if(Enemy.Agent.remainingDistance < 0.2f)
        {
            if(waypointIndex < Enemy.EnemyPath.Waypoints.Count -1) 
            {
                waypointIndex += 1;
            }
            else
            {
                waypointIndex = 0;
            }
            Enemy.Agent.SetDestination(Enemy.EnemyPath.Waypoints[waypointIndex].position);
        }
    }
}
