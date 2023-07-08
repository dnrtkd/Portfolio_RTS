using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeHeap 
{
    private List<Node> nodes = new();

    public int Count { get { return nodes.Count; } }  
    public bool Empty { get { return Count <= 0; } }
    public Node First
    {
        get
        {
            if (!Empty)
                return nodes[0];
            return null;
        }
    }

    public void Push(Node node)
    {
        this.nodes.Add(node);
        this.nodes.Sort();
    }
    public void Remove(Node node)
    {
        this.nodes.Remove(node);
        this.nodes.Sort();
    }
    

    public bool Constains(Node node)
    {
        return nodes.Contains(node);
    }

    
}
