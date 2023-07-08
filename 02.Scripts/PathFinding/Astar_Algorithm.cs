using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Astar_Algorithm 
{
    /// <summary>
    /// 유클리드 거리
    /// </summary>    
    private static float HeristicEstimateCost(Node curNode, Node goalNode)
    {
        Vector3 cost = curNode.position - goalNode.position;
        return cost.magnitude;
    }
    
    public static List<Node> GetNeighbours(Node node , NormalGrid grid)
    {
        Vector3 nodePos = node.position;
        int nodeIndex = grid.GetGridIndex(nodePos);

        int row = grid.GetRow(nodeIndex);
        int column = grid.GetColumn(nodeIndex);

        List<Node> nodes=new();
        nodes.Add( AssignNeighbour(row - 1, column,grid));
        nodes.Add( AssignNeighbour(row + 1, column,grid));
        nodes.Add( AssignNeighbour(row , column - 1,grid));
        nodes.Add( AssignNeighbour(row , column + 1,grid));

        nodes.Add( AssignNeighbour(row - 1 , column -1,grid));
        nodes.Add( AssignNeighbour(row -1, column + 1,grid));
        nodes.Add( AssignNeighbour(row +1, column - 1,grid));
        nodes.Add( AssignNeighbour(row +1, column + 1,grid));

        return nodes;
    }


    public static Node AssignNeighbour(int row, int column,NormalGrid grid)
    {
        if (row < 0 || column < 0 || row >= grid.Height || column >= grid.Width)
            return null;

        if (grid.cell[row, column] == false)
            return null;

        return new Node(grid.GetGridCellCenter(row,column));
    }
    /// <summary>
    /// 경로 집합을 반환함
    /// </summary>
    public static List<Vector3> FindPath(Vector3 startPos, Vector3 goalPos,NormalGrid grid)
    {
        Node start = new Node(grid.GetCellCenterWorld(startPos));
        Node goal = new Node(grid.GetCellCenterWorld(goalPos));

        NodeHeap OpenHeap = new();
        NodeHeap CloseHeap = new();

        OpenHeap.Push(start);
        start.nodeTotalCost = 0.0f;
        start.estimatedCost = HeristicEstimateCost(start, goal);

        Node node = null;

        while(OpenHeap.Empty == false)
        {
            node = OpenHeap.First;

            if (node.position == goal.position)
                return CalculatePath(node);

            List<Node> neighbours = GetNeighbours(node, grid);

            for(int i=0;i<neighbours.Count;++i)
            {
                Node neighbourNode = neighbours[i];
                if (CloseHeap.Constains(neighbourNode))
                    continue;

                float cost = HeristicEstimateCost(node, neighbourNode);

                float totalCost = node.nodeTotalCost + cost;
                float EstCost = HeristicEstimateCost(neighbourNode, goal);

                
            }
        }


        return null;
    }
    
    private static List<Vector3> CalculatePath(Node node)
    {
        List<Vector3> list = new();
        while(node != null)
        {
            list.Add(node.position);
            node = node.parent;
        }
        list.Reverse();
        return list;
    }

}
