using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MapToolState_CreateMap : MapToolState
{
    public MapToolState_CreateMap(MapToolWindow mapToolWindow) : base(mapToolWindow) { }

    string mapName;
    Vector2Int mapSize;
    int maxPeopleNum;

    public override void Init()
    {
        
    }
    public override void Release()
    {
        
    }
    public override void DrawGUI()
    {
        ToolUtillity.DrawCenterLabel(new GUIContent("�� �����ϱ�"), Color.blue, 20, FontStyle.BoldAndItalic);

        GUILayout.BeginVertical(GUI.skin.box);
        {
            GUILayout.Space(10);             
            mapName=EditorGUILayout.TextField(" �� �̸� ",mapName);
            GUILayout.Space(10);
            mapSize = EditorGUILayout.Vector2IntField(" �� ������ ", mapSize);
            GUILayout.Space(10);
            maxPeopleNum = EditorGUILayout.IntField(" �� �ο� ", maxPeopleNum);
            GUILayout.Space(30);

            GUILayout.BeginHorizontal();
            {
                GUILayout.FlexibleSpace();
                GUI.enabled = (string.IsNullOrEmpty(mapName) == true || mapSize.x <= 0 || mapSize.y <= 0 || maxPeopleNum <= 0) ? false : true;
                if (GUILayout.Button(" �� ���� ", GUILayout.Width(m_editorWindow.position.width * 0.25f)))
                {
                    RTS_Map map= ScriptableObject.CreateInstance<RTS_Map>();
                    map.Name = mapName;
                    map.MapSize = mapSize;
                    map.maxPlayer = maxPeopleNum;
                    m_editorWindow.targetMap = map;
                    ToolUtillity.SaveMapData(mapName, map);
                    m_editorWindow.ChangeState(EditMode.intro);
                }
                GUILayout.FlexibleSpace();
            }
            GUILayout.EndHorizontal();
        }
        GUILayout.EndVertical();

       


    }
    public override void onUpdate()
    {
    }
}
