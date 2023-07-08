using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UI_Profil : MonoBehaviour ,IObserver
{
    [SerializeField]
    TextMeshProUGUI NameText;

    [SerializeField]
    TextMeshProUGUI AtkText;

    [SerializeField]
    TextMeshProUGUI DefText;

    [SerializeField]
    TextMeshProUGUI HpText;
    
    Actor m_actor;
    public void Connect(Actor _actor)
    {
        this.m_actor = _actor;
        _actor.Attach(this);
        Init();
        this.gameObject.SetActive(true);
    }

    void Init()
    {
        NameText.text = $"���ָ� : {m_actor.ActorInfo.name}";
        AtkText.text = $"���ݷ� : {m_actor.ActorInfo.atk}";
        DefText.text = $"���� : {m_actor.ActorInfo.def}";
        HpText.text = $"ü�� : {m_actor.Hp.ToString()} / {m_actor.MaxHp}";        
    }

    public void Notify(ISubject _subject)
    {
        if (m_actor.IsSelected == false)
            this.gameObject.SetActive(false);
        HpText.text = $"HP : {(int)m_actor.Hp} / {m_actor.MaxHp}";
    }
}
