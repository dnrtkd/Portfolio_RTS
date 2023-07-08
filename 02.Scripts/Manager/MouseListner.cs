using UnityEngine;
using System;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class MouseListner :Base_Manager
{    
    public event Action<Enums.MouseEvent> OnMouseEvent = null;    
    
    List<string> MouseEventTable= new List<string>();

    Vector3 DragStart;
    bool isDragging;

    public override void Init()
    {
        
    }
    public override void OnUpdate()
    {
        if (OnMouseEvent != null)
        {
            OnMouseEvent.Invoke(Enums.MouseEvent.OnCurssor);

            if (Input.GetMouseButtonDown(0))
            {
                OnMouseEvent.Invoke(Enums.MouseEvent.DragStart);
                DragStart = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            }
            if (Input.GetMouseButton(0))
            {
                if (isDragging)
                    OnMouseEvent.Invoke(Enums.MouseEvent.Drag);
                else
                {
                    Vector3 currMousePoint = Camera.main.ScreenToViewportPoint(Input.mousePosition);
                    if (Mathf.Abs(DragStart.x - currMousePoint.x) > 0.02 || Mathf.Abs(DragStart.y - currMousePoint.y) > 0.02)
                        isDragging = true;
                }
            }
            if (Input.GetMouseButtonUp(0))
            {
                if (isDragging)
                {
                    OnMouseEvent.Invoke(Enums.MouseEvent.DragEnd);
                    isDragging = false;
                }
                else
                    OnMouseEvent.Invoke(Enums.MouseEvent.LeftClick);
            }
            if (EventSystem.current.IsPointerOverGameObject() == false &&
                Input.GetMouseButtonUp(1))
                OnMouseEvent.Invoke(Enums.MouseEvent.RightClick);
        }
    }
    public override void Clear()
    {
        
    }    
}

