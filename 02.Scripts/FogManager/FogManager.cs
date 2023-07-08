using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 전장의 안개를 관리하는 Manager
/// </summary>
public class FogManager : Base_Manager
{
    [SerializeField]
    private Material fogMaterial;

    private int fogWidth;
    private int fogHeight;

    private List<Actor> actors;
    private NormalGrid fogGrid=new();

    private Texture2D texBuffer;
    private Color[] colors;
    private bool[] visited;

    public override void Init()
    {
        //int size = (int)GameScene.I.MapMngP.mapSize;
        int size = 100;

        fogWidth = size; fogHeight = size;        
        texBuffer = new Texture2D(fogWidth, fogHeight, TextureFormat.ARGB32, false);
        texBuffer.wrapMode = TextureWrapMode.Clamp;

        fogGrid.SetSize(new Vector2Int(size, size));
        fogGrid.CellSize(1.0f);
        colors = new Color[size * size];
        visited = new bool[size * size];

        for (int i = 0; i < colors.Length; i++) colors[i].a = 1.0f;
        texBuffer.SetPixels(colors);
        texBuffer.Apply();

        fogMaterial.SetTexture("_MainTex", texBuffer);

        CreateFogPrefab();

        actors = GameScene.I.ActorMng.Actors;

        StartCoroutine(CoUpdateFogTexture());        
    }
    /// <summary>
    ///  매 프레임마다 fog 텍스쳐를 유닛의 위치에 기반하여 업데이트함.
    /// </summary>
    /// 
    IEnumerator CoUpdateFogTexture()
    {
        while(true)
        {
            for (int i = 0; i < visited.Length; ++i)
                visited[i] = false;

            UpdateFogTexture();            

            yield return new WaitForSeconds(0.5f);

            foreach (var actor in actors)
            {
                if (actor.m_team == Enums.TEAM.PLAYER)
                    continue;

                if (visited[fogGrid.GetGridIndex(actor.Position)] == false &&
                    visited[fogGrid.GetGridIndex(actor.Position + new Vector3(1, 0, 1))] == false &&
                    visited[fogGrid.GetGridIndex(actor.Position + new Vector3(1, 0, 0))] == false &&
                    visited[fogGrid.GetGridIndex(actor.Position + new Vector3(0, 0, 1))] == false &&
                    visited[fogGrid.GetGridIndex(actor.Position + new Vector3(-1, 0, -1))] == false &&
                    visited[fogGrid.GetGridIndex(actor.Position + new Vector3(-1, 0, 1))] == false &&
                    visited[fogGrid.GetGridIndex(actor.Position + new Vector3(1, 0, -1))] == false &&
                    visited[fogGrid.GetGridIndex(actor.Position + new Vector3(-1, 0, 0))] == false &&
                    visited[fogGrid.GetGridIndex(actor.Position + new Vector3(0, 0, -1))] == false) ;             
                    //actor.vision.RendereOff();                                    
            }
            for (int i = 0; i < visited.Length; ++i)
                if (visited[i] == false && colors[i].a == 0.0f)
                    colors[i].a = 0.7f;
        }
    }    

    void UpdateFogTexture()
    {
        //for (int i = 0; i < visited.Length; ++i)
        //    visited[i] = false;
        foreach(var actor in actors)
        {
            if (actor.m_team != Enums.TEAM.PLAYER)
                continue;
            
            UnitSight((int)actor.ActorInfo.field, fogGrid.GetCellCenterWorld(actor.transform.position));                       
        }
        //for (int i = 0; i < visited.Length; ++i)
        //    if (visited[i] == false && colors[i].a == 0.0f)
        //        colors[i].a = 0.7f;

        texBuffer.SetPixels(colors);
        texBuffer.Apply();        
        
        fogMaterial.SetTexture("_MainTex", texBuffer);


        void UnitSight(int _feild, Vector3 cellPos)
        {
            int radius = _feild;

            for(int x=-radius; x<=radius; ++x)
                for(int y=-radius; y<=radius; ++y)
                {
                    Vector3 pos = new Vector3(x, 0, y);
                    if (pos.sqrMagnitude > radius * radius)
                        continue;
                    pos += cellPos;
                    int gridIndex = fogGrid.GetGridIndex(pos);
                    if (gridIndex == -1)
                        continue;
                    if (visited[gridIndex] == true)
                        continue;
                    colors[gridIndex].a = 0.0f;
                    visited[gridIndex] = true;
                }

            //int size = _feild / 2;
            //for (int i = -size; i < size + 1; ++i)
            //    for (int j = -size; j < size + 1; ++j)
            //    {
            //        var pos = cellPos + new Vector3(i, 0, j);
            //        int gridIndex = fogGrid.GetGridIndex(pos);
            //        if (gridIndex == -1)
            //            continue;
            //        if (visited[gridIndex] == true)
            //            continue;
            //        colors[gridIndex].a = 0.0f;
            //        visited[gridIndex] = true;
            //    }
        }
    }
    /// <summary>
    /// 맵과 동일한 크기의 plane을 생성함. 포그메터리얼 적용
    /// </summary>
    void CreateFogPrefab()
    {        
        GameObject Fog = GameObject.CreatePrimitive(PrimitiveType.Plane);
        Fog.name = "Fog";        
        
        Fog.transform.position = new Vector3(fogWidth, 0, fogHeight) * 0.5f;
        Fog.transform.localScale = new Vector3(-fogWidth*0.1f, 1f,- fogHeight*0.1f);
       
        Fog.GetComponent<MeshRenderer>().material = fogMaterial;
    }    
    public override void Clear()
    {
        
    }    
    public override void OnUpdate()
    {        
        //UpdateFogTexture();
    }
}
