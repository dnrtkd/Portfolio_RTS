using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UnitInfo
{
    public string Name;

    public int hp;
    public int attack;
    public int defence;

    public float attackRange;
    public float speed;

    public int food;
    public int wood;

    public int population;
}

[System.Serializable]
public class BuildingInfo
{
    public string Name;
    public string KorName;
    public int hp;
    public int attack;
    public int defence;

    public float attackRange;

    public int food;
    public int wood;
}

public interface ILoader<Key, Value>
{
    Dictionary<Key, Value> MakeDict();
}

public class UnitInfoList : ILoader<string,UnitInfo>
{
    public List<UnitInfo> unitInfos = new List<UnitInfo>();

    public Dictionary<string, UnitInfo> MakeDict()
    {
        Dictionary<string, UnitInfo> unitInfoDict = new Dictionary<string, UnitInfo>();
        foreach (UnitInfo unitInfo in unitInfos)
        {
            unitInfoDict.Add(unitInfo.Name, unitInfo);
        }
        return unitInfoDict;
    }
}

public class BuildingInfoList : ILoader<string, BuildingInfo>
{
    public List<BuildingInfo> buildingInfos = new List<BuildingInfo>();

    public Dictionary<string, BuildingInfo> MakeDict()
    {
        Dictionary<string, BuildingInfo> buildingDict = new Dictionary<string, BuildingInfo>();
        foreach (BuildingInfo buildingInfo in buildingInfos)
        {
            buildingDict.Add(buildingInfo.Name, buildingInfo);
        }
        return buildingDict;
    }
}


public class DataManager : Singleton<DataManager>
{
    public  Dictionary<string, UnitInfo> UnitInfoTable { get; private set; } = new Dictionary<string, UnitInfo>();
    public  Dictionary<string, BuildingInfo> BuildingInfoTable { get; private set; } = new Dictionary<string, BuildingInfo>();

    public ActorDataTable unitDataTable;
    public ActorDataTable buildingDataTable;
    public StructBuildingTable structTable;
    public ProduceUnitTable produceUnitTable;
    public void Init()
    {
        UnitInfoTable = LoadJson<UnitInfoList, string, UnitInfo>("UnitInfoTable").MakeDict();
        BuildingInfoTable = LoadJson<BuildingInfoList, string, BuildingInfo>("BuildingInfoTable").MakeDict();
    }

    Loader LoadJson<Loader, Key, Value>(string path) where Loader : ILoader<Key, Value>
    {
        TextAsset textAsset =ResUtil.Load<TextAsset>($"Data/{path}");
        return JsonUtility.FromJson<Loader>(textAsset.text);
    }
}
