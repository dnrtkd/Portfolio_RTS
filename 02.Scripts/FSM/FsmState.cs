using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FsmState<T> where T:System.Enum
{
    protected T stateType;
    public T StateType { get { return stateType; } }
    public FsmState(T _type)
    {
        stateType = _type;
    }
    public virtual void Enter() { }
    public virtual void OnUpdate() { }
    public virtual void End() { }

}
