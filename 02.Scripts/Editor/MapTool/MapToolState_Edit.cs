using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;
using System;
using System.IO;
using UnityEngine.AI;
enum ToolType
{
    brush,
    eraser
}
enum BlockBrushSize
{
    size1x1,
    size3x3
}
public class MapToolState_Edit : MapToolState
{
    string sceneName;
    Enums.MapPartType selectMapPartType;
    Vector2 scrollPos;
    Dictionary<Enums.MapPartType, RTS_MapPartCollections> mapPartCollections = new Dictionary<Enums.MapPartType, RTS_MapPartCollections>();
    GameObject gridObject;
    NormalGrid grid;
    LayerMask gridLayer;
    int selectedID;
    ToolType toolType;
    Camera screenCamera;
    RenderTexture renderTexture;
    BlockBrushSize blockBrushSize;
    Enums.TEAM team;
    Material redUnitMat;
    Material redBuildingMat;
    Dictionary<Vector3, Tuple<int, GameObject>> blocks = new Dictionary<Vector3, Tuple<int, GameObject>>();
    Dictionary<Vector3, Tuple<int,GameObject,int>> mapObject=new Dictionary<Vector3, Tuple<int, GameObject,int>>();

    GameObject Preview;
    NavMeshSurface surface;
    public MapToolState_Edit(MapToolWindow toolWindow) : base(toolWindow) { }
    public override void Init()
    {
        redBuildingMat = AssetDatabase.LoadAssetAtPath<Material>("Assets/Resources/TT_RTS_buildings_red.mat");
        redUnitMat = AssetDatabase.LoadAssetAtPath<Material>("Assets/Resources/TT_RTS_Units_red.mat");

        selectMapPartType = Enums.MapPartType.Block;
        mapPartCollections[Enums.MapPartType.Block] = AssetDatabase.LoadAssetAtPath<RTS_MapPartCollections>("Assets/Resources/Data/MapPartCollections/Block_Collections.asset");
        mapPartCollections[Enums.MapPartType.environment] = AssetDatabase.LoadAssetAtPath<RTS_MapPartCollections>("Assets/Resources/Data/MapPartCollections/environment_coll.asset");
        mapPartCollections[Enums.MapPartType.Actor] = AssetDatabase.LoadAssetAtPath<RTS_MapPartCollections>("Assets/Resources/Data/MapPartCollections/Actor_coll.asset");

        sceneName = SceneManager.GetActiveScene().name;
        EditorSceneManager.OpenScene("Assets/01.Scenes/CreateMap.unity");

        gridObject = new GameObject("Grid");
        gridLayer= LayerMask.NameToLayer("Ground");
        gridObject.layer = gridLayer;
        var box = gridObject.AddComponent<BoxCollider>();
        box.center = new Vector3(m_editorWindow.targetMap.MapSize.x / 2, 0, m_editorWindow.targetMap.MapSize.x / 2);
        box.size= new Vector3(m_editorWindow.targetMap.MapSize.x , 0.1f, m_editorWindow.targetMap.MapSize.x );

        grid = new NormalGrid();
        grid.showGrid = true;
        grid.SetSize(m_editorWindow.targetMap.MapSize);
        grid.CellSize(1);

        surface = GameObject.FindObjectOfType<NavMeshSurface>();

        toolType = ToolType.brush;
        SceneView.duringSceneGui -= OnSceneGUI;
        SceneView.duringSceneGui += OnSceneGUI;

        if(m_editorWindow.targetMap.mapParts!=null)
        {
            foreach(var item in m_editorWindow.targetMap.mapParts)
            {
                int id = item.id;
                var pos = item.pos;

                if (GetMapPartType(id) == Enums.MapPartType.Block)
                    PaintBlock(pos, id);
                else if(GetMapPartType(id)==Enums.MapPartType.Actor)
                    PaintObject(GetMapPartType(id), pos, id,(Enums.TEAM)item.team);
                else
                    PaintObject(GetMapPartType(id), pos, id);
            }
        }
        SceneView.lastActiveSceneView.orthographic = true;
        SceneView.lastActiveSceneView.LookAt(new Vector3(m_editorWindow.targetMap.MapSize.x / 2, 0, m_editorWindow.targetMap.MapSize.y / 2));
        selectedID = -1;
        
        // **********  스크린샷 ************//
        screenCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        screenCamera.transform.position = new Vector3(m_editorWindow.targetMap.MapSize.x * 0.5f, 10, m_editorWindow.targetMap.MapSize.y * 0.5f) ;
        screenCamera.orthographicSize = m_editorWindow.targetMap.MapSize.x*0.4f;
        renderTexture = new RenderTexture(256, 256, 0);
        screenCamera.targetTexture = renderTexture;

        blockBrushSize = BlockBrushSize.size1x1;
    }
    int toolbarId;
    string[] toolbarStrings= { "브러쉬","지우개" };
    int toolbarId2;
    string[] toolbarStrings2 = { "Size 1 * 1","Size 3 * 3" };
    int toolbarId3;
    string[] toolbarStrings3 = { "플레이어", "적군" };
    public override void DrawGUI()
    {   
        ToolUtillity.DrawCenterLabel(new GUIContent("맵 편집하기"), Color.yellow, 20, FontStyle.BoldAndItalic);

        GUILayout.BeginVertical();
        {
            GUILayout.BeginHorizontal(EditorStyles.toolbar);
            {
                if (GUILayout.Button("블록", EditorStyles.toolbarButton))
                    selectMapPartType = Enums.MapPartType.Block;
                if (GUILayout.Button("환경", EditorStyles.toolbarButton))
                    selectMapPartType = Enums.MapPartType.environment;
                if (GUILayout.Button("상호작용 객체", EditorStyles.toolbarButton))
                    selectMapPartType = Enums.MapPartType.Actor;
            }
            GUILayout.EndHorizontal();

            GUILayout.Space(10);

            GUILayout.BeginHorizontal( GUILayout.MinHeight(50));
            {
                GUILayout.FlexibleSpace();
                toolbarId = GUILayout.Toolbar(toolbarId, toolbarStrings, GUILayout.MinHeight(50), GUILayout.MinWidth(50));
                toolType = (ToolType)toolbarId;
                GUILayout.FlexibleSpace();
            }
            GUILayout.EndHorizontal();
            GUILayout.Space(10);

            if(selectMapPartType==Enums.MapPartType.Block)
            {
                GUILayout.BeginHorizontal(GUILayout.MinHeight(30));
                {
                    GUILayout.FlexibleSpace();
                    toolbarId2 = GUILayout.Toolbar(toolbarId2, toolbarStrings2, GUILayout.MinHeight(30), GUILayout.MinWidth(30));
                    blockBrushSize = (BlockBrushSize)toolbarId2;
                    GUILayout.FlexibleSpace();
                }
                GUILayout.EndHorizontal();
            }
            else if(selectMapPartType==Enums.MapPartType.Actor)
            {
                GUILayout.BeginHorizontal(GUILayout.MinHeight(30));
                {
                    GUILayout.FlexibleSpace();
                    toolbarId3 = GUILayout.Toolbar(toolbarId3, toolbarStrings3, GUILayout.MinHeight(30), GUILayout.MinWidth(30));
                    team = (Enums.TEAM)toolbarId3+1;
                    GUILayout.FlexibleSpace();
                }
                GUILayout.EndHorizontal();
            }
        }
        GUILayout.EndVertical();

        
        var lastRect= GUILayoutUtility.GetLastRect();
        var area = new Rect(50, lastRect.yMax+200,m_editorWindow.position.width-100, m_editorWindow.position.height - lastRect.yMax - 400);

        // @@@@@ 맵 파트 목록 @@@@@@@@@
        GUILayout.BeginArea(area, GUI.skin.window);
        {
            scrollPos = GUILayout.BeginScrollView(scrollPos);
            {
                GUILayout.BeginHorizontal();
                {
                    foreach (var item in mapPartCollections[selectMapPartType].Items)
                    {
                        if (DrawMapPart(new Vector2(100, 100), selectedID==item.id, item))
                        {                        
                            if(Preview != null)
                            {
                                GameObject.DestroyImmediate(Preview.gameObject);
                                Preview = null;
                            }

                            Preview = GameObject.Instantiate(item.targetObject);
                            Preview.AddComponent<ObjectDrag>();
                            selectedID = item.id;
                        }

                        GUILayout.Space(10);
                    }
                }
                GUILayout.EndHorizontal();
            }
            GUILayout.EndScrollView();
        }
        GUILayout.EndArea();

        area = new Rect(100, area.y+area.height+110, m_editorWindow.position.width - 200, m_editorWindow.position.height -(area.yMax + area.height - 100) - 100);
        GUILayout.BeginArea(area);
        {
            GUILayout.BeginHorizontal();
            {
                GUILayout.FlexibleSpace();
                if (GUILayout.Button("저 장", GUILayout.Width(200), GUILayout.MinHeight(50)))
                {
                    save();
                }
                GUILayout.FlexibleSpace();
            }
            GUILayout.EndHorizontal();                                  
        }
        GUILayout.EndArea();
    }
    public override void Release()
    {
        if (gridObject != null)
            UnityEngine.Object.DestroyImmediate(gridObject);

        SceneView.duringSceneGui -= OnSceneGUI;
        if (!string.IsNullOrEmpty(sceneName))
            EditorSceneManager.OpenScene($"Assets/01.Scenes/{sceneName}.unity");

        SceneView.lastActiveSceneView.orthographic = false;
    }
    public override void onUpdate()
    {
        SceneView.lastActiveSceneView.Repaint();        
    }

