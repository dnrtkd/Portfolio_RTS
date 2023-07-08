using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public static class ToolUtillity 
{
    public static void DrawCenterLabel(GUIContent content,
        Color color,int fontSize,FontStyle fontStyle)
    {
        var guiStyle = new GUIStyle();
        guiStyle.fontSize = fontSize;
        guiStyle.fontStyle = fontStyle;
        guiStyle.normal.textColor = color;
        guiStyle.padding.top = 10;
        guiStyle.padding.bottom = 10;

        GUILayout.BeginHorizontal();
        {
            GUILayout.FlexibleSpace();
            GUILayout.Label(content, guiStyle);
            GUILayout.FlexibleSpace();
        }
        GUILayout.EndHorizontal();
    }
    /*
     * ¸ÊÆú´õ¿¡ ¸ÊÆÄÀÏ ÀúÀå
     * 
     */
    public static void SaveMapData(string fileName,RTS_Map mapFile)
    {
        if(AssetDatabase.Contains(mapFile))
        {
            RTS_Map map = ScriptableObject.CreateInstance<RTS_Map>();
            map.Name = mapFile.Name;
            map.MapSize = mapFile.MapSize;
            map.maxPlayer = mapFile.maxPlayer;
            map.mapParts = mapFile.mapParts;

            AssetDatabase.DeleteAsset("Assets/Resources/Data/MapFolder/" + fileName + ".asset");
            AssetDatabase.CreateAsset(map, "Assets/Resources/Data/MapFolder/" + fileName + ".asset");
        }
        else
        AssetDatabase.CreateAsset(mapFile, "Assets/Resources/Data/MapFolder/" + fileName + ".asset");
    }
    
}
