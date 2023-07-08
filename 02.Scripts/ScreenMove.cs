using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenMove : MonoBehaviour
{
    Camera mainCam;
    Vector3[] positions=new Vector3[4];
    [SerializeField]
    private LayerMask layerGround;
    private void Start()
    {
        mainCam = Camera.main;
        GameScene.I.LineMng.OnLineDraw += DrawOutLine;
    }
    void Update()
    {
        RaycastHit hit;
        Ray ray = mainCam.ScreenPointToRay(new Vector3(0, 0, 0));
        if (Physics.Raycast(ray, out  hit, Mathf.Infinity, layerGround))
            positions[0] = hit.point+new Vector3(0,1,0);

        ray = mainCam.ScreenPointToRay(new Vector3(Screen.width, 0, 0));
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerGround))
            positions[1] = hit.point + new Vector3(0, 1, 0);

        ray = mainCam.ScreenPointToRay(new Vector3(Screen.width, Screen.height, 0));
        if (Physics.Raycast(ray, out  hit, Mathf.Infinity, layerGround))
            positions[2] = hit.point + new Vector3(0, 1, 0);

        ray = mainCam.ScreenPointToRay(new Vector3(0, Screen.height, 0));
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerGround))
            positions[3] = hit.point + new Vector3(0, 1, 0);

        float sizeX = positions[1].x - positions[0].x;
        float sizeZ = positions[3].z - positions[0].z;
        Vector3 center = new Vector3(0, 0, 0);
        ray = mainCam.ScreenPointToRay(new Vector3(Screen.width , Screen.height,0)/2);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerGround))
            center = hit.point;

        if (mainCam.ScreenToViewportPoint(Input.mousePosition).x <= 0.05)
            transform.position += new Vector3(-1, 0, -1)*Time.deltaTime*10;
        if (mainCam.ScreenToViewportPoint(Input.mousePosition).x >= 0.95)
            transform.position += new Vector3(1, 0, 1) * Time.deltaTime * 10;
        if (mainCam.ScreenToViewportPoint(Input.mousePosition).y <= 0.05)
            transform.position += new Vector3(1, 0, -1) * Time.deltaTime * 10;
        if (mainCam.ScreenToViewportPoint(Input.mousePosition).y >= 0.95)
            transform.position += new Vector3(-1, 0, 1) * Time.deltaTime * 10;
    }

    void DrawOutLine()
    {
        if (Camera.current.gameObject.CompareTag("MainCamera"))
            return;
        GameScene.I.LineMng.DrawRactangle(positions, Color.green);
    }
}
