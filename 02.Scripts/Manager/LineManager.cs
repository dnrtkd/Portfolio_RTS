using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//선을 그리고, 출력을 담당함
public class LineManager : Base_Manager
{
    public event Action OnLineDraw = null;
    public Material material;

    private Material lineMaterial;
    private void CreateLineMaterial()
    {
        if (!lineMaterial)
        {
            // Unity에는 그리기에 유용한 빌트인 셰이더가 있습니다.
            // 단순한 색상의 것들.
            Shader shader = Shader.Find("Hidden/Internal-Colored");
            lineMaterial = new Material(shader);
            lineMaterial.hideFlags = HideFlags.HideAndDontSave;
            // 알파 블렌딩을 켭니다.
            lineMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            lineMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            // 뒷면 컬링을 끕니다.
            lineMaterial.SetInt("_Cull", (int)UnityEngine.Rendering.CullMode.Off);
            // 깊이 쓰기 끄기
            lineMaterial.SetInt("_ZWrite", 0);
        }
    }


    private void OnRenderObject()
    {
        CreateLineMaterial();

        lineMaterial.SetPass(0);

        GL.PushMatrix();
        if (OnLineDraw != null)
        { 
            OnLineDraw.Invoke();
        }
        GL.PopMatrix();
    }

    public override void Init()
    {
       
    }
    public override void OnUpdate()
    {
       
    }
    public override void Clear()
    {
        OnLineDraw = null;
    }
   
    public void DrawCircle(Vector3 center, float radius, Color color)
    {
        float increment = 0.01f;
        GL.Begin(GL.LINES);
        GL.Color(color);        
        for (float rad=0.0f; rad < (Mathf.PI*2); rad+=increment )
        {
            float cos = Mathf.Cos(rad);
            float sin = Mathf.Sin(rad);
            GL.Vertex(new Vector3(cos,0,sin)*radius+center);
        }
        GL.End();
    }
    public void DottedLine (Vector3 start, Vector3 end , Color color)
    {
        GL.Begin(GL.LINES);
        GL.Color(color);

        for (float ratio = 0.0f; ratio <= 1.0f; ratio += 0.1f)
            GL.Vertex( Vector3.Lerp(start, end, ratio));
        GL.End();
    }
    public void DrawLine(Vector3 start, Vector3 end,Color color)
    {
        Debug.Log(color);
        GL.Begin(GL.LINES);
        GL.Color(color);        
        GL.Vertex(start);  GL.Vertex(end);
        GL.End();
    }
    public void DrawRactangle(Vector3[] points,Color color)
    {
        Debug.Log(color);
        GL.Begin(GL.LINES);
        GL.Color(color);        
        for (int i = 0; i < points.Length-1; i++)
        {
            GL.Vertex(points[i]);
            GL.Vertex(points[i + 1]);           
        }
        GL.Vertex(points[3]);
        GL.Vertex(points[0]);
        GL.End();        
    }

    public void DrawRactangle2(Vector3[] points,Color color)
    {
        GL.Begin(GL.QUADS);
        GL.Color(color);

        for (int i = 0; i < points.Length - 1; i++)
        {
            GL.Vertex(points[i]);
            GL.Vertex(points[i + 1]);
        }
        GL.Vertex(points[3]);
        GL.Vertex(points[0]);
        GL.End();
    }

    public void DrawRactangle(Building building, Color color)
    {
        Vector3[] positions = new Vector3[4];
        positions[0] = building.GetStartPosition();
        positions[1] = positions[0] + new Vector3(building.SizeX, 0, 0);
        positions[2] = positions[1] + new Vector3( 0, 0,building.SizeZ);
        positions[3] = positions[2] + new Vector3(-building.SizeX,0, 0);

        DrawRactangle(positions, color);
        
    }






}
