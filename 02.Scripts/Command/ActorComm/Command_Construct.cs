using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Command_Construct : Command
{
    Farmer m_farmer;
    PlacebleObject obj;
    ProcessBuilding processBuilding;
    public Command_Construct(Farmer farmer, PlacebleObject obj)
    {
        this.m_farmer = farmer; this.obj=obj;        
    }
    public Command_Construct(Farmer farmer,ProcessBuilding building)
    {
        m_farmer = farmer; processBuilding = building;
    }
    public override void Cancel()
    {
        base.Cancel();
        m_farmer.fsm.SetState(Actor.HighState.NONE);
        m_farmer.fsm.SetState(Actor.State.IDLE);
        m_farmer.WaitbuildingsClear();
    }
    public override void Execute()
    { 
        if(processBuilding == null)
            processBuilding = GameScene.I.BuildMng.ProvideBuilding
                (obj, GameScene.I.WealthMng, m_farmer.m_team);        

        if (processBuilding == null)
            Cancel();
        else
        {
            m_farmer.builder.SetTarget(processBuilding);
            m_farmer.fsm.SetState(Actor.HighState.MoveConstruct);
        }                            
    }    
    public override void Update()
    {
        if (m_farmer.fsm.GetHighType==Actor.HighState.NONE)
        {
            if(m_farmer.WatingBuildings.Contains(obj))
                m_farmer.WatingBuildings.Remove(obj);

            isFinish = true;            
        }                    
    }
}
