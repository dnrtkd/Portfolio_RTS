using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaladinState : FsmState<Actor.State>
{
    protected readonly Hero_Paladin paladin;
    public PaladinState(Hero_Paladin paladin, Actor.State _type) : base(_type)
    {
        this.paladin = paladin;
    }
}

public class PaladinHighState : FsmState<Actor.HighState>
{
    protected readonly Hero_Paladin paladin;
    public PaladinHighState(Hero_Paladin paladin, Actor.HighState _type) : base(_type)
    {
        this.paladin = paladin;
    }
}
