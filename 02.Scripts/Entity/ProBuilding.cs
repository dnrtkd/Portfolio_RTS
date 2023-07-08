using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProBuilding : Building
{
    public Building_Produce Production { get; private set; }    
    public override void Init()
    {
        base.Init();
        Production = GetComponent<Building_Produce>();
        Production.Init();        
    }
    public override void OnUpdate()
    {
        base.OnUpdate();              
    }
    public override ActorCtrl AddCtrlComponent()
    {
        return AddCtrlComponent<ProBuildingCtrl>();
    }
}
