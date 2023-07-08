using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : Base_Manager
{
    public enum CameraType
    {
        MAIN,
        MINIMAP,       
    }
    [SerializeField]
    Camera m_mainCam;
    [SerializeField]
    Camera m_minimapCam;

    LineManager m_lineManager;

    [Range(1, 10)]
    [SerializeField]
    float sensitivity;

    //카메라와 지면의 거리
    float distance;

    //카메라의 방향
    Vector3 dir;
  
    //카메라 뷰의 각 꼭짓점 
    Vector3[] positions = new Vector3[4];
    public override void Init()
    {
        m_lineManager = GetComponent<LineManager>();
        m_lineManager.OnLineDraw += DrawViewLine;        
    }
    public void SetCameraProperties()
    {
        //뷰포트의 가운데로 레이를 쏜다
        Ray ray = m_mainCam.ScreenPointToRay(new Vector3(0.5f, 0.5f, 0));
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, LayerMask.GetMask("Ground")))
        {
            distance = Vector3.Distance(m_mainCam.transform.position, hit.point);
            dir = (m_mainCam.transform.position - hit.point).normalized;

            Debug.Log(distance);
            Debug.Log(dir.ToString());
        }
    }
    public override void OnUpdate()
    {        
        MainCamMoveByMouse();

        positions=GetViewPoints(positions);
    }
    public override void Clear()
    {
        //m_lineManager.OnLineDraw -= DrawViewLine;
    }
    Vector3 reff=Vector3.zero;
    private void MainCamMoveByMouse()
    {
        float value = Time.deltaTime * sensitivity;

        if (m_mainCam.ScreenToViewportPoint(Input.mousePosition).x <= 0.01)
            m_mainCam.transform.position += new Vector3(-1, 0, -1) * value;
        if (m_mainCam.ScreenToViewportPoint(Input.mousePosition).x >= 0.99)
            m_mainCam.transform.position += new Vector3(1, 0, 1)  * value;
        if (m_mainCam.ScreenToViewportPoint(Input.mousePosition).y <= 0.01)
            m_mainCam.transform.position += new Vector3(1, 0, -1) * value;
        if (m_mainCam.ScreenToViewportPoint(Input.mousePosition).y >= 0.99)
            m_mainCam.transform.position += new Vector3(-1, 0, 1) * value;
    }
    private Vector3[] GetViewPoints(Vector3[] points)
    {
        RaycastHit hit;
        Ray ray = m_mainCam.ScreenPointToRay(new Vector3(0, 0, 0));
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Ground")))
            points[0] = hit.point + new Vector3(0, 1, 0);

        ray = m_mainCam.ScreenPointToRay(new Vector3(Screen.width, 0, 0));
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Ground")))
            points[1] = hit.point + new Vector3(0, 1, 0);
        
        ray = m_mainCam.ScreenPointToRay(new Vector3(Screen.width, Screen.height, 0));
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Ground")))
            points[2] = hit.point + new Vector3(0, 1, 0);

        ray = m_mainCam.ScreenPointToRay(new Vector3(0, Screen.height, 0));
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Ground")))
            points[3] = hit.point + new Vector3(0, 1, 0);
        return points;
    }
    private void DrawViewLine()
    {
        if (Camera.current.gameObject.CompareTag("MainCamera"))
            return;        
        m_lineManager.DrawRactangle(positions, Color.white);
    }
    //미니맵 카메라 size변경
    public void SetMapOthoSize(float _size, CameraType type = CameraType.MAIN)
    {
        if (type == CameraType.MINIMAP)
            m_minimapCam.orthographicSize = _size;
        else if (type == CameraType.MAIN)
            m_mainCam.orthographicSize = _size;
    }
    //미니맵 카메라나 메인 카메라의 위치 변경
    public void SetCameraPos(Vector3 pos,CameraType type=CameraType.MAIN)
    {
        if (type == CameraType.MINIMAP)
            m_minimapCam.transform.position = pos;
        else if (type == CameraType.MAIN)
        {
            m_mainCam.transform.position = pos;
            SetCameraProperties();
        }
    }

    public void PlaceAvoveActor(Actor actor)
    {
        Vector3 position = actor.Position;
        PlaceCamera(position);
    }

    public void PlaceCamera(Vector3 position)
    {
        m_mainCam.transform.position
            = position +  (dir * distance); 
    }    

    public IEnumerator SmoothCameraMove(Vector3 des, float time)
    {
        Vector3 vel = Vector3.zero;
        time = Mathf.Clamp(time, 0.3f, 2.0f);

        float offset = 0.05f;
        while((des.x-offset>=m_mainCam.transform.position.x ||
            des.z-offset >=m_mainCam.transform.position.z) ||
            (des.x + offset <= m_mainCam.transform.position.x ||
            des.z + offset <= m_mainCam.transform.position.z))
        {
            m_mainCam.transform.position
                = Vector3.SmoothDamp(m_mainCam.transform.position, des,ref vel, time);

            yield return null;
        }
        m_mainCam.transform.position = des;

        yield break;
    }    
}
