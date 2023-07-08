using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmerState_MoveGather : FarmerHighState
{   
    enum State
    {
        gotoResoruce,
        gather,        
        returnBase
    }
    State currType;
    public FarmerState_MoveGather(Farmer farmer) : base(farmer, Actor.HighState.MoveGather)
    {
    }
    private void UpdateState()
    {
        switch (currType)
        {
            case State.gotoResoruce:
                farmer.Destination = farmer.gather.target.Position;
                farmer.fsm.SetState(Actor.State.MOVE);
                var distance = Actor.Distance
                (farmer.gather.target.GetComponent<Actor>(), farmer);                
                if (distance <= 0.3f)
                {
                    currType = State.gather;
                    farmer.fsm.SetState(Actor.State.GATHERING);
                }
                break;
            case State.gather:
                if (farmer.gather.haveItem == true)
                {
                    currType = State.returnBase;
                    farmer.Destination = farmer.gather.castle.Position;
                    farmer.fsm.SetState(Actor.State.MOVE);
                }
                break;
            case State.returnBase:
                distance = Actor.Distance
                (farmer.gather.castle.GetComponent<Actor>(), farmer);

                if (distance <= 0.5f)
                {
                    currType = State.gotoResoruce;
                    farmer.gather.Store();
                    farmer.gather.haveItem = false;                                     
                    farmer.fsm.SetState(Actor.State.IDLE);
                }
                    break;
            default:
                break;
        }
    }
    public override void Enter()
    {
        base.Enter();
        farmer.Destination = farmer.gather.target.Position;
        farmer.fsm.SetState(Actor.State.MOVE);
    }
    public override void OnUpdate()
    {
        base.OnUpdate();

        //if (farmer.fsm.GetStateType == Actor.State.IDLE)
        //{
        //    farmer.fsm.SetState(Actor.HighState.NONE);
        //    return;
        //}

        UpdateState();
    }
    public override void End()
    {
        base.End();
        farmer.fsm.SetState(Actor.State.IDLE);
    }

}
