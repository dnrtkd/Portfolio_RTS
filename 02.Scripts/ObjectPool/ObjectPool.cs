using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T:MonoBehaviour,IPoolObj
{
    private List<PoolItem<T>> activeList = new();
    private List<PoolItem<T>> hideList = new();

    public List<PoolItem<T>> GetList { get { return activeList; } }

    private int maxCount = 100;
    
    public ObjectPool(int _maxCount=100)
    {
        maxCount = _maxCount;
    }

    //hideListø° «ÿ¥Á ∞¥√º∏¶ √Ê¿¸ Ω√≈¥
    public void Add(string path,T item)
    {
        if (item == null)
            return;

        if(maxCount<=hideList.Count)
        {
            Debug.Log("∞¥√º «Æ¿Ã ≤À √°Ω¿¥œ¥Ÿ.");
            return;
        }

        int key = path.GetHashCode();
        var poolItem = new PoolItem<T>(key, item);
        hideList.Add(poolItem);
    }

    public PoolItem<T> GetItem(string _path)
    {
        int key = _path.GetHashCode();
        var findItem = hideList.Find
            (item => item == null ? false : item.key == key);

        if(findItem == null)
        {        
            Debug.Log("ObjectPool:: GetItem() " +
                "[null==Component]" + typeof(T));        
        }
        else
        {            
            hideList.Remove(findItem);
            activeList.Add(findItem);
        }
        return findItem;
    }   

    public void OnUpdate()
    {
        for (int i = 0; i < activeList.Count; i++)
        {
            if(activeList[i].item == null)
            {
                activeList.RemoveAt(i);
                continue;
            }
            if(activeList[i].item.isReturn()== true)
            {
                var active = activeList[i];
                active.item.gameObject.SetActive(false);
                activeList.RemoveAt(i);
                if(maxCount<=hideList.Count)
                {
                    GameObject.Destroy(activeList[i].item.gameObject);
                }
                else
                {
                    hideList.Add(active);
                }
                continue;
            }
            activeList[i].item.OnUpdate();
        }                  
    }
}



public class PoolItem<T> where T:MonoBehaviour,IPoolObj
{
    public int key;
    public T item;

    public PoolItem(int _key,T _item)
    {
        key = _key;
        item = _item;
    }
}
