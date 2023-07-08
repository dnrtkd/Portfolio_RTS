using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class Actor : SubjectMono,IPoolObj
{
    //구체적인 동작을 결정 지어주는 상위 상태
    public enum HighState
    {           
        LOOKOUT,
        NONE,
        MoveAttack,
        CC,
        //일꾼
        MoveConstruct,
        MoveGather,
        DIE,

        //영웅 팔라딘
        Skill_1,
    }
    //구체적인 동작
    public enum State
    {
        // 공통
        IDLE,        
        ATTACK,
        Airborne,
        //유닛
        MOVE,
        PATROL,
        TRACE, 
        DieAction,

        //건물        
        ALREADY,//지어지기전 상태        
        PRODUCT,        
        DAMAGED,

        //일꾼
        FARMING, //농사        
        CONSTRUCT, //건설
        GATHERING,//목재

        //영웅
        Dash,
        Hit,
    }
    public int ID { get; set; }
    public Enums.TEAM m_team;
    protected float m_hp;
    protected int m_maxhp;
    [SerializeField]
    protected ActorRecord m_actorRecord;
    protected bool m_isSelected;
    private CommandQueue m_commands;
    protected bool isReturnToPool;
    public Renderer[] renderes;
    public float actorScale 
    { get { return transform.localScale.x* GetComponent<BoxCollider>().size.x; } }    
    public UI_HpBar hPbar;
    public LayerFSM<HighState,State> fsm = new();
    public float Hp { get { return m_hp; } }
    public int MaxHp { get { return m_maxhp; } }
    public bool IsSelected { get { return m_isSelected; }  set { m_isSelected = value; Notify(); } }
    public float HpRatio { get { return m_hp / m_maxhp; } }
    public Vector3 Position { get { return transform.position; }  set { transform.position = value; } }
    public Quaternion Rotation { get { return transform.rotation; } set { transform.rotation = value; } }
    public ActorRecord ActorInfo { get { return m_actorRecord; } }
    public Actor Target { get; set; }        
    public static float Distance(Actor a, Actor b)
    {
        return Vector3.Distance(a.Position, b.Position) - 
            ((a.actorScale/2)*1.4f) - ((b.actorScale / 2) * 1.4f);
        
    }        
    public virtual void Init()
    {
        renderes= GetComponentsInChildren<Renderer>();        
        SetMaxHp(m_actorRecord.maxHp);
        m_commands = new CommandQueue();
        //MinimpaPlane();
        
        hPbar = GetComponent<UI_HpBar>();        
        if (hPbar != null)
            hPbar.Init();
    }
    public virtual void OnUpdate()
    {
        m_commands.Update();
    }    
    public void InitActorRecord(ActorData actorData)
    {
        if (actorData == null)
            return;

        m_actorRecord.actor = actorData.actor;
        m_actorRecord.atk = actorData.atk;
        m_actorRecord.atkTime = actorData.atkTime;
        m_actorRecord.def = actorData.def;
        m_actorRecord.maxHp = actorData.maxHp;
        m_actorRecord.maxRange = actorData.maxRange;
        m_actorRecord.minRange = actorData.minRange;
        m_actorRecord.moveSpeed = actorData.moveSpeed;
        m_actorRecord.name = actorData.name;
        m_actorRecord.field = actorData.field;
    }
    public void AddHp(float _value)
    {
        SetHp(m_hp + _value);
    }
    public void SubHp(float _value)
    {
        SetHp(m_hp - _value);
    }
    private void SetMaxHp(int _maxHP)
    {
        m_maxhp = _maxHP;
    }
    public void SetHp(float _hp)
    {
        m_hp = _hp;
        if (m_hp < 0)
            m_hp = 0;
        if (m_hp > m_maxhp)
            m_hp = m_maxhp;
        if (hPbar != null)
            hPbar.SetSliderValue(Hp / MaxHp);
        Notify();              
    }
    public void AddCommands(ICommand _command)
    {
        m_commands.Add(_command);
    }
    public void CommandCancle()
    {
        m_commands.Cancel();
    }
    public virtual ActorCtrl AddCtrlComponent()
    {
        return null;
    }
    protected ActorCtrl AddCtrlComponent<T>() where T:ActorCtrl
    {
        if (GetComponent<T>() != null)
            return GetComponent<T>();

        return gameObject.AddComponent<T>();
    }
    public void Damaged(float _damage)
    {
        SubHp(_damage);
        if (hPbar != null)
            hPbar.ON();
        if (Hp<=0)
            fsm.SetState(HighState.DIE);
        GameScene.I.GameEvent.Publish(RTS_EventSystem.RtsEventType.BE_ATTACKED);
    }
    public virtual void Open(Vector3 pos, Enums.TEAM _team)
    {
        CommandCancle();
        GetComponent<BoxCollider>().enabled = true;
        isReturnToPool = false;
        gameObject.SetActive(true);
        SetHp(ActorInfo.maxHp);
        transform.position = pos;
        m_team = _team;
        IsSelected = false;
        if (_team == Enums.TEAM.PLAYER)
            hPbar.SetColor(Color.green);
        else if (_team == Enums.TEAM.ENMEY)
            hPbar.SetColor(Color.red);
    } 
    public void MinimpaPlane()
    {
        var quad = GameObject.CreatePrimitive(PrimitiveType.Quad);
        quad.transform.parent = transform;
        quad.layer = LayerMask.NameToLayer("Minimap");
        quad.transform.localPosition = new Vector3(0, 0, 0);
        quad.transform.Rotate(new Vector3(90, 0, 0));
        quad.transform.localScale *= actorScale;
        quad.GetComponent<Renderer>().material = new Material(ResUtil.Load<Material>("Quad"));
        if (m_team == Enums.TEAM.PLAYER)
            quad.GetComponent<Renderer>().material.color = Color.blue;
        else if(m_team == Enums.TEAM.ENMEY)
            quad.GetComponent<Renderer>().material.color = Color.red;
    }
    public virtual void Close()
    {
        GetComponent<BoxCollider>().enabled = false;        
        GameScene.I.ActorMng.Actors.Remove(this);
        GetComponent<ActorCtrl>().Remove();        
    }
    public virtual bool isReturn()
    {
        return isReturnToPool;
    }   
    public void ReturnToPool()
    {
        isReturnToPool = true;
    }
}
