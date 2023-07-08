using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WealthDataCollection : SubjectMono
{
    private List<WealthData> wealthDatas = new();
    public void Init()
    {

    }
    private WealthData GetWealth(WealthData.WEALTH_TYPE _type)
    {
        return wealthDatas.Find(item => item.type == _type);
    }
    public int GetCount(WealthData.WEALTH_TYPE _type)
    {
        WealthData _find = GetWealth(_type);
        if (null == _find)
            return 0;

        return _find.count;
    }
    public void SetCount(WealthData.WEALTH_TYPE _type, int _count)
    {
        WealthData _find = GetWealth(_type);
        if (null == _find)
            wealthDatas.Add(_find = new WealthData(_type));

        _find.SetCount(_count);
        Notify();
    }
    public void AddCount(WealthData.WEALTH_TYPE _type, int _Count)
    {
        SetCount(_type, GetCount(_type) + _Count);
    }
    public void SubCount(WealthData.WEALTH_TYPE _type, int _Count)
    {
        SetCount(_type, GetCount(_type) - _Count);
    }
}

