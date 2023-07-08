using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�ǹ��� ���� ���� ��ũ��Ʈ
public class ProcessBuilding : MonoBehaviour
{
    private Building m_building;
    private float m_processTime; //����ð�
    private float m_timer; //Ÿ�̸�
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
