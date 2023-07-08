using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public abstract class MapToolState 
{
    protected MapToolWindow m_editorWindow;
    public MapToolState(MapToolWindow editorWindow)
    {
        m_editorWindow = editorWindow;
    }
    public abstract void DrawGUI();
    public abstract void Init();
    public abstract void Release();
    public abstract void onUpdate();

}
