using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class BuildingState_Destroy : BuildingHighState
{
    float Timer;
    float disapear;
    public BuildingState_Destroy(Building building,float disapear) : base(building, Actor.HighState.DIE)
    {
        this.disapear = disapear;
    }

    public override void Enter()
    {
        onwer.changeBuildingShpae(1);
        base.Enter();                    
        onwer.Effect.EffectOn(Building_Effect.EffectType.Destory);
        onwer.GetComponent<NavMeshObstacle>().enabled = false;
        onwer.Close();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (Timer > disapear)
        {
            onwer.ReturnToPool();                        
            onwer.Effect.EffectOff(Building_Effect.EffectType.Destory);
        }
        else
            Timer += Time.deltaTime;
    }
}
