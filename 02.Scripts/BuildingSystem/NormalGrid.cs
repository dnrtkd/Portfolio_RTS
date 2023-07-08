using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalGrid
{
    private int numOfRows;
    private int numOfColumns;
    private float gridCellSize;
    public bool showGrid;
    public bool[,] cell;
    private Vector3 origin;
    public int Width { get { return numOfColumns; } }
    public int Height { get { return numOfRows; } }
    public void SetSize(Vector2Int size)
    {
        numOfColumns = size.x; numOfRows = size.y;
        SetSize(numOfColumns, numOfRows);
    }
    public void SetSize(int x, int y)
    {
        cell = new bool[x, y];
        origin = Vector3.zero;
    }
    public void CellSize(float cellSize)
    {
        gridCellSize = cellSize;
    }
    /*
     * cell 의 시작위치를 반환
     * 
     */
    private Vector3 GetGridCellPosition(int index)
    {
        int row = GetRow(index);
        int col = GetColumn(index);        
        return GetGridCellPosition(row, col);
    }
    private Vector3 GetGridCellPosition(int row, int col)
    {
        float xPosInGrid = col * gridCellSize;
        float zPosInGrid = row * gridCellSize;

        return origin + new Vector3(xPosInGrid, 0.0f, zPosInGrid);
    }
    /*
     * 셀의 중앙위치를 반환
     * 
     */
    public Vector3 GetGridCellCenter(int index)
    {
        Vector3 cellPosition = GetGridCellPosition(index);
        cellPosition.x += (gridCellSize / 2.0f);
        cellPosition.z += (gridCellSize / 2.0f);

        return cellPosition;
    }
    public Vector3 GetGridCellCenter(int row, int col)
    {
        return GetGridCellPosition(row, col) +
            new Vector3(gridCellSize / 2.0f, 0, gridCellSize / 2.0f);
    }
    public Vector3 GetCellCenterWorld(Vector3 pos)
    {
        int index = GetGridIndex(pos);
        return GetGridCellCenter(index);
    }    
    public int GetGridIndex(Vector3 pos)
    {
        if (!IsInBounds(pos))
            return -1;
        pos -= origin;
        int col = (int)(pos.x / gridCellSize);
        int row = (int)(pos.z / gridCellSize);
        return (row * numOfColumns + col);
    } 
   
    public bool IsInBounds(Vector3 pos)
    {
        float width = numOfColumns * gridCellSize;
        float height = numOfRows * gridCellSize;

        return (pos.x >= origin.x && pos.x <= origin.x + width &&
             pos.z <= origin.z + height && pos.z >= origin.z);
    }
    public int GetRow(int index)
    {
        return index / numOfColumns;
    }

    public int GetColumn(int index)
    {
        return index % numOfColumns;
    }    
    public bool getCellValue(Vector3 pos)
    {
        int index = GetGridIndex(pos);
        return cell[GetColumn(index), GetRow(index)];
    }
    public void CellOn(Vector3 pos)
    {
        if(!IsInBounds(pos))
        {
            return;
        }
        int index = GetGridIndex(pos);
        cell[GetColumn(index),GetRow(index)]=true;
    }

    public void CellOff(Vector3 pos)
    {
        if (!IsInBounds(pos))
        {            
            return;
        }
        int index = GetGridIndex(pos);
        cell[GetColumn(index), GetRow(index)] = false;
    }
    public void DebugDrawGrid(Vector3 origin, int numRows,
        int numCols, float cellSize, Color color)
    {
        float width = (numCols * cellSize);
        float height = (numRows * cellSize);

        for (int i = 0; i < numRows + 1; i++)
        {
            Vector3 startPos = origin + (i * cellSize * new Vector3(0.0f, 0.0f, 1.0f));
            Vector3 endPos = startPos + width * new Vector3(1.0f, 0.0f, 0.0f);
            Debug.DrawLine(startPos, endPos, color);
        }
        for (int i = 0; i < numCols + 1; i++)
        {
            Vector3 startPos = origin + (i * cellSize * new Vector3(1.0f, 0.0f, 0.0f));
            Vector3 endPos = startPos + height * new Vector3(0.0f, 0.0f, 1.0f);
            Debug.DrawLine(startPos, endPos, color);
        }
    }

}
