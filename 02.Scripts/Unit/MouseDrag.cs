using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseDrag : MonoBehaviour
{
    [SerializeField]
    private RectTransform dragRectangle;

    private Rect dragRect;
    private Vector2 start = Vector2.zero;
    private Vector2 end = Vector2.zero;

    private Camera mainCamera;
    private GroupCtrl groupCtrl;

    private void Awake()
    {
        mainCamera = Camera.main;
        groupCtrl = GetComponent<GroupCtrl>();

        DrawDragRectangle();
        
    }

    private void Start()
    {
        GameScene.I.MouseMng.OnMouseEvent += OnStartDrag;
        GameScene.I.MouseMng.OnMouseEvent += OnDrag;
        GameScene.I.MouseMng.OnMouseEvent += OnDragEnd;
    }

    void OnStartDrag(Enums.MouseEvent mouseEvent)
    {
        if (mouseEvent != Enums.MouseEvent.DragStart)
            return;

        start = Input.mousePosition;
        dragRect = new Rect();
    }
    void OnDrag(Enums.MouseEvent mouseEvent)
    {
        if (mouseEvent != Enums.MouseEvent.Drag)
            return;

        end = Input.mousePosition;
        DrawDragRectangle();
    }

    void OnDragEnd(Enums.MouseEvent mouseEvent)
    {
        if (mouseEvent != Enums.MouseEvent.DragEnd)
            return;
        groupCtrl.DeselectAll();
        CalculateDragRect();
        SelectUnits();
        start = end = Vector2.zero;
        DrawDragRectangle();
    }
    
    void DrawDragRectangle()
    {
        dragRectangle.position = (start + end) * 0.5f;
        dragRectangle.sizeDelta = new Vector2(Mathf.Abs(start.x - end.x)
            , Mathf.Abs(start.y - end.y));
    }
    void CalculateDragRect()
    {
        if (Input.mousePosition.x < start.x)
        {
            dragRect.xMin = Input.mousePosition.x;
            dragRect.xMax = start.x;
        }
        else
        {
            dragRect.xMin = start.x;
            dragRect.xMax = Input.mousePosition.x;
        }
        if (Input.mousePosition.y < start.y)
        {
            dragRect.yMin = Input.mousePosition.y;
            dragRect.yMax = start.y;
        }
        else
        {
            dragRect.yMin = start.y;
            dragRect.yMax = Input.mousePosition.y;
        }
    }
    void SelectUnits()
    {
        foreach (var actor in groupCtrl.AllActors)
        {
            if (dragRect.Contains(
                mainCamera.WorldToScreenPoint(actor.transform.position)) &&
                actor.m_team == Enums.TEAM.PLAYER)
            {
                groupCtrl.SelectActor(actor.GetComponent<ActorCtrl>());
            }
        }
    }

   
}
