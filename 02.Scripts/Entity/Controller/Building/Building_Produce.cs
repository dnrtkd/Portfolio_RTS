using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 건물의 유닛 생산을 담당하는 컴포넌트
/// </summary>
public class Building_Produce : SubjectMono
{    
    public ProBuilding Owner { get; private set; }
    private ProduceUnitTable table ;
    private WealthDataCollection wealth;

    [SerializeField]
    private List<Enums.UnitType> unitCatalog = new List<Enums.UnitType>();

    [SerializeField]
    private readonly int MAXWAIT = 4;  //명령 개수 제한 상수

    private Vector3 m_rallyPoint; //랠리 포인트
    public Unit_Creator UnitCre { get; private set; }
    public Vector3 RallyPoint
    {
        get
        {
            return (m_rallyPoint == Vector3.zero) ? m_rallyPoint =
            new Vector3(transform.position.x, transform.position.y, transform.position.z + Owner.SizeX / 2) :
            m_rallyPoint;
        }
        set { m_rallyPoint = value; }
    }
    public Queue<Enums.UnitType> WaitList { get; set; } = new Queue<Enums.UnitType>();   
    public bool IsProduct { get { return WaitList.Count > 0; } }   
    public bool isFull { get { return WaitList.Count >= 5; } }
    public List<Enums.UnitType> UnitCatalog { get { return unitCatalog; } }

    Vector3 respawnPosition;

    public GameObject TheFlag { get; private set; }    
    public void EnQueue(Enums.UnitType type)
    {
        if (isFull)
            return;

        if (isEnoughCost(type))
        {
            wealth.SubCount(WealthData.WEALTH_TYPE.Food, table.GetItem(type).food);
            wealth.SubCount(WealthData.WEALTH_TYPE.Wood, table.GetItem(type).wood);
        }
        else
        {            
            Debug.Log("자원이 모자랍니다.");
            return;
        }

        WaitList.Enqueue(type);
        Owner.AddCommands(new Command_ProductUnit(type, this.Owner));        
        Notify();
    }
    public Enums.UnitType DeQueue()
    {        
        var enum1= WaitList.Dequeue();
        Notify();
        return enum1;            

    }
    public void Init()
    {
        Owner = GetComponent<ProBuilding>();
        table = DataManager.Instance.produceUnitTable;
        wealth = GameScene.I.WealthMng;
        UnitCre = new(this);
        respawnPosition = new Vector3(Owner.Position.x + Owner.actorScale, Owner.Position.y, Owner.Position.z);

        if (TheFlag == null)
            TheFlag = ResUtil.Create("TT_Banner_Blue_A");        
    }           
    public void SetRallyPoint(Vector3 pos)
    {
        TheFlag.transform.position = pos;
        m_rallyPoint = pos;
    }
    public void DrawToRallyPoint()
    {
        //GetComponent<LineRenderHelper>().DrawLine(transform.position, RallyPoint, Color.green);
        //GameScene.I.LineMng.DrawLine(transform.position, RallyPoint, Color.blue);
    }
    public void CreateDone()
    {       
        GameScene.I.ActorMng.CreateUnit(DeQueue(), respawnPosition, Owner.GetComponent<Building>().m_team).
                GetComponent<Unit>()?.Move(RallyPoint);
    }
    private bool isEnoughCost(Enums.UnitType type)
    {
        ProduceUnitData data = table.GetItem(type);

        if (data == null)
        {
            Debug.Log(" 해당 UnitType의 정보를 확인할 수 없습니다.");
            return false;
        }
        var food = data.food;
        var wood = data.wood;
        var pop = data.Pop;

        if (GameScene.I.WealthMng.GetCount(WealthData.WEALTH_TYPE.Food) >= food &&
            GameScene.I.WealthMng.GetCount(WealthData.WEALTH_TYPE.Wood) >= wood)
            return true;
        else
        {
            GameScene.I.GameEvent.Publish(RTS_EventSystem.RtsEventType.LACK_RESOURCES);
            return false;
        }
    }   
}

/// <summary>
/// 직접적인 유닛 생산을 담당함
/// </summary>
public class Unit_Creator :Subject
{       
    private readonly Building_Produce building;

    private float Timer = -1f;
    private float ProductTime = 0;    

    public float Ratio { get { return Timer / ProductTime; } }
    public bool isFinish { get { return Timer >= ProductTime; } }
    Enums.UnitType CurrentType;
    public Unit_Creator(Building_Produce produce)
    {
        building = produce;              
    }    
    public void Push(Enums.UnitType type )
    {                
        Timer = 0f;
        ProductTime = DataManager.Instance.produceUnitTable.GetItem(type).Time;

        CurrentType = type;                                   
    }
    public void Create()
    {
        if (isFinish)
            return;
               
        Timer += Time.deltaTime;
        Notify();
    }    
    public void Finish()
    {
        Timer = 1.0f;
    }       
}