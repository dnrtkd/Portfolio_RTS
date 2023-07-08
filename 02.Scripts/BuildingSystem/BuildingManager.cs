using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//건물 빌드에 관한 클래스
public class BuildingManager : Base_Manager
{
    private PlacebleObject m_objectToPlace;
    MouseListner Input;
    ActorManager Actor;
    LineManager Line;
    MapManager Map;
    
    //농부가 빌딩시스템에 접속
    Farmer farmer;
    
    public RTS_MapPartCollections BuildingModel;
    public override void Init()
    {
        Input = GetComponent<MouseListner>();
        Actor = GetComponent<ActorManager>();
        Map = GetComponent<MapManager>();
        Line = GetComponent<LineManager>();
                
        Input.OnMouseEvent += BuildingRightClick;       
    }
    public override void OnUpdate()
    {        
        if (m_objectToPlace != null)
        {
            bool isPlaceble = true;
            if (CanBePlaced(m_objectToPlace, m_objectToPlace.targetObj.transform.position))
                isPlaceble = true;
            else
                isPlaceble = false;
            m_objectToPlace.OnUpdate(isPlaceble);
        }        
    }
    public override void Clear()
    {        
        Input.OnMouseEvent -= BuildingRightClick;
    }
    //즉시 완료
    public void immediateBuild(Enums.BuildingType type, Vector3 pos,Enums.TEAM team)
    {
        GameObject obj = Actor.CreateBuilding(type, pos,team);
        Building _placeble = obj.GetComponent<Building>();
        obj.transform.position = Map.grid.GetCellCenterWorld(pos);
        FillCell(_placeble.SizeX,pos);
        _placeble.changeBuildingShpae(3);
        _placeble.SetHp(_placeble.MaxHp);
        _placeble.isComplete = true;
    }
    public void BuildingLeftClick(Vector3 pos)
    {        
        if (CanBePlaced(m_objectToPlace ,  m_objectToPlace.targetObj.transform.position))
        {            
            farmer.ConstructBuilding(m_objectToPlace.Position, m_objectToPlace);
            m_objectToPlace.isPlace = true;
            //Line.OnLineDraw -= DrawGrid;
            m_objectToPlace = null;
        }
        else
        {
            CanclePlaceble();            
        }

        MouseClick.Instance.SetState(MouseClick.ClickState.Normal);
        farmer = null;
    }
    void BuildingRightClick(Enums.MouseEvent type)
    {
        if (m_objectToPlace == null || type != Enums.MouseEvent.RightClick)
            return;

        CanclePlaceble();
    }
    void CanclePlaceble()
    {
        //Line.OnLineDraw -= DrawGrid;
        m_objectToPlace.Destory();
        m_objectToPlace = null;
        farmer = null;
    }
    //건물 모델을 생성하여 마우스 포인트에 장착
    public void InitializeWithObject(Enums.BuildingType type ,Farmer farmer,WealthDataCollection wealth)
    {
        if (m_objectToPlace != null)
            return;

        if (isEnoughCost(type, wealth) == false)
            return;

        this.farmer = farmer;
        
        StructBuildingData data = DataManager.Instance.structTable.GetItem(type);
        m_objectToPlace = new PlacebleObject(data);
    }
    //건물 생성을 생성하고 ProcessBuilding 스크립트 반환
    public ProcessBuilding ProvideBuilding(PlacebleObject obj ,WealthDataCollection Wealth ,Enums.TEAM team)
    {
        if (obj == null)
            return null;

        if (CanBePlaced(obj, obj.Position) == false ||
            isEnoughCost(obj.Type, Wealth)==false)
        {
            GameScene.I.GameEvent.Publish(RTS_EventSystem.RtsEventType.LACK_RESOURCES);
            return null;            
        }
        else
        {
            FillCell(obj.size, obj.Position);
            Wealth.SubCount(WealthData.WEALTH_TYPE.Wood, obj.wood);
            Wealth.SubCount(WealthData.WEALTH_TYPE.Food, obj.food);

            var Process = Actor.CreateBuilding(obj.Type, obj.Position, team).AddComponent<ProcessBuilding>();
            Process?.Init();
            obj.Destory();
            return Process;            
        }
    }
    bool isEnoughCost(Enums.BuildingType type,WealthDataCollection wealth)
    {
        StructBuildingData data= DataManager.Instance.structTable.GetItem(type);

        if( data==null)
        {
            Debug.Log(" 해당 BuildingType의 정보를 확인할 수 없습니다.");
            return false;
        }    
        var food = data.food;
        var wood = data.wood;

        if (wealth.GetCount(WealthData.WEALTH_TYPE.Food) >= food && 
            wealth.GetCount(WealthData.WEALTH_TYPE.Wood) >= wood)
            return true;
        else
            return false;
    }
    bool CanBePlaced(PlacebleObject obj,Vector3 _pos)
    {
        int size = obj.size / 2;
        for (int i = -size; i < size + 1; ++i)
            for (int j = -size; j < size + 1; ++j)
            {
                var pos = _pos + new Vector3(i, 0, j);
                if (Map.grid.IsInBounds(pos) == false)
                    return false;
                int gridIndex = Map.grid.GetGridIndex(pos);
                int colum = Map.grid.GetColumn(gridIndex);
                int row = Map.grid.GetRow(gridIndex);
                if (Map.grid.cell[colum, row] == false)
                    return false;
            }
        return true;
    }
    //void DrawGrid()
    //{
    //    Vector3 origin = m_objectToPlace.GetStartPosition();
    //    int sizeX = m_objectToPlace.SizeX;
    //    int sizeZ = m_objectToPlace.SizeZ;

    //    for (int i = 0; i < sizeZ + 1; i++)
    //    {
    //        Vector3 startPos = origin + (i  * new Vector3(0.0f, 0.0f, 1.0f));
    //        Vector3 endPos = startPos + sizeX * new Vector3(1.0f, 0.0f, 0.0f);
    //        Line.DrawLine(startPos, endPos, Color.black);
    //    }
    //    for (int i = 0; i < sizeX + 1; i++)
    //    {
    //        Vector3 startPos = origin + (i * new Vector3(1.0f, 0.0f, 0.0f));
    //        Vector3 endPos = startPos + sizeZ * new Vector3(0.0f, 0.0f, 1.0f);
    //        Line.DrawLine(startPos, endPos, Color.black);
    //    }
    //}
    public void FillCell(int _size,Vector3 cellPos)
    {
        int size = _size / 2;
        for (int i = -size; i < size + 1; ++i)
            for (int j = -size; j < size + 1; ++j)
            {
                var pos = cellPos + new Vector3(i, 0, j);
                int gridIndex = Map.grid.GetGridIndex(pos);
                int colum = Map.grid.GetColumn(gridIndex);
                int row = Map.grid.GetRow(gridIndex);
                Map.grid.cell[colum, row] = !Map.grid.cell[colum, row];
            }
    }
    public void OnkeyBoardBuilding()
    {

    }
}
