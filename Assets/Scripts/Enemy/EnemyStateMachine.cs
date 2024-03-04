using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : MonoBehaviour
{

    public EnemyBaseState activeState;
    public EnemyIdleState idleState;
    // Start is called before the first frame update

    public void Init()
    {
        idleState = new EnemyIdleState();
        ChangeState(idleState);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (activeState != null)
        {
            activeState.Perform();
        }
        
    }

    public void ChangeState(EnemyBaseState nextState)
    {
        if (activeState != null)
        {
            activeState.Exit();
        }
        activeState = nextState;

        if (activeState != null)
        {
            activeState.StateMachine = this;
            activeState.Enemy = GetComponent<Enemy>();
            activeState.Enter();
        }
    }
}
