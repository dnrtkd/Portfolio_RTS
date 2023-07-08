using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Group : MonoBehaviour,IObserver
{
    //연결
    GroupCtrl groupCtrl;    
    Dictionary<Enums.UnitType, UI_GroupImage> elements=new();

    //이미지 프리팹
    [SerializeField]
    GameObject GroupImagePrefab;
    [SerializeField]
    RectTransform uI_ProfilPanel;
    [SerializeField]
    RectTransform uI_GropuPanel;
    [SerializeField]
    UI_Product uI_Product;
    public void Connect(GroupCtrl ctrl)
    {
        groupCtrl = ctrl;
        groupCtrl.Attach(this);
        Refresh();
    }

    void Set()
    {
        int count = groupCtrl.SelectedActors.Count;

        if (count == 0)
        {
            gameObject.SetActive(false);
            uI_ProfilPanel.gameObject.SetActive(false);
            uI_Product.gameObject.SetActive(false);
        }
        else if (count == 1)
        {
            gameObject.SetActive(false);
            uI_ProfilPanel.gameObject.SetActive(true);                                            
        }
        else if (count > 1)
        {
            gameObject.SetActive(true);
            uI_ProfilPanel.gameObject.SetActive(false);
            uI_Product.gameObject.SetActive(false);
        }
    }    

    void Delete()
    {
        if (elements.Count == 0)
            return;

        foreach (var item in elements)
            Object.Destroy(item.Value.gameObject);
        elements.Clear();
    }
    void Refresh()
    {
        Set();
        Delete();
        foreach(var item in groupCtrl.SelectedActors)
        {
            if (item.Owner.ActorInfo.actor != Enums.ActorType.CHARACTER)
                return;

            var type = (item.Owner as Unit).type;

            if (elements.ContainsKey((item.Owner as Unit).type))
                elements[type].PlusNum();
            else
            {
                var groupIamage = GameObject.Instantiate(GroupImagePrefab, uI_GropuPanel.transform).
                    GetComponent<UI_GroupImage>();
                elements.Add(type, groupIamage);
                elements[type].PlusNum();
                elements[type].Set(type);
            }                
        }
    }
    public void Notify(ISubject _subject)
    {
        Refresh();
    }
}
