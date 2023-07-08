using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//슬라이더 객체이 붙는 컴포넌트
//붙어있던 
public class UI_HpBar : MonoBehaviour
{
    public GameObject HpUI;    
    Slider Hp_bar;    
    Image fill;
    public void Init()
    {
        HpUI = ResUtil.Create("Prefab/UI/Ui_Hp",transform);
        Hp_bar = HpUI.GetComponentInChildren<Slider>();
        fill = HpUI.GetComponentInChildren<Image>();        
        if (GetComponent<Actor>().m_team == Enums.TEAM.ENMEY)
            fill.color = Color.red;

        HpUI.transform.position = transform.position + Vector3.up * (GetComponent<Collider>().bounds.size.y) + new Vector3(0,0.5f,0);
        Hp_bar.GetComponent<RectTransform>().sizeDelta = 
            new Vector2(Hp_bar.GetComponent<RectTransform>().sizeDelta.x*transform.localScale.x * GetComponent<BoxCollider>().size.x, 30);
        Canvas canvas = HpUI.GetComponent<Canvas>();
        canvas.worldCamera = Camera.main;
        SetSliderValue(GetComponent<Actor>().Hp/GetComponent<Actor>().ActorInfo.maxHp);
        Off();
    }
    public void Update()
    {
        HpUI.transform.rotation = Camera.main.transform.rotation;
    }

    public void ON()
    {
        HpUI.SetActive(true);
    }
    public void Off()
    {
        HpUI.SetActive(false);
    }
    
    public void SetSliderValue(float ratio)
    {
        Hp_bar.value = ratio;
    }

    public void SetColor(Color color)
    {
        fill.color = color;
    }
    
}
