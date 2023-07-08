using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDrag : MonoBehaviour
{
    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit raycastHit;
        Vector3 pos;
        if (Physics.Raycast(ray, out raycastHit))
            pos = raycastHit.point;
        else
            pos = Vector3.zero;

        transform.position = GameScene.I.MapMngP.grid.GetCellCenterWorld(pos);
    }
}