    bool blockPaintFlag;
    public void OnSceneGUI(SceneView sv)
    {
        var mousePos = Event.current.mousePosition;
        var ray = HandleUtility.GUIPointToWorldRay(mousePos);

        Handles.BeginGUI();
        {
            var rtBox = new Rect((sv.position.x+sv.position.width)-220, 20, 200, 240);
            var rtTex = new Rect(rtBox.x + 10, rtBox.y + 20, 170, 200);

            GUI.Box(rtBox, GUIContent.none, GUI.skin.window);
            GUI.Label(new Rect(rtBox.x + 10, rtBox.y + 5, 180, 10), m_editorWindow.targetMap.Name,EditorStyles.boldLabel);
            GUI.DrawTexture(rtTex, renderTexture);
        }
        Handles.EndGUI();

        if (Physics.Raycast(ray, out var hitInfo, Mathf.Infinity, ~gridLayer))
        {
            if (Preview != null)
            {
                Preview.transform.position = grid.GetCellCenterWorld(hitInfo.point);
            }
        }
        
        


        else return;
        if (!grid.IsInBounds(hitInfo.point))
            return;
        var cellPos = grid.GetCellCenterWorld(hitInfo.point);

        if (Event.current.button == 0 && Event.current.type == EventType.MouseDown)
        {
            if(Enums.MapPartType.Actor == selectMapPartType || Enums.MapPartType.environment == selectMapPartType)
            {
                if (toolType == ToolType.brush)
                    PaintObject(selectMapPartType, cellPos, selectedID, team);
                else
                    EraseObject(hitInfo);
            }
            else if (Enums.MapPartType.Block == selectMapPartType)
                blockPaintFlag = !blockPaintFlag;
            Event.current.Use();

        }
        else if (Enums.MapPartType.Block == selectMapPartType )
        {           
            if(blockPaintFlag)
            {
                if (toolType == ToolType.brush)
                    //Paint(selectMapPartType, cellPos, selectedID);
                    PaintBlock(cellPos, selectedID, blockBrushSize);
                else
                    //Erase(cellPos);
                    EraseBlock(cellPos, blockBrushSize);
            }            
        }        

        if (Physics.Raycast(ray, out var hitInfo2, Mathf.Infinity, LayerMask.GetMask("building", "Unit")))
        {
            Handles.BeginGUI();
            {
                var previewTex = AssetPreview.GetAssetPreview(hitInfo2.transform.gameObject);

                var rtBox = new Rect(30, 30, previewTex.width + 10, previewTex.height + 10);
                var rtTex = new Rect(35, 35, previewTex.width, previewTex.height);

                GUI.Box(rtBox, GUIContent.none, GUI.skin.window);
                GUI.DrawTexture(rtTex, previewTex);
            }
            Handles.EndGUI();
        }        
    }
    void PaintBlock(Vector3 cellPos,int id, BlockBrushSize size=BlockBrushSize.size1x1)
    {
        if (!grid.IsInBounds(cellPos) || id == -1)
            return;

        if (blocks.ContainsKey(cellPos))
            return;
        
        if (size == BlockBrushSize.size1x1)
            CreateBlock(cellPos, id);
        else if (size == BlockBrushSize.size3x3)
            for (int i = -1; i < 2; i++)
                for (int j = -1; j < 2; j++)
                    CreateBlock(cellPos+new Vector3(i,0,j), id);
    }
    void CreateBlock(Vector3 cellPos,int id)
    {
        if (!grid.IsInBounds(cellPos) || blocks.ContainsKey(cellPos))
            return;

        GameObject go = GameObject.Instantiate(mapPartCollections[Enums.MapPartType.Block].GetItem(id).targetObject);

        if (go == null )
            return;

        go.transform.position = cellPos;
        blocks[cellPos] = new Tuple<int, GameObject>(id, go);
        grid.CellOn(cellPos);
    }
    void PaintObject(Enums.MapPartType type,Vector3 cellPos,int id,Enums.TEAM _team=Enums.TEAM.NONE)
    {
        if (!grid.IsInBounds(cellPos) ||id==-1)
            return;
        
        RTS_MapPartObject partObject = mapPartCollections[type].GetItem(id);
        GameObject go = GameObject.Instantiate(partObject.targetObject);

        if (go == null || partObject == null)
            return;
        if(type == Enums.MapPartType.Actor)
        {
            if(_team== Enums.TEAM.ENMEY)
            {
                var renderer = go.GetComponentsInChildren<Renderer>();

                if (go.layer == LayerMask.NameToLayer("building"))
                    foreach (var item in renderer)
                        item.material = redBuildingMat;
                else
                    foreach (var item in renderer)
                        item.material = redUnitMat;
            }                
        }

        go.transform.position = cellPos;

        if (CanBePlaced(partObject, cellPos))
        {
            FillCell(partObject, cellPos);

            mapObject[cellPos] = new Tuple<int, GameObject,int>(id, go,(int)_team);
            go.transform.position += new Vector3(0, 0.3f, 0);
        }
        else
            UnityEngine.Object.DestroyImmediate(go);        
    }
    void EraseBlock(Vector3 cellPos, BlockBrushSize size)
    {
        if (!grid.IsInBounds(cellPos) || !blocks.ContainsKey(cellPos))
            return;

        if (size == BlockBrushSize.size1x1)
            DestroyBlock(cellPos);
        else if (size == BlockBrushSize.size3x3)
            for (int i = -1; i < 2; i++)
                for (int j = -1; j < 2; j++)
                    DestroyBlock(cellPos + new Vector3(i, 0, j));

    }
    void DestroyBlock(Vector3 cellPos)
    {
        if ( !grid.IsInBounds(cellPos) || !blocks.ContainsKey(cellPos))
            return;
            /*
             * 블록 컨테이너에는 속해있으나 건물에의해 cell값이 false로 됬을 때는 지우지않는다.              
             */
        if (grid.getCellValue(cellPos) == false)
            return;
        else
        {
            UnityEngine.Object.DestroyImmediate(blocks[cellPos].Item2);
            blocks.Remove(cellPos);
            grid.CellOff(cellPos);
        }
    }
    void EraseObject( RaycastHit hit)
    {
        var cellPos = hit.transform.position - new Vector3(0, 0.3f, 0);
        if (mapObject.ContainsKey(cellPos) == false)
            return;

        RTS_MapPartObject partObject = mapPartCollections[GetMapPartType(mapObject[cellPos].Item1)].GetItem(mapObject[cellPos].Item1);
        FillCell(partObject, cellPos);
        UnityEngine.Object.DestroyImmediate(mapObject[cellPos].Item2);
        mapObject.Remove(cellPos);
    }
    bool DrawMapPart(Vector2 slotSize,bool isSelected,RTS_MapPartObject item)
    {
        var area = GUILayoutUtility.GetRect
            (slotSize.x, slotSize.y, GUIStyle.none, GUILayout.MaxWidth(slotSize.x), GUILayout.MaxHeight(slotSize.y));
        bool selected = GUI.Button(area, AssetPreview.GetAssetPreview(item.targetObject));
        GUI.Label(new Rect(area.center.x, area.center.y, 100, 50), item.name);

        if(isSelected)
        {
            var selectMarkArea = area;
            selectMarkArea.x = selectMarkArea.center.x - 30;
            selectMarkArea.width = 30;
            selectMarkArea.height = 30;
            GUI.DrawTexture(selectMarkArea, EditorGUIUtility.FindTexture("d_FilterSelectedOnly@2x"));
        }
        return selected;
    }
    bool CanBePlaced(RTS_MapPartObject selectItem, Vector3 cellPos)
    {
        int size = selectItem.size.x / 2;
        for (int i=-size;i<size+1;++i)
            for(int j=-size;j<size+1;++j)
            {
                var pos=cellPos+new Vector3(i, 0, j);
                if (grid.IsInBounds(pos ) == false)
                    return false;              
               int gridIndex = grid.GetGridIndex(pos);
               int colum = grid.GetColumn(gridIndex);
               int row = grid.GetRow(gridIndex);
                 if( grid.cell[colum,row] == false)
                    return false;   
            }
        return true;
    }
    void FillCell(RTS_MapPartObject selectItem,Vector3 cellPos)
    {
        int size = selectItem.size.x / 2;   
        for (int i = -size; i < size+1; ++i)
            for (int j = -size; j < size+1; ++j)
            {
                var pos =cellPos+ new Vector3(i, 0, j);
                int gridIndex = grid.GetGridIndex(pos);
                int colum = grid.GetColumn(gridIndex);
                int row = grid.GetRow(gridIndex);                
                grid.cell[colum,row] = !grid.cell[colum,row];
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
    void save()
    {        
        List<MapPartObject> temp = new List<MapPartObject>();
        foreach(var item in mapObject)
        {
            temp.Add(new MapPartObject(item.Key,item.Value.Item1,item.Value.Item3));
        }
        foreach(var item in blocks)
        {
            temp.Add(new MapPartObject(item.Key, item.Value.Item1));
        }
        temp.Sort();
        m_editorWindow.targetMap.mapParts = temp;

        ToolUtillity.SaveMapData(m_editorWindow.targetMap.Name, m_editorWindow.targetMap);
        
        Texture2D texture = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.RGB24, false);
        RenderTexture.active = renderTexture;
        texture.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        texture.Apply();
        
        File.WriteAllBytes($"{Application.dataPath}/Resources/Data/MapFolder/{m_editorWindow.targetMap.Name}.jpg"
            ,texture.EncodeToJPG());
        
        //AssetDatabase.CreateAsset();
    }  
}
