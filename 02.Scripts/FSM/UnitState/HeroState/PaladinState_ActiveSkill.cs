using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaladinState_ActiveSkill : PaladinHighState
{
    enum State
    {
        Dash,
        StanBy,
        Hit,
    }
    State currType;

    public PaladinState_ActiveSkill(Hero_Paladin paladin) : base(paladin, Actor.HighState.Skill_1)
    {
        
    }

    public override void Enter()
    {
        currType = State.Dash;               
    }

    public override void OnUpdate()
    {
        UpdateState();
    }

    public override void End()
    {
        
    }

    public void UpdateState()
    {
        switch (currType)
        {
            case State.Dash:
                paladin.fsm.SetState(Actor.State.Dash);
                if(paladin.skill_1.Crash())
                {
                    currType = State.StanBy;
                    paladin.fsm.SetState(Actor.HighState.LOOKOUT);                    
                }
                break;
            case State.StanBy:
                
                break;
            case State.Hit:
                break;
            default:
                break;
        }
    }
}
