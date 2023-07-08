using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaladinState_Dash : PaladinState
{
    public PaladinState_Dash(Hero_Paladin paladin) : base(paladin, Actor.State.Dash)
    {
        
    }

    public override void Enter()
    {
        base.Enter();
        paladin.m_Animator.SetBool("isDash",true);
        paladin.m_agent.enabled = false;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        
        paladin.skill_1.Dash();
        
    }

    public override void End()
    {
        paladin.m_agent.enabled = true;
        paladin.m_Animator.SetBool("isDash", false);
        paladin.skill_1.Stop();
        base.End();
    }
}
