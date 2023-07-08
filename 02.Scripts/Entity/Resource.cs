using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 나무나 석재같은 자원을 나타내는 클래스
/// </summary>
public class Resource : Actor
{
    //자원 량
    public WealthData wealth;
    public int gatherTime; // 채취 시간

    public override void Init()
    {
        base.Init();        
    }
    public override ActorCtrl AddCtrlComponent()
    {
        return AddCtrlComponent<ResourceCtrl>();
    }
    public override void OnUpdate()
    {
        base.OnUpdate();
    }

    public void BeGathered(int _value)
    {       
        wealth.SubCount(_value);        
        Notify();
    }
}