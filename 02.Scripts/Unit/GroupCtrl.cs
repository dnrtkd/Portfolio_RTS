using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//플레이어가 다수의 엑터들에게 명령을 내리기 위한 클래스
public class GroupCtrl : SubjectMono
{
    private List<ActorCtrl> m_selectedActors;
    private FormationList m_formationList;
    public List<Actor> AllActors { private set; get; }
    public List<ActorCtrl> SelectedActors { get { return m_selectedActors; } }    

    [SerializeField]
    KeyboardManager Setting;

    [SerializeField]
    private UI_Profil uI_Profil;

    [SerializeField]
    private UI_Group uI_Group;

    [SerializeField]
    private UI_Product uI_Product;

    ActorList actorList = new();
    //[SerializeField]
    //private UI_Product uI_Product;
    public void Awake()
    {
        m_selectedActors = new List<ActorCtrl>();
        AllActors = new();
        m_formationList = new FormationList(new SquaerFormation());
        m_formationList.Init();
        uI_Group.Connect(this);
    }
    public void Start()
    {
        AllActors = GameScene.I.ActorMng.Actors;
    }
    public void Update()
    {
        actorList.Update();

        for(var key=KeyCode.Alpha1; key<=KeyCode.Alpha9; ++key)
        {
            if(Input.GetKeyDown(key))
            {
                if(Input.GetKey(KeyCode.LeftShift))
                {
                    actorList.Add(m_selectedActors,key-KeyCode.Alpha1);
                }
                else
                {
                    var actors = actorList.Select(key - KeyCode.Alpha1);
                    if (actors == null || actors.Count == 0)
                        return;

                    DeselectAll();
                    SelectAll(actors);
                }
            }
        }

        //for (int i = 0; i < m_selectedActors.Count; i++)
        //{
        //    if (m_selectedActors[i].Owner.fsm.GetHighType == Actor.HighState.DIE)
        //        return;

        //    DiselectUnit(m_selectedActors[i]);
        //}
    }
    public void SelectAll(List<ActorCtrl> actorCtrls)
    {
        for (int i = 0; i < actorCtrls.Count; ++i)
        {
            Debug.Log(actorCtrls[i].Owner.ID);
            SelectActor(actorCtrls[i]);
        }
    }
    public void ClickSelectUnit(ActorCtrl _newActor)
    {
        DeselectAll();
        SelectActor(_newActor);
    }
    public void ShiftClickSelectUnit(ActorCtrl _newActor)
    {
        if(m_selectedActors.Contains(_newActor))
        {
            DiselectUnit(_newActor);
        }
        else
        {
            SelectActor(_newActor);
        }
    }    
    public void RightClickEvent(Vector3 pos)
    {
        for (int i = 0; i < m_selectedActors.Count; i++)
        {
           if (m_selectedActors[i].Owner.fsm.GetHighType == Actor.HighState.DIE)
            {
                DiselectUnit(m_selectedActors[i]);
                continue;
            }
            m_selectedActors[i].RightClickBehaviour(pos);
        }
        m_formationList.MoveFormation(pos);
    }
    public void LeftClickEvent(Vector3 pos)
    {
        for (int i = 0; i < m_selectedActors.Count; i++)
        {
            if (m_selectedActors[i].Owner.fsm.GetHighType == Actor.HighState.DIE)
            {
                DiselectUnit(m_selectedActors[i]);
                continue;
            }

            m_selectedActors[i].LeftClickBehaviour(pos);
        }        
    }
    public void SetTargetEvent(Actor actor)
    {        
        foreach (var actorCtrl in m_selectedActors)
            actorCtrl.Targetting(actor);
    }
    public void AttackCommand(Vector3 pos)
    {
        Debug.Log(pos.ToString());

        for (int i = 0; i < m_selectedActors.Count; i++)
        {
            if (m_selectedActors[i].Owner.ActorInfo.actor == Enums.ActorType.CHARACTER == false)
                continue;

            if (m_selectedActors[i].Owner.fsm.GetHighType == Actor.HighState.DIE)
            {
                DiselectUnit(m_selectedActors[i]);
            }

            m_selectedActors[i].Owner.AddCommands(new Command_Attack(m_selectedActors[i].Owner as Unit, pos));
        }
    }
    public void DeselectAll()
    {   
        for(int i=0;i< m_selectedActors.Count;i++)
        {
            //DiselectUnit(m_selectedActors[i]);
            m_selectedActors[i].DeselectUnit();            
        }
        m_selectedActors.Clear();
        Setting.RemoveAll();
        m_formationList.Clear();      
        Notify();        
        //InputManager.instance.OnKeyboardEvent -= OnKeboardFarmer;
    }
    public void SelectActor(ActorCtrl _newActor)
    {
        if (_newActor.Owner.fsm.GetHighType == Actor.HighState.DIE)
            return;

        if (m_selectedActors.Contains(_newActor) || _newActor ==null 
            || _newActor.Owner.m_team==Enums.TEAM.ENMEY)
            return;

        if(_newActor.Owner.ActorInfo.actor==Enums.ActorType.CHARACTER)
        {
            m_formationList.AddCharacter(_newActor.Owner as Unit);
        }

        if (_newActor.Owner.ActorInfo.actor == Enums.ActorType.BUILDING)
        {
            if (_newActor.Owner.GetType() == typeof(ProBuilding))
            {
                uI_Product.Connect((_newActor.Owner as ProBuilding).Production);
            }
        }

        _newActor.SelectUnit();
        m_selectedActors.Add(_newActor);
        
        uI_Profil.Connect(_newActor.Owner);

        Setting.Add(_newActor.actorSKills);
        
        Notify();
        //if (_newActor.Owner.ActorInfo.actor == Enums.ActorType.BUILDING || m_selectedActors.Count==1)
            //uI_Product.Connect(_newActor.Owner as Building);
    }
    public void DiselectUnit(ActorCtrl _newActor)
    {
        _newActor.DeselectUnit();
        m_selectedActors.Remove(_newActor);
        //Setting.Remove(_newActor.actorSKills);
        //_newActor.Owner.Detach(uI_Product);
        _newActor.Owner.Detach(uI_Profil);
        
        if (_newActor.Owner.ActorInfo.actor == Enums.ActorType.CHARACTER)
        {
            m_formationList.RemoveCharacter(_newActor.Owner as Unit);
        }

        if (_newActor.Owner.ActorInfo.actor == Enums.ActorType.BUILDING)
        {
            if (_newActor.Owner.GetType() == typeof(ProBuilding))
            {
                (_newActor.Owner as ProBuilding).Production.Detach(uI_Product);
            }
        }

        Notify();
    }   
}

public class ActorList
{
    // 1~9 번까지 단축키로 부대 지정 가능
    static readonly int maxNum=9;

    List<ActorCtrl>[] Offsets = new List<ActorCtrl>[maxNum];
      
    //오프셋에 유닛 상태 저장
    public void Add(List<ActorCtrl> actorsCtrl , int index)
    {
        List<ActorCtrl> actors=new();

        foreach (ActorCtrl ctrl in actorsCtrl)
            actors.Add(ctrl);

        Offsets[index] = actors;        
    }    

    public void Update()
    {
        for (int i = 0; i < maxNum; ++i)
        {
            if (Offsets[i] != null)
            {
                for (int j = 0; j < Offsets[i].Count; ++j)
                    if (Offsets[i][j].Owner.fsm.GetHighType == Actor.HighState.DIE)
                        Offsets[i].Remove(Offsets[i][j]);
            }            
        }            
    }

    //오프셋 반환
    public List<ActorCtrl> Select(int index)
    {        
        return Offsets[index];
    }

}
