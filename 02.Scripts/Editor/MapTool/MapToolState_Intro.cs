using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class MapToolState_Intro : MapToolState
{
    RTS_Map targetMap;
    Texture2D icon;
    public MapToolState_Intro(MapToolWindow editorWindow) : base(editorWindow) { targetMap = editorWindow.targetMap; }
    public override void DrawGUI()
    {
        ToolUtillity.DrawCenterLabel(new GUIContent("∏  º±≈√«œ±‚"), Color.green, 20, FontStyle.BoldAndItalic);
        m_editorWindow.targetMap = EditorGUILayout.ObjectField("∏  : ", m_editorWindow.targetMap, typeof(RTS_Map)) as RTS_Map;

        var rect = GUILayoutUtility.GetLastRect();
        if (m_editorWindow.targetMap != null)
        {   
            if(m_editorWindow.targetMap.mapParts!=null)
                icon = AssetDatabase.LoadAssetAtPath<Texture2D>($"Assets/Resources/Data/MapFolder/{m_editorWindow.targetMap.Name}.jpg");  
            GUILayout.BeginHorizontal(GUI.skin.box);
            {
                GUILayout.BeginVertical();
                {
                    GUILayout.Label($"∏  ¿Ã∏ß : {m_editorWindow.targetMap.Name}");
                    GUILayout.Label($"∏  ªÁ¿Ã¡Ó : {m_editorWindow.targetMap.MapSize.x} * {m_editorWindow.targetMap.MapSize.y}");
                    GUILayout.Label($"{m_editorWindow.targetMap.maxPlayer} ¿ŒøÎ ∏ ");
                }
                GUILayout.EndVertical();
                GUILayout.FlexibleSpace();
                GUILayout.Box(icon,GUILayout.Width(300),GUILayout.Height(200));
            }
            GUILayout.EndHorizontal();                
        }
        GUILayout.BeginHorizontal();
        {
            GUILayout.FlexibleSpace();
            GUI.enabled = (m_editorWindow.targetMap == null) ? false : true;
            if (GUILayout.Button("º±≈√ «œ±‚") )
            {
                m_editorWindow.ChangeState(EditMode.Edit);
            }
            GUI.enabled = true;
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("∏  ªı∑Œ ª˝º∫«œ±‚"))
            {
                m_editorWindow.ChangeState(EditMode.MapCreate);
            }
            GUILayout.FlexibleSpace();
        }
        GUILayout.EndHorizontal();
    }
    public override void Init()
    {
        icon = new Texture2D(100, 100);
    }
    public override void Release()
    {
       
    }
    public override void onUpdate()
    {
        throw new System.NotImplementedException();
    }
}
