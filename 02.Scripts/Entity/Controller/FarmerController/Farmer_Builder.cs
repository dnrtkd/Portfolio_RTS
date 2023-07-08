using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farmer_Builder : MonoBehaviour
{
    Farmer farmer;
    public ProcessBuilding processBuilding { get; set; }
   
    public void Init()
    {
        farmer = GetComponent<Farmer>();
    }    
    public void SetTarget(ProcessBuilding _process)
    {
        processBuilding = _process;
    }   
    public void Build()
    {
        if (processBuilding.Complete)
        {                                   
            return;
        }
        processBuilding.BuildingStructure(Time.deltaTime);
    }

}
