using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmerState : FsmState<Unit.State>
{
    protected readonly Farmer farmer;
    public FarmerState(Farmer farmer, Actor.State _type) : base(_type)
    {
        this.farmer=farmer;
    }    
}

public class FarmerHighState : FsmState<Unit.HighState>
{
    protected Farmer farmer;
    public FarmerHighState(Farmer farmer, Actor.HighState _type) : base(_type)
    {
        this.farmer = farmer;
    }
}