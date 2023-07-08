using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 유닛의 대형을 갖추게 하는 클래스
/// </summary>
public class FormationList 
{
    private FormationPattern m_pattern;
    private List<Unit> m_slotAssignments;
    private Vector3[] m_desPositions;
    private Vector3 m_center;
    private Vector3 m_driftOffset;
    private int m_maxUnitCount;
    public int UnitCount { get { return m_slotAssignments.Count; } }
    public FormationList(FormationPattern _formation)
    {
        m_pattern = _formation;
        m_maxUnitCount = 50;
        m_slotAssignments = new List<Unit>();
        m_desPositions = new Vector3[m_maxUnitCount];
        for(int i=0; i<m_maxUnitCount;++i)
        {
            m_desPositions[i] = new Vector3(0, 0, 0);
        }
    }
    public void Init()
    {
        
    }
    public void Clear()
    {
        m_slotAssignments.Clear();
    }
    public bool AddCharacter(Unit _character)
    {
        m_slotAssignments.Add(_character);
        UpdateSlotAssignments();

        return true;
    }
    public void RemoveCharacter(Unit _character)
    {
        m_slotAssignments.Remove(_character);
        UpdateSlotAssignments();
    }
    private void UpdateSlotAssignments()
    {
        m_pattern.GetStartPosition(UnitCount);
        if (m_slotAssignments != null && m_slotAssignments.Count>0)
        {
            m_center = Vector3.zero;
            foreach(var unit in m_slotAssignments)
            {
                m_center += unit.transform.position;
            }
            m_center /= UnitCount;
        }
    }
    public void MoveFormation(Vector3 _des)
    {
        UpdateSlotAssignments();
        Vector3 moveVector = _des - m_center;
        if (moveVector.sqrMagnitude <= 0)
            return;

        Vector3 forward = moveVector.normalized;
        Quaternion rot = Quaternion.LookRotation(forward);

        for (int i = 0; i < UnitCount; i++)
        {
            //상대 위치
            Vector3 relaPos = m_pattern.GetSlotLocation(i);
            m_desPositions[i] =rot*relaPos + _des;
        }

        Vector3[] unitPos = new Vector3[UnitCount];
        for(int i=0;i<UnitCount;++i)
        {
            unitPos[i] = m_slotAssignments[i].transform.position;
        }

        var wPartner= StableMarriage_Algoritm.Calculate
            (m_pattern.CalulatePrefer(unitPos, m_desPositions, UnitCount));

        for(int i=0; i<UnitCount;i++)
        {
            int unitId = wPartner[i];
            int destId = i;

            m_slotAssignments[unitId].Move(m_desPositions[destId]);
        }
        
    }
}
