using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ��ã��� ��� Ŭ����
/// 
/// </summary>
public class Node : System.IComparable<Node>
{
    // G��
    public float nodeTotalCost;
    // H��
    public float estimatedCost;
    public bool bObstacle;
    public Node parent;
    public Vector3 position;

    public Node()
    {
        estimatedCost = 0.0f;
        nodeTotalCost = 1.0f;
        bObstacle = false;
        parent = null;
    }

    public Node(Vector3 pos)
    {
        estimatedCost = 0.0f;
        nodeTotalCost = 1.0f;
        bObstacle = false;
        parent = null;
        position = pos;
    }

    public void MarkAsObstacle()
    {
        bObstacle = true;
    }
    public int CompareTo(  Node other)
    {
        //�޸���ƽ���� ���� ������� ����
        if (this.estimatedCost < other.estimatedCost)
            return -1;
        if (this.estimatedCost > other.estimatedCost)
            return 1;
        return 0;
    }
}
