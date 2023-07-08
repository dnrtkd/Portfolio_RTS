using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingCtrl : ActorCtrl
{    
    public Building OwnerBuilding { get { return m_owner as Building; } }
    public override void Init()
    {
        base.Init();
        m_owner = GetComponent<Building>();
    }
    public override void SelectUnit()
    {
        base.SelectUnit();        
    }
    public override void DeselectUnit()
    {
        base.DeselectUnit();                
    }
    public override void RightClickBehaviour(Vector3 pos)
    {
        if (OwnerBuilding.isComplete == false)
            return;

        base.RightClickBehaviour(pos);        
    }           
}
