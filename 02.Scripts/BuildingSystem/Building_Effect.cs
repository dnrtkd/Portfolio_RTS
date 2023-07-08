using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building_Effect : MonoBehaviour
{
    public enum EffectType
    {
        Destory,
        Burnning
    }

    [SerializeField]
    GameObject DestroyEffect;

    [SerializeField]
    GameObject BurnningEffect;

    Dictionary<EffectType, GameObject> Effects=new();

    public void Start()
    {
        Effects.Add(EffectType.Destory, DestroyEffect);
        Effects.Add(EffectType.Burnning, BurnningEffect);
    }
    public void EffectOn(EffectType type)
    {        
        Effects[type].SetActive(true);
    }

    public void EffectOff(EffectType type)
    {
        Effects[type].SetActive(false);
    }
}
