using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


/// <summary>
/// ��� ����
/// </summary>
public enum CommandType
{
    //����
    Move,
    MoveAttack,
    Stop,

    //�ϲ�
    Construct,

    //�ǹ�
    UnitProduce,
}

public class CommandManager : Base_Manager
{    
    
    public override void Init()
    {        
    }
    public override void Clear()
    {
           
    }    
    public override void OnUpdate()
    {
        
    }
}
