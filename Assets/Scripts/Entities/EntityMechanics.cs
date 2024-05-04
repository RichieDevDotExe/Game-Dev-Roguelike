using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EntityMechanics : ScriptableObject
{
    Entity entity;

    [SerializeField] protected float savedHealth;
    [SerializeField] protected float savedMaxHealth;
    [SerializeField] protected float savedSpeed;
    [SerializeField] protected float savedDamage;

    [Header("Mechanics Tuning")]
    [SerializeField] private float frostStrength;
    private int frostStacks;


    public void frostMechanic(int stacks)
    {
        frostStacks = frostStacks + stacks-1;
        if ((stacks == 0) && (savedSpeed != entity.EntitySpeed))
        {
            savedSpeed = entity.EntitySpeed;
        }
        else if (stacks < 7)
        {
            entity.EntitySpeed = (float)(savedSpeed - (savedSpeed * Math.Pow(frostStrength, 1 / stacks)));
        }
        //else if (stacks <= 7)
        //{
        //    frostStacks = 1;
        //}
    }
    //public void frozen()
    //{
    //    entity.EntitySpeed = 0;
    //}


}
