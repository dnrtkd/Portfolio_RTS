using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * ���� ����µ� ����� ��ǰ(����, ���, ���� , �ǹ�)
 * �� ��� �ִ� ������ �����̳�
 * 
 * id 1~100 ���
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
