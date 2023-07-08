using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/StructBuildingTable")]
public class StructBuildingTable : ScriptableObject
{
    public List<StructBuildingData> Items = new List<StructBuildingData>();
    public StructBuildingData GetItem(Enums.BuildingType type)
    {
        return Items.Find(t => t.type == type);
    }
}
[System.Serializable]
public class StructBuildingData
{
    // 기본키
    public Enums.BuildingType type;

    //필요한 자원
    public int wood;
    public int food;

    //건축 시간
    public int Time;

    public int size;

    public GameObject targetObj;
}

