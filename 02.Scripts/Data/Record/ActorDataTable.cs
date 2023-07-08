using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/ActorDataTable")]
public class ActorDataTable : ScriptableObject
{
    public List<ActorData> actorDatas = new();

    public ActorData GetItem(int id)
    {
        return actorDatas.Find(t => t.id == id);
    }
}







