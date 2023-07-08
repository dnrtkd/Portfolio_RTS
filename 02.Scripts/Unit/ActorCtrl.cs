using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public abstract class ActorCtrl : MonoBehaviour
{
    protected Actor m_owner;
    public Actor Owner { get { return m_owner; } }
    
    private float actorScale;
    private UI_HpBar hpBar;
    GroupCtrl groupCtrl;
    public List<ActorSkill> actorSKills = new();

    public virtual void Init()
    {
        actorScale = GetComponent<Actor>().actorScale;
        hpBar = GetComponent<UI_HpBar>();
        groupCtrl = FindObjectOfType<GroupCtrl>();
        
    }   
    public virtual void SelectUnit()
    {
        GameScene.I.LineMng.OnLineDraw += DrawCircle;
        //Owner.GetComponent<LineRenderHelper>().DrawCircle(actorScale, Color.green);
        if (hpBar!=null)
            hpBar.ON();
        m_owner.IsSelected = true;        
    }
    public void Remove()
    {
        if (groupCtrl == null)
            return;
        groupCtrl.DiselectUnit(this);
    }
    public virtual void DeselectUnit()
    {
        Owner.GetComponent<LineRenderHelper>().Off();
        GameScene.I.LineMng.OnLineDraw -= DrawCircle;        
        if (hpBar != null)
            hpBar.Off();
        m_owner.IsSelected = false;
    }
    public virtual void RightClickBehaviour(Vector3 pos)
    {
        
    }
    public virtual void LeftClickBehaviour(Vector3 pos)
    {

    }
    public void MouseExit()
    {
        var renderes = Owner.renderes;
        foreach (var item in renderes)
            item.material.color = Color.white;
        GameScene.I.LineMng.OnLineDraw -= DrawCircle2;
        scale = actorScale;
        StopAllCoroutines();
    }

    public void MouseOn()
    {
        var renderes = Owner.renderes;
        foreach (var item in renderes)
            item.material.color = Color.yellow;
        GameScene.I.LineMng.OnLineDraw += DrawCircle2;        
        StartCoroutine(CoDrawCircle());
    }

    public void MouseClicked()
    {
        StartCoroutine(CoFlicker());
    }

    IEnumerator CoFlicker()
    {
        var renderes = Owner.renderes;

        foreach (var item in renderes)
            item.material.color = Color.yellow;

        yield return new WaitForSeconds(0.05f);

        foreach (var item in renderes)
            item.material.color = Color.white;

        yield return new WaitForSeconds(0.05f);

        foreach (var item in renderes)
            item.material.color = Color.yellow;

        yield return new WaitForSeconds(0.05f);

        foreach (var item in renderes)
            item.material.color = Color.white;
    }
        
    float scale = 0.0f;
    IEnumerator CoDrawCircle()
    {
        float t = 0.0f;        
        scale = actorScale;
        while (true)
        {
            t += 0.05f;
            scale = (actorScale * Mathf.Sin(t))*0.2f + actorScale;
            yield return null;
        }        
    }
    void DrawCircle()
    {
        if (Camera.current.CompareTag("MainCamera") == false)
            return;

        GameScene.I.LineMng.DrawCircle(transform.position+Vector3.up*0.2f, actorScale , Color.green);
    }
    void DrawCircle2()
    {
        if (Camera.current.CompareTag("MainCamera") == false)
            return;

        GameScene.I.LineMng.DrawCircle(transform.position+ Vector3.up * 0.2f, scale, Color.white);
    }

    public virtual void CommandCancle()
    {
        m_owner.CommandCancle();
    }

    public virtual void Targetting(Actor actor)
    {

    }

}
