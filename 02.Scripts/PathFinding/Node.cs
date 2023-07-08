using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 길찾기용 노드 클래스
/// 
/// </summary>
public class Node : System.IComparable<Node>
{
    // G값
    public float nodeTotalCost;
    // H값
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
        //휴리스틱값이 작은 순서대로 정렬
        if (this.estimatedCost < other.estimatedCost)
            return -1;
        if (this.estimatedCost > other.estimatedCost)
            return 1;
        return 0;
    }
}
