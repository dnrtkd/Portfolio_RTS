using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "RTS_Map/create MapData")]
public class RTS_Map : ScriptableObject
{
    public string Name;
    public Vector2Int MapSize;
    //그리드 위치 , 아이디
    public List<MapPartObject> mapParts = new List<MapPartObject>();
    public int maxPlayer;

    public PathData pathData;
}

[Serializable]
public class MapPartObject:IComparable<MapPartObject>
{ 
    public MapPartObject(Vector3 _pos,int _id,int _team= (int)Enums.TEAM.NONE)
    {
        pos = _pos;  id = _id; team=_team;
    }
    public Vector3 pos;
    public int id;
    public int team;
    public int CompareTo(MapPartObject other)
    {
        return id.CompareTo(other.id);
    }
}

[Serializable]
public class ActorPartObject : MapPartObject
{
    public ActorPartObject(Vector3 _pos, int _id, int _team=(int)Enums.TEAM.NONE):base(_pos,_id)
    {
        team = _team;
    }
       
}

[Serializable]
public class PathData
{   
    public PathData(Bounds bound, Vector3 position, Quaternion rotation)
    {
        sourceBounds = bound;
        this.position = position;
        this.rotation = rotation;
    }

    public Bounds sourceBounds;
    public Vector3 position;
    public Quaternion rotation;
}