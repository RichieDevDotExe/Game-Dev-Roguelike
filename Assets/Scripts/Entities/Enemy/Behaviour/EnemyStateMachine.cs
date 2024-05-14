using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : MonoBehaviour
{

    public EnemyBaseState activeState;

    //initalise statemachine
    public void Init()
    {
        changeState(new EnemyIdleState());
    }
    void Start()
    {
        
    }

    void Update()
    {
        if (activeState != null)
        {
            activeState.Perform();
        }
        
    }

    public void changeState(EnemyBaseState nextState)
    {
        //if no next state is called perform exit function
        if (activeState != null)
        {
            activeState.Exit();
        }
        activeState = nextState;

        //If no active state reset to default. this should usually only run at the start
        if (activeState != null)
        {
            activeState.StateMachine = this;
            activeState.Enemy = GetComponent<Enemy>();
            activeState.Enter();
        }
    }
}
