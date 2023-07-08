using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class MapManager : Base_Manager
{
    // 선택된 맵
    private RTS_Map m_map; 
    //맵 부품
    public RTS_MapPartCollections blocks;
    public RTS_MapPartCollections environment;

    GameObject rootBlock;
    public NormalGrid grid;
    BuildingManager build;
    ActorManager actor;
    public float mapSize;
    Transform enivronmentRoot;
    public override void Init()
    {
        grid = new NormalGrid();        
        rootBlock = new GameObject("Blocks");
        build=GetComponent<BuildingManager>();
        actor = GetComponent<ActorManager>();
        enivronmentRoot = new GameObject("environmentRoot").GetComponent<Transform>();
    }
    public void SetMapFile(RTS_Map _map)
    {
        m_map = _map;

        grid.SetSize(m_map.MapSize);
        grid.CellSize(1);

        mapSize = m_map.MapSize.x;
    }
    public override void OnUpdate()
    {
        
    }
    public override void Clear()
    {
        
    }
    public void CreateMap()
    {
        if (m_map == null)
            return;

        grid.SetSize(m_map.MapSize);
        grid.CellSize(1);

        var mapParts = m_map.mapParts;

        foreach(var item in mapParts)
        {
            InstantiatePart(GetMapPartType(item.id), item.pos, item.id,(Enums.TEAM)item.team);
        }

        //네비메쉬 베이크
        var surface = GetComponent<NavMeshSurface>();        
        surface.BuildNavMesh();
        

        //그라운드 콜리더 생성 및 사이즈 조절

        GameObject ground= new GameObject("Ground");
        ground.transform.Rotate(new Vector3(0f,45f,0f));
        ground.layer = LayerMask.NameToLayer("Ground");
        float size = Mathf.Cos(45 / 180 * Mathf.PI)*mapSize * 1.4f;

        ground.transform.position = new Vector3(mapSize, 0, mapSize)*0.5f;
        var box = ground.AddComponent<BoxCollider>();
        box.size = new Vector3(size, 0.2f, size);        
    }
    public void InstantiatePart(Enums.MapPartType type,Vector3 cellPos, int id,Enums.TEAM team)
    {
        if (!grid.IsInBounds(cellPos))
            return;
        if(type==Enums.MapPartType.Block)
        {
            GameObject go = GameObject.Instantiate(blocks.GetItem(id).targetObject,rootBlock.transform);
            go.transform.position = cellPos+new Vector3(0,-0.3f,0);            
            grid.CellOn(cellPos);
        }
        if(type==Enums.MapPartType.environment|type==Enums.MapPartType.Actor)
        {
            RTS_MapPartObject partObject;
            var pos = cellPos + new Vector3(0, 0.3f, 0);
            if (type == Enums.MapPartType.environment)
            {
                partObject = environment.GetItem(id);
                GameObject go;
                if (partObject.id < 105)
                {
                    go = GameObject.Instantiate(partObject.targetObject);
                    go.transform.position += pos;
                }
                else
                    go = actor.CreateResource(partObject.targetObject.name, pos);
                FillCell(partObject,pos);
                go.transform.parent = enivronmentRoot;
            }
            else if (type == Enums.MapPartType.Actor)
            {
                if (id >= 251)
                    actor.CreateUnit((Enums.UnitType)id - 251, pos, team);
                else    
                    build.immediateBuild((Enums.BuildingType)id-201, pos, team);
            }
        }
    }    
    void FillCell(RTS_MapPartObject selectItem, Vector3 cellPos)
    {
        int size = selectItem.size.x / 2;
        for (int i = -size; i < size + 1; ++i)
            for (int j = -size; j < size + 1; ++j)
            {
                var pos = cellPos + new Vector3(i, 0, j);
                int gridIndex = grid.GetGridIndex(pos);
                int colum = grid.GetColumn(gridIndex);
                int row = grid.GetRow(gridIndex);
                grid.cell[colum, row] = !grid.cell[colum, row];
            }
    }
    Enums.MapPartType GetMapPartType(int id)
    {
        Enums.MapPartType temp;
        if (id < (int)Enums.MapPartType.environment)
            temp = Enums.MapPartType.Block;
        else if (id < (int)Enums.MapPartType.Actor)
            temp = Enums.MapPartType.environment;
        else
            temp = Enums.MapPartType.Actor;
        return temp;
    }

}
