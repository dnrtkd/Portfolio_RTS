using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Data/ProduceUnittable")]
public class ProduceUnitTable : ScriptableObject
{
    public List<ProduceUnitData> Items = new List<ProduceUnitData>();
    public ProduceUnitData GetItem(Enums.UnitType type)
    {
        return Items.Find(t => t.type == type);
    }
}

[System.Serializable]
public class ProduceUnitData
{
    public Enums.UnitType type;
    public int wood;
    public int food;
    public int Pop;

    //생산 시간
    public float Time;
}
