using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM<T> where T: System.Enum
{
    protected Dictionary<T, FsmState<T>> stateList = new();    
    protected FsmState<T> state;
    protected T before;
    protected bool isStateChanging = false;
    public FsmState<T> getState { get { return state; } }
    public T getStateType
    {
        get
        {
            if (null == state)
                return default(T);

            return state.StateType;
        }
    }
    public void Before()
    {      
        SetCurrState(before);
    }
    public virtual void Clear()
    {
        stateList.Clear();
        state = null;
    }
    public virtual void AddState(FsmState<T> _state)
    {
        if(null== _state)
        {
            Debug.Log("FsmClass:: AddFsm()[null==Fsmstate<T>]");
            return;
        }
        if(true== stateList.ContainsKey(_state.StateType))
        {
            Debug.Log("FsmClass::AddFsm()[already have]");
        }

        stateList.Add(_state.StateType, _state);
    }
    public virtual void SetCurrState(T _type )
    {
        if(false== stateList.ContainsKey(_type))
        {
            Debug.LogError("FsmClass::AddFsm()[no have state]"+_type);
            return;
        }

        if(isStateChanging ==true)
        {
            Debug.LogError("FsmClass::AddFsm()[change state]"+_type);
            //return;
        }

        FsmState<T> _nextState = stateList[_type];
        if(_nextState==state)
        {
            //Debug.LogWarning("FsmClass::SetState() [same state:" + _type);
            return;
        }

        isStateChanging = true;

        if(null != state)
        {
            state.End();
            before = state.StateType;
        }        
        state = _nextState;
        state.Enter();
        isStateChanging = false;
    }  
    public virtual void Update()
    {
        if (null == state)
            return;
        state.OnUpdate();
    }
}
