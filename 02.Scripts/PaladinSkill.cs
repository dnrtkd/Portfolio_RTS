using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//������ ���� �� , ���� �� ���� �ε����� �� ��� �� ���� ������
public class PaladinSkill : MonoBehaviour
{
    Hero_Paladin hero;

    float Timer;
    //��Ÿ��
    [SerializeField]
    float coolDownTime;

    //���� �̵� �Ÿ�
    [SerializeField]
    float moveDistance;

    //���ӵ� ��
    [SerializeField]
    float accelation;

    GameObject lightningEffect;
    GameObject lightningPrefab;

    GameObject HitEffect;
    GameObject HitPrefab;
    public bool StanBy {
        get 
        {
            if (Timer >= coolDownTime)
                return true;
            else
                return false;
        } 
    }

    float Speed;
    Vector3 destination;
    Vector3 dir;

    public void Init()
    {
        hero = GetComponent<Hero_Paladin>();
        lightningEffect = ResUtil.Load<GameObject>("Lightning");
        HitEffect = ResUtil.Load<GameObject>("HitEffect");
    }
    
    public void SetDestination(Vector3 pos)
    {
        destination = pos;
        dir = (destination - hero.Position).normalized;
        hero.transform.LookAt(pos);
        //������Ʈ�� �ڵ� ������ ��� ��        
        hero.fsm.SetState(Actor.HighState.Skill_1);
        hero.gameObject.layer = LayerMask.NameToLayer("UseSkill");
        lightningPrefab = Instantiate(lightningEffect,hero.transform);
    }

    public bool Crash()
    {        
        if (Physics.CheckBox(hero.Position, Vector3.one,Quaternion.identity,
            (1 << LayerMask.NameToLayer("Unit") | 1 << LayerMask.NameToLayer("building"))))
        {
            HitPrefab = Instantiate(HitEffect, hero.transform);
            var PaladinCrash = HitPrefab.AddComponent<PaladinCrash>();
;           PaladinCrash.Init(hero, 5f, 7);
            return true;                   
        }
        else
            return false;
    }

    public void ShockWave()
    {
        
        var colls = Physics.OverlapBox(hero.Position, Vector3.one * 2);
        for (int i = 0; i < colls.Length; ++i)
            if (colls[i].TryGetComponent(out Unit unit))
                if (unit.m_team == Enums.TEAM.ENMEY)
                {
                    unit.SetCC(CC_Type.Airborne);
                    unit.Damaged(30.0f);
                }
    }

    public void Dash()
    {        
        
        if(Speed < 10.0f)
        {
            Speed += accelation*Time.deltaTime;
            hero.m_Animator.speed = Speed/5.0f;
        }
        hero.Position += dir * Speed*Time.deltaTime;        
    }

    public void Stop()
    {
        Speed = 0.0f;
        hero.m_Animator.speed = 1.0f;
        hero.gameObject.layer = LayerMask.NameToLayer("Unit");
        Destroy(lightningPrefab);
    }
    
    public void Hit()
    {
        
    }

    public void OnUpdate()
    {
        if(Timer<coolDownTime)
            Timer += Time.deltaTime;
    }

}
