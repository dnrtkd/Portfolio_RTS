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
    // �⺻Ű
    public Enums.BuildingType type;

    //�ʿ��� �ڿ�
    public int wood;
    public int food;

    //���� �ð�
    public int Time;

    public int size;

    public GameObject targetObj;
}

