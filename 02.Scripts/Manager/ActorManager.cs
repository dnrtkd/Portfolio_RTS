using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//생성된 오브젝트의 관리와 오브젝트의 생성을 담당
public class ActorManager : Base_Manager
{
    private ObjectPool<Actor> actorpool=new();
    public List<Actor> Actors = new();
    public List<ActorCtrl> ActorCtrls = new();
    Dictionary<string, GameObject> originalActors  = new Dictionary<string, GameObject>();
    public int actorID = 0;    
    private Transform rootUnit;
    private Transform rootBilding;
    //적 오브젝트의 메터리얼
    #region 메터리얼
    [SerializeField]
    Material enemyMatrial_Unit;
    [SerializeField]
    Material enemyMatrial_Build;
    [SerializeField]
    Material blueMaterial_Unit;
    [SerializeField]
    Material blueMatrial_Build;
    #endregion
    public override void Init()
    {        
        GameObject rootObject = new GameObject("RootObject");
        rootUnit = new GameObject("UnitObject").transform;
        rootUnit.parent = rootObject.transform;
        rootBilding = new GameObject("BuildingObject").transform;
        rootBilding.parent = rootObject.transform;

        var units = Resources.LoadAll<GameObject>("Prefab/Unit");
        for (int i = 0; i < units.Length; i++)
        {
            Debug.Log(units[i].name);
            originalActors.Add(units[i].name, units[i]);
        }
        var Buildings = Resources.LoadAll<GameObject>("Prefab/Building");
        for (int i = 0; i < Buildings.Length; i++)
        {
            originalActors.Add(Buildings[i].name, Buildings[i]);
        }
        var ResourcePrefabs = Resources.LoadAll<GameObject>("Prefab/Resource");
        for (int i = 0; i < ResourcePrefabs.Length; i++)
        {
            originalActors.Add(ResourcePrefabs[i].name, ResourcePrefabs[i]);
        }
    }
    public override void OnUpdate()
    {                
        actorpool.OnUpdate();
    }
    public override void Clear()
    {
        
    }
    public GameObject CreateUnit(Enums.UnitType unitType,Vector3 pos, Enums.TEAM _team)
    {
        string _path = ResUtil.EnumToString(unitType);

        var poolitem = actorpool.GetItem(_path);
        //풀에 오브젝트가 없으면
        if(poolitem == null)
        {
            GameObject obj = Instantiate(originalActors[_path]);
            if (obj == null)
            {
                Debug.Log($"ActorManager.CreateUnit : {_path}경로가 존재하지 않음");
                return null;
            }

            Unit unit = obj.GetComponent<Unit>();
            unit.InitActorRecord(DataManager.Instance.unitDataTable.GetItem((int)unitType));
            obj.AddComponent<UnitMove>();
            unit.m_team = _team;
            unit.Init();
            unit.type = unitType;
            unit.ID = actorID++;
            obj.transform.position = pos;
            obj.transform.parent = rootUnit;
            actorpool.Add(_path, unit);
            poolitem=actorpool.GetItem(_path);                                               
            AddCtrl(unit);
            if (_team == Enums.TEAM.ENMEY)
            {
                var renderer = poolitem.item.gameObject.GetComponentsInChildren<Renderer>();

                foreach (var item in renderer)
                    item.material = enemyMatrial_Unit;
            }
            else
            {
                var renderer = poolitem.item.gameObject.GetComponentsInChildren<Renderer>();

                foreach (var item in renderer)
                    item.material = blueMaterial_Unit;
            }
            poolitem.item.MinimpaPlane();
        }
        else
        {            
            poolitem.item.Open(pos, _team);
            if (_team == Enums.TEAM.ENMEY)
            {
                var renderer = poolitem.item.gameObject.GetComponentsInChildren<Renderer>();

                foreach (var item in renderer)
                    item.material = enemyMatrial_Unit;
            }
            else
            {
                var renderer = poolitem.item.gameObject.GetComponentsInChildren<Renderer>();

                foreach (var item in renderer)
                    item.material = blueMaterial_Unit;
            }
            poolitem.item.MinimpaPlane();
        }        
        Actors.Add(poolitem.item);
        return poolitem.item.gameObject;
    }    
    public GameObject CreateBuilding(Enums.BuildingType buildingType,Vector3 pos,Enums.TEAM _team)
    {
        string _path = ResUtil.EnumToString(buildingType);

        var poolitem = actorpool.GetItem(_path);

        if(poolitem==null)
        {
            GameObject go = Instantiate(originalActors[_path], pos, Quaternion.identity);

            if (go == null)
            {
                Debug.Log($"ActorManager.CreateUnit : {_path}경로가 존재하지 않음");
                return null;
            }

            go.transform.parent = rootBilding;

            Building building = go.GetComponent<Building>();
            ActorData buildingData = DataManager.Instance.buildingDataTable.GetItem((int)buildingType);
            building.InitActorRecord(buildingData);
            building.Init();
            building.SizeX = DataManager.Instance.structTable.GetItem(buildingType).size;
            building.SizeZ = DataManager.Instance.structTable.GetItem(buildingType).size;
            building.ProcessTime = DataManager.Instance.structTable.GetItem(buildingType).Time;
            building.ID = actorID++;
            actorpool.Add(_path, building);
            actorpool.GetItem(_path);
            Actors.Add(building);
            building.m_team = _team;
            if (building.m_team == Enums.TEAM.PLAYER)
            {
                AddCtrl(building);
            }
            else
            {
                var renderer = go.GetComponentsInChildren<Renderer>();

                foreach (var item in renderer)
                    item.material = enemyMatrial_Build;

            }
            building.MinimpaPlane();
            return go;
        }
        else
        {
            poolitem.item.Open(pos, _team);
            return poolitem.item.gameObject;
        }        
    }        
    public GameObject CreateResource(string _path,Vector3 pos)
    {
        GameObject obj = Instantiate(originalActors[_path]);
        if (obj == null)
        {
            Debug.Log($"ActorManager.CreateUnit : {_path}경로가 존재하지 않음");
            return null;
        }        
        obj.transform.position = pos;
        var resource = obj.GetComponent<Resource>();
        resource.Init();
        AddCtrl(resource);
        return obj;
    }
    public void AddCtrl(Actor _actor) 
    {
        var _actorCtrl = _actor.AddCtrlComponent();
        ActorCtrls.Add(_actorCtrl);
        _actorCtrl.Init();
    }
    /// <summary>    
    /// 가장 가까이에 있는 적팀 Actor객체를 반환함   
    /// </summary>
    public Actor FindNearTarget(Actor owner, float bound)
    {
        float minDis = float.MaxValue;
        Actor nearActor = null;

        for (int i = 0; i < Actors.Count; ++i)
        {
            Actor actor = Actors[i];
            if (actor == null)
                continue;
            if (actor == owner)
                continue;
            if (actor.m_team == owner.m_team)
                continue;
            if (actor.fsm.GetHighType == Actor.HighState.DIE)
                continue;

            float dis = Vector3.Distance(actor.Position, owner.Position);
            if (dis > bound)
                continue;
            if (dis < minDis)
            {
                minDis = dis;
                nearActor = actor;
            }
        }
        return nearActor;
    }    
    /// <summary>    
    /// 가장 가까이에 있는 적팀 Actor객체를 반환함   
    /// </summary>
    public Actor FindBuilding(Actor owner,Enums.BuildingType _type, float bound)
    {
        float minDis = float.MaxValue;
        Actor nearActor = null;

        for (int i = 0; i < Actors.Count; ++i)
        {
            Actor actor = Actors[i];
            if (actor == null)
                continue;
            if (actor == owner)
                continue;
            if (actor.m_team != owner.m_team)
                continue;
            if (actor.fsm.GetHighType == Actor.HighState.DIE)
                continue;

            float dis = Vector3.Distance(actor.Position, owner.Position);
            if (dis > bound  )
                continue;
            if(actor is Building)
            {
                if (dis < minDis && (actor as Building).type == _type)
                {
                    minDis = dis;
                    nearActor = actor;
                }
            }            
        }
        return nearActor;
    }
}
