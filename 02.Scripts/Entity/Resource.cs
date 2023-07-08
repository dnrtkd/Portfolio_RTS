using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ������ ���簰�� �ڿ��� ��Ÿ���� Ŭ����
/// </summary>
public class Resource : Actor
{
    //�ڿ� ��
    public WealthData wealth;
    public int gatherTime; // ä�� �ð�

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