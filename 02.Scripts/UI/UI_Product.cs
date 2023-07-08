using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// ���� ���� UI
/// </summary>
public class UI_Product : MonoBehaviour,IObserver
{
    Building_Produce produce;

    //�����̴�
    [SerializeField]
    UI_CurrentUnitBar currentUnitBar;

    [SerializeField]
    List<UI_Image> images;

    UI_Image[] imageSlots = new UI_Image[5];

    void Awake()
    {
        for (int i = 0; i < 5; ++i)
            imageSlots[i] = images[i];
    }
    void Refresh()
    {        
        //Delete();        
        int count = 0;
        //���� ����        
        foreach(var item in produce.WaitList)
        {
            imageSlots[count].Set(ResUtil.EnumToString(item));
            count++;
        }       
        
        //�� ����
        for(;count<5;++count)
        {
            imageSlots[count].Set("Empty");            
        }        
    }
    public void Delete()
    {
        foreach (var image in imageSlots)
            Object.Destroy(image);
        //imageSlots.Clear();      
    }    

    public void Connect(Building_Produce pro)
    {        
        produce = pro;
        pro.Attach(this);
        if (produce.IsProduct)
        {
            gameObject.SetActive(true);
            Refresh();
        }
        else
            gameObject.SetActive(false);
        
        currentUnitBar.Connet(produce.UnitCre);        
    }    
    public void Notify(ISubject _subject)
    {        
         if(produce.WaitList.Count==0)
        {
            gameObject.SetActive(false);
            return;
        }

         gameObject.SetActive(true);
         Refresh();             
    }
}

