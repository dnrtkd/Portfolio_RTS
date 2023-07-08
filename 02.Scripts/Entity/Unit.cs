using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Unit : Actor
{   
    Actor m_TargetActor;
    public Animator m_Animator { get; private set; }
    public UnitMove m_unitMove { get; private set; }
    public UnitAttack m_unitAtk { get; private set; }
    public NavMeshAgent m_agent { get; private set; }    
    public Vector3 Destination;
    public Enums.UnitType type;
    public CC_Type currCC;
    private readonly int hashMove = Animator.StringToHash("isWork");
    public override void Init()
    {
        base.Init();
        m_Animator = GetComponent<Animator>();
        m_unitMove = GetComponent<UnitMove>();
        m_unitAtk = GetComponent<UnitAttack>();
        m_agent = GetComponent<NavMeshAgent>();
        m_unitMove.Init();
        m_unitAtk.Init();
        SetHp(ActorInfo.maxHp);

        InitState();               
    }
    void InitState()
    {
        fsm.AddState(new UnitState_Idle(this));
        fsm.AddState(new UnitState_Trace(this));
        fsm.AddState(new UnitState_Move(this));
        fsm.AddState(new UnitState_Attack(this));
        fsm.AddState(new UnitState_Die(this, 3.0f));
        fsm.AddState(new UnitState_LookOut(this));
        fsm.AddState(new UnitState_None(this));
        fsm.AddState(new UnitState_MoveAttack(this));
        fsm.AddState(new UnitState_DieAciton(this));
        fsm.AddState(new Unitstate_Airborne(this));
        fsm.AddState(new UnitState_CC(this));

        fsm.SetState(HighState.LOOKOUT);
    }
    public override void Open(Vector3 pos, Enums.TEAM _team)
    {
        base.Open(pos,_team);
        fsm.SetState(HighState.LOOKOUT);
        m_agent.enabled = true;
    }
    public override void Close()
    {
        base.Close();        
        m_Animator.SetTrigger("isDie");
        m_agent.enabled = false;
    }
    public override void OnUpdate()
    {                
        fsm.OnUpdate();

        if (fsm.GetHighType == Actor.HighState.DIE)
            return;
        base.OnUpdate();
        m_unitMove.OnUpdate();
        m_unitAtk.OnUpdate();

        //if (Hp <= 0 && fsm.GetHighType == Actor.HighState.DIE)
          //  fsm.SetState(Actor.HighState.DIE);        
    }
    public void Move(Vector3 end)
    {
         AddCommands(new Command_UnitMove(this, end));         
    }    
    public void Attack(Vector3 des)
    {
        AddCommands(new Command_Attack(this, des));
    }
    public override ActorCtrl AddCtrlComponent()
    {
        return AddCtrlComponent<UnitCtrl>();
    }    
    public void Die()
    {
        
    }
    public void SetCC(CC_Type cc)
    {
        currCC = cc;
        fsm.SetState(Actor.HighState.CC);
    }
}
