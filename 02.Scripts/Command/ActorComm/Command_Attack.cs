using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
///  전방위 공격 명령
/// </summary>
public class Command_Attack : UnitCommand
{
    Vector3 des;    
    public Command_Attack(Unit unit ,Vector3 des):base(unit)
    {
        this.unit = unit; this.des = des;
    }   
    public override void Execute()
    {
        unit.Destination = des;
        unit.fsm.SetState(Actor.HighState.MoveAttack);        
    }
    public override void Cancel()
    {
        base.Cancel();
        unit.fsm.SetState(Actor.HighState.LOOKOUT);
    }

    public override void Update()
    {
        if (Mathf.Abs(unit.transform.position.x - des.x) <= 0.1 &&
            Mathf.Abs(unit.transform.position.z - des.z) <= 0.1)
        {
            Cancel();
        }
        //if (unit.Target == null)
        //{
        //    unit.Target = GameScene.I.ActorMng.FindNearTarget(unit, unit.ActorInfo.maxRange);
        //}
        //if (unit.Target == null)
        //{
        //    //unit.m_unitMove.SetTarget(des);
        //    return;
        //}                
        //float distance = Vector3.Distance(unit.Target.transform.position, unit.transform.position);
        //if (distance > unit.ActorInfo.maxRange)
        //{
        //    unit.Target = null;
        //    return;
        //}

        //if(unit.fsm.getStateType!=Actor.State.ATTACK)
        //    unit.fsm.SetCurrState(Actor.State.ATTACK);

        //if (Mathf.Abs(unit.transform.position.x - des.x) <= 0.1 &&
        //    Mathf.Abs(unit.transform.position.z - des.z) <= 0.1)
        //{
        //    unit.fsm.SetCurrState(Actor.State.IDLE);
        //}



        //if (unit.Target == null || unit.Target.fsm.getStateType == Actor.State.DIE)
        //{
        //    unit.Target = GameScene.I.ActorMng.FindNearTarget(unit, unit.ActorInfo.field);
        //}
        //if (unit.Target == null)
        //{
        //    unit.m_unitMove.SetTarget(des);
        //    return;
        //}
        //var distance = Vector3.Distance(unit.Target.transform.position, unit.transform.position);
        //if (distance > unit.ActorInfo.field)
        //{
        //    unit.Target = null;
        //    return;
        //}
        //if (distance > unit.Target.ActorInfo.maxRange)
        //    unit.fsm.SetCurrState(Unit.State.TRACE);

        //if (Mathf.Abs(unit.transform.position.x - des.x) <= 0.1 &&
        //    Mathf.Abs(unit.transform.position.z - des.z) <= 0.1)
        //{
        //    Cancel();
        //}
    }
}
