using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farmer : Unit
{
    public Farmer_Builder builder;
    public Farmer_Gather gather;

    private List<PlacebleObject> m_waitBuildings = new List<PlacebleObject>();
    public List<PlacebleObject> WatingBuildings { get { return m_waitBuildings; } }
    public override void Init()
    {
        base.Init();
        builder = GetComponent<Farmer_Builder>();
        gather = GetComponent<Farmer_Gather>();
        builder.Init();
        gather.Init();
        fsm.AddState(new FarmerState_MoveConstruct(this));
        fsm.AddState(new FarmerState_Construct(this));
        fsm.AddState(new FarmerState_MoveGather(this));
        fsm.AddState(new FarmerState_gathering(this));
        fsm.SetState(Actor.HighState.NONE);        
    }
    public override ActorCtrl AddCtrlComponent()
    {
        return AddCtrlComponent<FarmerCtrl>();
    }
    public void WaitbuildingsClear()
    {
        foreach (var item in m_waitBuildings)
        {
            Destroy(item.targetObj);
        }
        m_waitBuildings.Clear();
    }
    public void ConStruct(PlacebleObject obj)
    {
        AddCommands(new Command_Construct(this, obj));
    }
    public void ConStruct(ProcessBuilding obj)
    {
        AddCommands(new Command_Construct(this, obj));
    }
    public void Gathering(Resource resource)
    {
        AddCommands(new Command_Gather(this, resource));
    }
    public void ConstructBuilding(Vector3 end, PlacebleObject obj)
    {
        //MoveTo(end);
        m_waitBuildings.Add(obj);
        ConStruct(obj);
    }
    public void SetOnWaitBuildings()
    {
        foreach (var obj in m_waitBuildings)
            obj.SetOn();
    }
    public void SetOffWaitBuildings()
    {
        foreach (var obj in m_waitBuildings)
            obj.SetOff();
    } 
    //대기열에 있는 건물들에 선을 그어줌
    public void DrawConnectLine()
    {
        if (m_waitBuildings.Count <= 0)
            return;

        GameScene.I.LineMng.DrawLine(transform.position, m_waitBuildings[0].Position, Color.green);

        for (int i = 0; i < m_waitBuildings.Count-1; i++)
        {
            var startPos = m_waitBuildings[i].Position;
            var endPos = m_waitBuildings[i+1].Position;

            GameScene.I.LineMng.DrawLine(startPos, endPos, Color.green);
        }
    }
}
