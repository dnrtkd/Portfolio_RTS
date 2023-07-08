using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero_Paladin : Hero
{
    //스킬을 관리 해주는 컴포넌트
    public PaladinSkill skill_1;

    public override void Init()
    {
        fsm.AddState(new PaladinState_ActiveSkill(this));
        fsm.AddState(new PaladinState_Dash(this));
        base.Init();
        skill_1 = GetComponent<PaladinSkill>();
        skill_1.Init();        
        
    }

    public override void OnUpdate()
    {        
        base.OnUpdate();
        skill_1.OnUpdate();
    }

    public override ActorCtrl AddCtrlComponent()
    {
        return AddCtrlComponent<PaladinCtrl>();
    }

}
