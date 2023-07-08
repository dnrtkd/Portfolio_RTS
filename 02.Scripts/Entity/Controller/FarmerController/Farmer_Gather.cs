using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farmer_Gather : MonoBehaviour
{
    Farmer farmer;

    public Resource target;
    //채집 시간
    [SerializeField]
    float gatherTime;
    //채집 량
    public int value;
    //채취한 자원
    WealthData.WEALTH_TYPE type;
    public int count;

    public bool haveItem;
    
    public bool isWork;
    
    public Actor castle;

    float Timer;
    public void Init()
    {
        farmer = GetComponent<Farmer>();
        castle = GameScene.I.ActorMng.FindBuilding(this.farmer, Enums.BuildingType.castle, 30.0f);
        Timer = 0.0f;
    }

    public void StartGathering()
    {
        if (target.wealth.count == 0)
            return;

        castle = GameScene.I.ActorMng.FindBuilding(this.farmer, Enums.BuildingType.castle, 30.0f) ;
        farmer.transform.LookAt(target.Position);        
    }
    
    public void Store()
    {
        GameScene.I.WealthMng.AddCount(type,count);
        var hud= ResUtil.Create("UI_HUD_TEXT").GetComponent<UI_HUD_Resource>();
        Vector3 castlePos = castle.transform.position;
        Collider coll = castle.GetComponent<Collider>();
        Vector3 pos = new Vector3( 0,coll.bounds.max.y-1.0f,0) + castlePos;
        hud.Set(count, ResUtil.EnumToString(type),pos);

        count = 0;                
        haveItem = false;
    }

    public void OnUpdate()
    {
        if(Timer>gatherTime)
        {
            Timer = 0.0f;
            target.BeGathered(value);
            type = target.wealth.type;
            count = value;
            haveItem = true;
            return;
        }

        Timer += Time.deltaTime;
    }        
}
