using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 일꾼이 건물을 짓기위한 정보 집합
/// </summary>
public class PlacebleObject
{      
    public Enums.BuildingType Type;
    public int size;
    public int wood;
    public int food;

    public bool isPlace;
    public GameObject targetObj;

    private MeshRenderer renderer;
    private Color OriginColor;
    public Vector3 Position { get; private set; }    
    public PlacebleObject(StructBuildingData data)
    {
        Type = data.type; size = data.size; wood = data.wood; food = data.food;
        if (data.targetObj == null)
            return;

        targetObj=Object.Instantiate(data.targetObj);
        renderer = targetObj.GetComponent<MeshRenderer>();
        OriginColor = renderer.material.color;
    }
    public void Destory()
    {
        Object.Destroy(targetObj);
    }
    public void SetOn()
    {
        if(targetObj!=null)
            targetObj.SetActive(true);
    }
    public void SetOff()
    {
        if (targetObj != null)
            targetObj.SetActive(false);
    }    
    private void MouseDrag()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit raycastHit;
        Vector3 pos;
        if (Physics.Raycast(ray, out raycastHit,100 ,1<<LayerMask.NameToLayer("Ground"),QueryTriggerInteraction.Ignore))
            pos = raycastHit.point;
        else
            pos = Vector3.zero;

        targetObj.transform.position = GameScene.I.MapMngP.grid.GetCellCenterWorld(pos);        
        Position = targetObj.transform.position;
    }   
    public void OnUpdate(bool isPlaceble)
    {
        if (isPlace || targetObj==null)
            return;

        if(isPlaceble == false)
        {
            renderer.material.color = Color.red;
        }
        else
        {
            renderer.material.color = OriginColor;
        }

        MouseDrag();
    }
}
