using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class MouseClick : MonoBehaviour
{
    public static MouseClick Instance;
    public enum ClickState
    {
        Normal,
        Build
    }

    [SerializeField]
    private LayerMask layerUnit;

    [SerializeField]
    private LayerMask layerGround;

    [SerializeField]
    private LayerMask layerBuilding;

    private Camera mainCam;

    private GroupCtrl groupCtrl;    

    private bool isShift
    {
        get
        {
            return Input.GetKey(KeyCode.LeftShift);
        }
    }
    public bool isAttack = false;
    private void Awake()
    {
        mainCam = Camera.main;
        groupCtrl = GetComponent<GroupCtrl>();
        Instance = this;
    }

    private void Start()
    {
        SetState(ClickState.Normal);
    }

    RaycastHit hitInfo;
    ActorCtrl beChosen;
    
    void OnCurssor(Enums.MouseEvent mouseEvent)
    {
        if (mouseEvent != Enums.MouseEvent.OnCurssor)
            return;
        if (EventSystem.current.IsPointerOverGameObject() == true)
            return;

        Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity, layerUnit | layerBuilding | layerGround))
        {
            if (1 << hitInfo.transform.gameObject.layer == layerUnit ||
                1 << hitInfo.transform.gameObject.layer == layerBuilding.value)
            {
                ActorCtrl newChosen = hitInfo.transform.GetComponent<ActorCtrl>();
                if (newChosen == null)
                    return;

                if (beChosen == null)
                {
                    beChosen = newChosen;
                    beChosen.MouseOn();
                    return;
                }

                if (newChosen.Owner.ID != beChosen.Owner.ID)
                {
                    beChosen.MouseExit();
                    beChosen = newChosen;
                    beChosen.MouseOn();
                }
            }
            else
            {
                if (beChosen != null)
                {
                    beChosen.MouseExit();
                    beChosen = null;
                }
            }
        }
    }
    void LeftClick(Enums.MouseEvent mouseEvent)
    {
        if (mouseEvent != Enums.MouseEvent.LeftClick)
            return;                

        Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);

        if (EventSystem.current.IsPointerOverGameObject() == true)
        {            
            return;
        }

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layerUnit | layerBuilding | layerGround))
        {            
            //gameObject.layer : layer의 순서를 그대로 반환
            //LayerMask는 비트연산된 값을 반환함
            if (1<<hit.transform.gameObject.layer == layerUnit || 1<<hit.transform.gameObject.layer == layerBuilding.value)
            {                
                if (isShift)
                {
                    groupCtrl.ShiftClickSelectUnit(
                        hit.transform.GetComponent<ActorCtrl>());
                }
                else
                {
                    groupCtrl.ClickSelectUnit(
                        hit.transform.GetComponent<ActorCtrl>());
                }                
            }
            else
            {                               
                groupCtrl.LeftClickEvent(GameScene.I.MapMngP.grid.GetCellCenterWorld(hit.point));

                //if (!isShift)
                //    groupCtrl.DeselectAll();

            }
        }        
    }    
    void RightClick(Enums.MouseEvent mouseEvent)
    {
        if (mouseEvent != Enums.MouseEvent.RightClick)
            return;

        Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
        isAttack = false;
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layerUnit | layerBuilding | layerGround))
        {
            if (1 << hit.transform.gameObject.layer == layerUnit || 1 << hit.transform.gameObject.layer == layerBuilding.value)
            {
                ActorCtrl actorCtrl = hit.transform.GetComponent<ActorCtrl>();
                actorCtrl.MouseClicked();
                groupCtrl.SetTargetEvent(actorCtrl.Owner);
            }
            else
            {
                //if (isShift == false)
                //    groupCtrl.CommandCancel();

                groupCtrl.RightClickEvent(GameScene.I.MapMngP.grid.GetCellCenterWorld(hit.point));
            }                
        }       
    }
    void BuildingLeftClick(Enums.MouseEvent mouseEvent)
    {
        if (mouseEvent != Enums.MouseEvent.LeftClick)
            return;

        Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);

        if (EventSystem.current.IsPointerOverGameObject() == true)
        {
            return;
        }

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity,  layerGround))
            groupCtrl.LeftClickEvent(GameScene.I.MapMngP.grid.GetCellCenterWorld(hit.point));
    }
    public void SetState(ClickState state)
    {
        if(state==ClickState.Normal)
        {
            GameScene.I.MouseMng.OnMouseEvent -= BuildingLeftClick;
            GameScene.I.MouseMng.OnMouseEvent -= RightClick;
            GameScene.I.MouseMng.OnMouseEvent += LeftClick;
            GameScene.I.MouseMng.OnMouseEvent += RightClick;
            GameScene.I.MouseMng.OnMouseEvent += OnCurssor;
        }
        else if(state==ClickState.Build)
        {
            GameScene.I.MouseMng.OnMouseEvent -= LeftClick;
            GameScene.I.MouseMng.OnMouseEvent -= OnCurssor;
            GameScene.I.MouseMng.OnMouseEvent += BuildingLeftClick;
        }
    }
}