using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingState : FsmState<Actor.State>
{
    protected Building onwer;

    public BuildingState (Building building,Actor.State state):base(state)
    {
        onwer = building;
    }
}

public class BuildingHighState : FsmState<Actor.HighState>
{
    protected Building onwer;

    public BuildingHighState(Building building, Actor.HighState state) : base(state)
    {
        onwer = building;
    }
}
