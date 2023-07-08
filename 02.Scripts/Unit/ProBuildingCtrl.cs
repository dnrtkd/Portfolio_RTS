using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class ProBuildingCtrl : BuildingCtrl
{        
    public ProBuilding building { get { return GetComponent<ProBuilding>(); } }
    private UnityEvent<Vector3> LeftClickAction = new();
    public override void RightClickBehaviour(Vector3 pos)
    {
        base.RightClickBehaviour(pos);
        building.Production.SetRallyPoint(pos);        
    }

    public override void Init()
    {
        base.Init();
        actorSKills.Add(new BuildingSkill(KeyCode.R,this.building, "RallyPoint", "RallyPoint", () =>
          {
              CursorManager.instance.SetCursor(CursorManager.CursorType.Rally);
              LeftClickAction.RemoveAllListeners();
              LeftClickAction.AddListener(building.Production.SetRallyPoint);
          }));

        

        for (int i = 0; i < building.Production.UnitCatalog.Count; ++i)
        {

            string _name = ResUtil.EnumToString(building.Production.UnitCatalog[i]);
            
            actorSKills.Add(new ProBuildingSkill((KeyCode)_name[0], this.building,_name ,_name, building.Production.UnitCatalog[i]
            ));
        }

    }
    public override void LeftClickBehaviour(Vector3 pos)
    {
        if (Input.GetKey(KeyCode.LeftShift) == false)
            //CommandCancle();

            base.LeftClickBehaviour(pos);

        LeftClickAction.Invoke(pos);
        LeftClickAction.RemoveAllListeners();

        CursorManager.instance.SetCursor(CursorManager.CursorType.Normal);
    }
    public override void SelectUnit()
    {
        base.SelectUnit();
        Owner.GetComponent<Building_Produce>().TheFlag.SetActive(true);
        //Owner.GetComponent<LineRenderHelper>().renderDraw += building.Production.DrawToRallyPoint;
        //GameScene.I.LineMng.OnLineDraw -= building.Production. DrawToRallyPoint;
        //GameScene.I.LineMng.OnLineDraw += building.Production. DrawToRallyPoint;          
    }
    public override void DeselectUnit()
    {
        base.DeselectUnit();
        Owner.GetComponent<Building_Produce>().TheFlag.SetActive(false);
        //Owner.GetComponent<LineRenderHelper>().Off();
        //GameScene.I.LineMng.OnLineDraw -= building.Production.DrawToRallyPoint;             
    }    
}
