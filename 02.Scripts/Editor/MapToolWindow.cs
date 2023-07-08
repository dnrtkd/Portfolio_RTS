using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public enum EditMode
{
    None=0,
    intro,
    MapCreate,
    Edit,
}
public class MapToolWindow : EditorWindow
{
    EditMode currentEditState;
    public RTS_Map targetMap;
    Vector2 scrollPosition;
    Dictionary<EditMode, MapToolState> EditStateCollctions=new Dictionary<EditMode, MapToolState>();

    #region 메뉴 추가
    [MenuItem("Useful_Tool/MapTool %t" )]
    static void Open()
    {
        var window = GetWindow<MapToolWindow>();
        window.titleContent = new GUIContent("MapTool");
    }
    #endregion
    #region 유니티 호출 함수

    /*
     * 에디터가 클릭되었을 때 호출됨
     */
    private void OnEnable()
    {
        currentEditState = EditMode.intro;
        EditStateCollctions[EditMode.intro] = new MapToolState_Intro(this);
        EditStateCollctions[EditMode.intro].Init();
        EditStateCollctions[EditMode.MapCreate] = new MapToolState_CreateMap(this);
        EditStateCollctions[EditMode.Edit] = new MapToolState_Edit(this);
    }
    /*
    * 에디터 윈도우가 포커스 상태가 아니게 되었을 때 호출됨
    */
    private void OnDisable()
    {
        foreach (var state in EditStateCollctions)
            state.Value.Release();
    }

    string mapName;
    Vector2Int mapSize;
    int maxPlayer;
    private void OnGUI()
    {
        EditStateCollctions[currentEditState].DrawGUI();
    }
    private void Update()
    {        
        if (currentEditState == EditMode.Edit)
        {
            EditStateCollctions[currentEditState].onUpdate();
        }
            
    }

    #endregion
    public void ChangeState(EditMode editMode)
    {
        EditStateCollctions[currentEditState].Release();
        currentEditState = editMode;
        EditStateCollctions[currentEditState].Init();
    }
}
