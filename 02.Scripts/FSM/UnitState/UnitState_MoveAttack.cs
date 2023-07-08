using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitState_MoveAttack : UnitHighState
{
    float Timer = 0f;
    public UnitState_MoveAttack(Unit unit) : base(unit, Actor.HighState.MoveAttack)
    {
        
    }
    public override void Enter()
    {
        base.Enter();
        Timer = 0.31f;
    }
    public override void End()
    {
        base.End();        
    }
    public override void OnUpdate()
    {
        base.OnUpdate();

        if (m_owner.fsm.GetHighType == Actor.HighState.DIE)
            return;

        if (Timer<0.03f)
        {
            Timer += Time.deltaTime;
            return;
        }
        else
        {
            Timer = 0.0f;
        }

        if (m_owner.Target == null)
        {
            m_owner.Target = GameScene.I.ActorMng.FindNearTarget(m_owner, m_owner.ActorInfo.field);

            if (m_owner.Target == null)
            {
                m_owner.fsm.SetState(Actor.State.MOVE);
                return;
            }
        }        

        var distance = Actor.Distance(m_owner.Target, m_owner);
        if (distance <= m_owner.ActorInfo.maxRange && m_owner.fsm.GetStateType != Actor.State.ATTACK)
        {            
            m_owner.fsm.SetState(Actor.State.ATTACK);
        }
        if (distance > m_owner.ActorInfo.maxRange)
        {
            m_owner.fsm.SetState(Actor.State.TRACE);
        }
        //if (distance > m_owner.ActorInfo.field || m_owner.Target.fsm.GetStateType == Actor.State.DIE)
        //{
        //    //m_owner.fsm.SetState(Actor.State.MOVE);
        //    m_owner.Target = null;            
        //}
    }
}
