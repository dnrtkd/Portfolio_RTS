using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitState : FsmState<Unit.State>
{
    protected Unit m_owner;
    public UnitState(Unit unitm,Unit.State _state):base(_state)
    {
        m_owner = unitm;
    }    
}

public class UnitHighState: FsmState<Actor.HighState>
{
    protected Unit m_owner;    
    public UnitHighState(Unit unit , Unit.HighState state):base(state)
    {
        m_owner = unit;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();        
    }

    protected void CorutineStart(float period,System.Action action)
    {        
        m_owner.StartCoroutine(UpdateCycle(period,action));
    }

    protected void CorutineStop()
    {
        m_owner.StopAllCoroutines();
    }

    IEnumerator UpdateCycle(float period,System.Action action)
    {
        System.Action _action = action;

        while(true)
        {
            yield return new WaitForSeconds(period);
            action.Invoke();
        }
    }
}
