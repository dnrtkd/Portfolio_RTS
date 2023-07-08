using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Command_ProductUnit : Command
{
    public Enums.UnitType type;
    ProBuilding building;

    public Command_ProductUnit(Enums.UnitType type,ProBuilding building)
    {
        this.type = type; this.building = building;   
    }    
    public override void Execute()
    {
        building.Production.UnitCre.Push(type);            
    }   
    public override void Update()
    {
        if(building.Production.UnitCre.isFinish)
        {
            building.Production.CreateDone();
            isFinish = true;
        }
        
        building.Production.UnitCre.Create();

    }       
}
