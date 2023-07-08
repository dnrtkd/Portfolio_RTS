using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitState_None : UnitHighState
{
    public UnitState_None(Unit unitm) : base(unitm, Actor.HighState.NONE)
    {

    }
    public override void Enter()
    {
        base.Enter();       
    }    
}
