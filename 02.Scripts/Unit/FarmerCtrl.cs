using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmerCtrl : UnitCtrl
{
    public enum FState
    {
        Normal,
        ChooseBuilding,
        isBuilding
    }
    FState currState;
    public Farmer OwnerFarmer { get { return m_owner as Farmer; } }

    ActorSkill build;
    ActorSkill build_Castle;
    ActorSkill build_Barrack;
    ActorSkill build_Archery;

    List<ActorSkill> buildSkiils=new();
    List<ActorSkill> isBildingSkills =new();

    public override void Init()
    {
        base.Init();
        m_owner = GetComponent<Farmer>();

        InitSkill();

        actorSKills.Add(build);

        buildSkiils.Add(build_Castle);
        buildSkiils.Add(build_Barrack);
        buildSkiils.Add(build_Archery);

        isBildingSkills.Add(stop);
        
        void InitSkill()
        {
            build = new FarmerSkill(KeyCode.B, this.OwnerFarmer, "Build", "Build",
                    () =>
                    {
                        SetState(FState.ChooseBuilding);
                    });
            build_Castle = new FarmerSkill(KeyCode.C, this.OwnerFarmer, "Castle", "Castle",
                    () =>
                    {
                        LeftClickAction.AddListener(GameScene.I.BuildMng.BuildingLeftClick);
                        MouseClick.Instance.SetState(MouseClick.ClickState.Build);
                        GameScene.I.BuildMng.InitializeWithObject(Enums.BuildingType.castle, this.OwnerFarmer, GameScene.I.WealthMng);
                    });
            build_Barrack = new FarmerSkill(KeyCode.B, this.OwnerFarmer, "Barrack", "Barrack",
                    () =>
                    {
                        LeftClickAction.AddListener(GameScene.I.BuildMng.BuildingLeftClick);
                        MouseClick.Instance.SetState(MouseClick.ClickState.Build);
                        GameScene.I.BuildMng.InitializeWithObject(Enums.BuildingType.Barrack, this.OwnerFarmer, GameScene.I.WealthMng);
                    });
            build_Archery = new FarmerSkill(KeyCode.A, this.OwnerFarmer, "Archery", "Archery",
                    () =>
                    {
                        LeftClickAction.AddListener(GameScene.I.BuildMng.BuildingLeftClick);
                        MouseClick.Instance.SetState(MouseClick.ClickState.Build);
                        GameScene.I.BuildMng.InitializeWithObject(Enums.BuildingType.Archery, this.OwnerFarmer, GameScene.I.WealthMng);
                    });
        }
    }
    public override void DeselectUnit()
    {
        base.DeselectUnit();
        OwnerFarmer.SetOffWaitBuildings();        
        GameScene.I.LineMng.OnLineDraw -= OwnerFarmer.DrawConnectLine;
    }
    public override void Targetting(Actor actor)
    {
        if (Input.GetKey(KeyCode.LeftShift) == false)
            CommandCancle();

        if (actor == null)
            return; 

        if(actor.ActorInfo.actor==Enums.ActorType.BUILDING)
        {
            var building = actor as Building;
            if (building.isComplete == false)
                OwnerFarmer.ConStruct(building.GetComponent<ProcessBuilding>());
        }        
        if(actor.ActorInfo.actor==Enums.ActorType.RESOURCE)
        {            
            var resource = actor as Resource;
            OwnerFarmer.Gathering(resource);
        }
    }
    public override void SelectUnit()
    {
        base.SelectUnit();
        OwnerFarmer.SetOnWaitBuildings();        
        GameScene.I.LineMng.OnLineDraw += OwnerFarmer.DrawConnectLine;        
    }
    public override void RightClickBehaviour(Vector3 pos)
    {
        base.RightClickBehaviour(pos);
    }
    public override void LeftClickBehaviour(Vector3 pos)
    {
        base.LeftClickBehaviour(pos);
    }    
    public void SetState(FState state)
    {
        switch (state)
        {
            case FState.Normal:
                currState = state;                
                KeyboardManager.Instance.RemoveAll();
                KeyboardManager.Instance.Add(actorSKills);
                break;
            case FState.ChooseBuilding:
                currState = state;
                KeyboardManager.Instance.RemoveAll();
                KeyboardManager.Instance.Add(buildSkiils);
                break;
            case FState.isBuilding:
                currState = state;                
                KeyboardManager.Instance.RemoveAll();
                KeyboardManager.Instance.Add(actorSKills);
                break;
            default:
                break;
        }
    }
}
