using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//rts���� ��ü�� ������Ʈ�� ������ ���� ��ü�� groupCtrl������Ʈ������ ����
public class HpBarManager : Base_Manager
{
    [SerializeField]
    UI_HpBar hpBar;

    [SerializeField]
    GameObject HpbarPannel;

    List<UI_HpBar> UI_HpBars;
    public override void Init()
    {
        UI_HpBars = new List<UI_HpBar>();
    }
    public override void OnUpdate()
    {
        //if (UI_HpBars.Count == 0) return;

        //foreach (var item in UI_HpBars)
        //{
        //    item.OnUpdate();
        //}
    }
    public override void Clear()
    {
        
    }
    public void OnHpBar( Actor _actor)
    {
        //Transform unitTransform = _actor.transform;
        //UI_HpBar ui_hp= Instantiate(hpBar.gameObject, unitTransform.position, Quaternion.identity, HpbarPannel.transform).GetComponent<UI_HpBar>();
        //ui_hp.Connect(_actor);
        //UI_HpBars.Add(ui_hp);
    }
    public void RemoveHpBar(UI_HpBar _HpBar)
    {
        UI_HpBars.Remove(_HpBar);
    }
}
