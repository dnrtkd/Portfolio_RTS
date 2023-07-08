using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class BaseScene :MonoBehaviour
{
    protected Base_Manager[] Managers;
    public virtual void Init()
    {
        Object obj = GameObject.FindObjectOfType(typeof(EventSystem));
        if (obj == null)
            ResUtil.Create("Prefab/UI/EventSystem").name="@EventSystem";

        Managers = GetComponents<Base_Manager>();

        if(Managers!=null)
        {
            foreach (var item in Managers)
                item.Init();
        }       
    }
    //해당 씬의 매니저타입 인스턴스만 가져옴
    public virtual void OnUpdate()
    {
        if (Managers != null)
        {
            foreach (var item in Managers)
                item.OnUpdate();
        }
    }
    public virtual void Clear()
    {
        if (Managers != null)
        {
            foreach (var item in Managers)
                item.Clear();
        }
    }
}
