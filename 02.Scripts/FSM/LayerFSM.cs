using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerFSM<High,Low> where High : System.Enum where Low : System.Enum 
{  
    protected FSM<High> highLayer=new();
    protected FSM<Low> LowLayer=new();
    public FsmState<Low> GetState
    {
        get
        {
            return LowLayer.getState;
        }
    }
    public FsmState<High> GetHigh { get { return highLayer.getState; } }
    public Low GetStateType { get { return LowLayer.getStateType; } }
    public High GetHighType { get { return highLayer.getStateType; } }
    public void AddState( FsmState<High> state)
    {        
          highLayer.AddState(state);
    }
    public void AddState(FsmState<Low> state)
    {
        LowLayer.AddState(state);
    }
    public void SetState(High type)
    {       
        highLayer.SetCurrState(type);
    }   
    public void SetState(Low type)
    {
        LowLayer.SetCurrState(type);
    }
    public void OnUpdate()
    {
        highLayer.Update();
        LowLayer.Update();
    }
}
