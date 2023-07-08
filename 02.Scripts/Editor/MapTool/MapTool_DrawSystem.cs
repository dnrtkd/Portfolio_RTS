using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTool_DrawSystem 
{
    NormalGrid m_grid;
    RTS_MapPartCollections partCollec;
    //선택된 오브젝트
    GameObject selectedObject;
    public MapTool_DrawSystem(NormalGrid _grid)
    {
        m_grid = _grid;
    }

    public void Draw()
    { }

   
}
