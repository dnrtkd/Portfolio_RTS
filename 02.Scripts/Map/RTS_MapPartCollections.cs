using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 맵을 만드는데 사용할 부품(나무, 블록, 유닛 , 건물)
 * 이 들어 있는 데이터 컨테이너
 * 
 * id 1~100 블록
 * id 101~200 
 */
[CreateAssetMenu(menuName = "RTS_Map/Create Part_Collections")]
public class RTS_MapPartCollections : ScriptableObject
{
    public List<RTS_MapPartObject> Items;
    public RTS_MapPartObject GetItem(int id)
    {
        return Items.Find(t => t.id == id);
    }
}
