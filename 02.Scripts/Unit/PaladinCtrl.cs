using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaladinCtrl : UnitCtrl
{
    public Hero_Paladin Owner_Paladin;
    public override void Init()
    {
        base.Init();
        Owner_Paladin = GetComponent<Hero_Paladin>();
        actorSKills.Add(new UnitSkill(KeyCode.D, GetComponent<Hero_Paladin>(), "DashAttack", "DashAttack",
            () =>
            {
                LeftClickAction.RemoveAllListeners();
                LeftClickAction.AddListener(Owner_Paladin.skill_1.SetDestination);
                CursorManager.instance.SetCursor(CursorManager.CursorType.Hand);
            }));
    }
}
