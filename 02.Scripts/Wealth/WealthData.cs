using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//자원 데이터
[System.Serializable]
public class WealthData 
{
    public enum WEALTH_TYPE
    {
        Wood,
        Food,
        Pop
    }
    [SerializeField]
    private WEALTH_TYPE m_type;
    [SerializeField]
    private int m_count;   

    public int count { get { return m_count; } }
    public WEALTH_TYPE type { get { return m_type; } }

    public WealthData() { m_count = 0; }
    public WealthData(WEALTH_TYPE _type) { m_type = _type; }
    public void SetCount(int _count)
    {
        m_count = _count;
        if (m_count < 0)
            m_count = 0;
    }
    public void AddCount(int _count)
    {
        SetCount(m_count + _count);
    }
    public void SubCount(int _count)
    {
        SetCount(m_count - _count);
    }
}
