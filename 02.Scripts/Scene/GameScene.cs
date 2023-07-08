using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameScene : BaseScene
{
    [SerializeField]
    RTS_Map Map;

    [SerializeField]
    UI_StateBar stateBar;

    ActorManager m_actorManager;
    CameraManager m_cameraManager;
    MouseListner m_mouseListner;
    LineManager m_lineManager;
    MapManager m_mapManager;
    HpBarManager m_hpBarManager;
    BuildingManager m_buildingManager;
    WealthDataCollection m_wealth;

    private static GameScene m_instance;
    public static GameScene I 
    {
        get
        {
            if(m_instance==null)
            {
                Debug.Log("현재 씬은 게임씬이 아닙니다.");
                return null;
            }
            return m_instance;
        }
    }
    public ActorManager ActorMng { get { return m_actorManager; } }
    public CameraManager CameraMng { get { return m_cameraManager; } }
    public MouseListner MouseMng { get { return m_mouseListner; } }
    public LineManager LineMng { get { return m_lineManager; } }
    public MapManager MapMngP { get { return m_mapManager; } }
    public HpBarManager HpBarMng { get { return m_hpBarManager; } }
    public BuildingManager BuildMng { get { return m_buildingManager; } }
    public WealthDataCollection WealthMng { get { return m_wealth; } }
    public RTS_EventSystem GameEvent;

    public void Awake()
    {
        m_instance = this;     
    }

    public void Start()
    {
        Init();
    }

    public override void Init()
    {
        m_actorManager = GetComponent<ActorManager>();
        m_cameraManager = GetComponent<CameraManager>();
        m_mouseListner = GetComponent<MouseListner>();
        //m_lineManager = GetComponent<LineManager>();
        m_lineManager = FindObjectOfType<LineManager>();
        m_mapManager = GetComponent<MapManager>();
        m_hpBarManager = GetComponent<HpBarManager>();
        m_buildingManager = GetComponent<BuildingManager>();
        m_wealth = GetComponent<WealthDataCollection>();

        base.Init();        

        //맵 초기화
        m_mapManager.SetMapFile(MapFileLoder.RTS_MapLoad());
        m_mapManager.CreateMap();

        //카메라 세팅
        m_cameraManager.SetCameraPos(new Vector3(m_mapManager.mapSize, 10, m_mapManager.mapSize)*0.5f, CameraManager.CameraType.MINIMAP);        
        m_cameraManager.SetCameraPos(new Vector3(m_mapManager.mapSize, 80, m_mapManager.mapSize) * 0.5f);
        m_cameraManager.SetMapOthoSize(m_mapManager.mapSize * 0.4f,CameraManager.CameraType.MINIMAP);                

        m_wealth.AddCount(WealthData.WEALTH_TYPE.Food, 3000);
        m_wealth.AddCount(WealthData.WEALTH_TYPE.Wood, 3000);
        m_wealth.AddCount(WealthData.WEALTH_TYPE.Pop, 0);

        stateBar.Connect(m_wealth);
        
        m_cameraManager.PlaceAvoveActor
            (GameObject.FindGameObjectWithTag("Starting").GetComponent<Actor>());        
        //InvokeRepeating("Test", 5, 15);

        //Invoke("Test", 5.0f);
        //StartCoroutine(m_cameraManager.SmoothCameraMove(new Vector3(0, 10, 0), 2.0f));        
    }   
    void Test()
    {
        for (int i = 0; i < 5; i++)
        {
            ActorMng.CreateUnit(Enums.UnitType.sword, new Vector3(90, 0, 90), Enums.TEAM.ENMEY).
                GetComponent<Unit>().Attack(new Vector3(25, 0, 25));

            ActorMng.CreateUnit(Enums.UnitType.lance, new Vector3(90, 0, 90), Enums.TEAM.ENMEY).
                GetComponent<Unit>().Attack(new Vector3(25, 0, 25));
        }

        //StartCoroutine(m_cameraManager.SmoothCameraMove(new Vector3(0, 10, 0), 2.0f));
    }
    
    public override void OnUpdate()
    {
        base.OnUpdate();        
    }
}
