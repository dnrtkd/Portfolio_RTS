using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : Base_Manager
{
    public static CursorManager instance;

    [SerializeField]
    Texture2D NormalIcon;
    [SerializeField]
    Texture2D _attackIcon;
    [SerializeField]
    Texture2D _handIcon;
    [SerializeField]
    Texture2D _RallyIcon;

    [SerializeField]
    LayerMask unit;
    [SerializeField]
    LayerMask building;
    int mask = 0;
        
    public enum CursorType
    {        
        Attack,
        Hand,
        Normal,
        Rally,
    }

    CursorType cursorType;
    public void SetCursor(CursorType type)
    {
        if (cursorType == type)
            return;

        switch (type)
        {
            case CursorType.Normal:
                Cursor.SetCursor(NormalIcon, new Vector2(NormalIcon.width / 3, 0), CursorMode.ForceSoftware);                
                break;
            case CursorType.Attack:
                Cursor.SetCursor(_attackIcon, new Vector2(_attackIcon.width / 5, 0), CursorMode.ForceSoftware);
                break;
            case CursorType.Hand:
                Cursor.SetCursor(_handIcon, new Vector2(_attackIcon.width / 5, 0), CursorMode.ForceSoftware);
                break;
            case CursorType.Rally:
                Cursor.SetCursor(_RallyIcon, new Vector2(_RallyIcon.width / 5, 0), CursorMode.ForceSoftware);
                break;
            default:
                break;
        }
        cursorType = type;
    }
    public override void Clear()
    {
        
    }
    public override void Init()
    {
        instance = this;
        mask = unit | building;
        SetCursor(CursorType.Normal);
    }
    public override void OnUpdate()
    {
        //if (Input.GetMouseButton(0))
        //    return;

        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        //if (Physics.Raycast(ray,out var hit,Mathf.Infinity,mask))
        //{            
        //    SetCursor(CursorType.Attack);
        //}        
        //else
        //{
        //    SetCursor(CursorType.Normal);
        //}
    }    
}
