using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;
using UnityEngine.Events;

public class UnitCtrl:ActorCtrl 
{    
    private GameObject UnitMarker;
    public Unit OwnerUnit { get { return m_owner as Unit; } }    

    protected UnityEvent<Vector3> LeftClickAction=new();
    protected UnityEvent<Actor> LeftTargettingAction = new();

    protected ActorSkill move;
    protected ActorSkill attack;
    protected ActorSkill stop;
    
    public override void Init()
    {
        base.Init();
        m_owner = GetComponent<Unit>();
        UnitMarker = transform.GetChild(0).gameObject;
        InitSKill();

        actorSKills.Add(move);
        actorSKills.Add(attack);
        actorSKills.Add(stop);

        void InitSKill()
        {
            move = new UnitSkill(KeyCode.M, this.OwnerUnit, "Move", "Move",
            () =>
            {
                LeftClickAction.RemoveAllListeners();
                LeftClickAction.AddListener(OwnerUnit.Move);
                CursorManager.instance.SetCursor(CursorManager.CursorType.Hand);
            });

            attack = new UnitSkill(KeyCode.A, this.OwnerUnit, "Attack", "Attack",
            () =>
            {
                LeftClickAction.RemoveAllListeners();
                LeftClickAction.AddListener(OwnerUnit.Attack);
                CursorManager.instance.SetCursor(CursorManager.CursorType.Attack);
            });

            stop = new UnitSkill(KeyCode.S, this.OwnerUnit, "Stop", "Stop",
            () =>
            {
                OwnerUnit.CommandCancle();
            });
        }
    }
    public override void SelectUnit()
    {
        base.SelectUnit();
        //UnitMarker.SetActive(true);
    }

    public override void DeselectUnit()
    {
        base.DeselectUnit();

        //UnitMarker.SetActive(false);
    }
    public override void RightClickBehaviour(Vector3 pos)
    {
        if (Input.GetKey(KeyCode.LeftShift) == false)
            CommandCancle();

        base.RightClickBehaviour(pos);
    }    
    public override void LeftClickBehaviour(Vector3 pos )
    {
        if (Input.GetKey(KeyCode.LeftShift) == false)
            CommandCancle();

        base.LeftClickBehaviour(pos);
       
        LeftClickAction.Invoke(pos);
        LeftClickAction.RemoveAllListeners();
        
        CursorManager.instance.SetCursor(CursorManager.CursorType.Normal);
    }     
}
