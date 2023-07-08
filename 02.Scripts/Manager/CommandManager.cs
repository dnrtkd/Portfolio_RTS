using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


/// <summary>
/// ¸í·É Á¾·ù
/// </summary>
public enum CommandType
{
    //À¯´Ö
    Move,
    MoveAttack,
    Stop,

    //ÀÏ²Û
    Construct,

    //°Ç¹°
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
