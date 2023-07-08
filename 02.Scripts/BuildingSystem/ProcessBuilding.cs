using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//건물에 같이 들어가는 스크립트
public class ProcessBuilding : MonoBehaviour
{
    private Building m_building;
    private float m_processTime; //생산시간
    private float m_timer; //타이머
    public bool Complete { get; private set; }
    public Building OwnerBuilding { get { return m_building; } }
    public void Init()
    {
        m_building = GetComponent<Building>();
        m_building.changeBuildingShpae(1);
        m_processTime = m_building.ProcessTime;
        m_timer = 0;
    }
    public void BuildingStructure(float _time)
    {
        if (m_processTime <= m_timer)
        {
            Complete = true;
            m_building.changeBuildingShpae(3);

            return;
        }
        m_timer += _time;
        m_building.AddHp(m_building.MaxHp * (_time / m_processTime));
        if (m_timer / m_processTime > 0.5)
            m_building.changeBuildingShpae(2);
    }    
}
