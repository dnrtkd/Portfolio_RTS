using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormationCtrl : MonoBehaviour
{
    //private FormationPattern m_pattern;
    //private List<SlotAssignment> m_slotAssignments;
    //private Vector3 m_driftOffset;

    //public void Init()
    //{
    //    m_slotAssignments = new List<SlotAssignment>();
    //}
    //private void UpdateSlotAssignments()
    //{
    //    for (int i = 0; i < m_slotAssignments.Count; i++)
    //    {
    //        m_slotAssignments[i].slotIndex = i;
    //    }
    //    m_driftOffset = m_pattern.GetDriftOffset(m_slotAssignments);
    //}
    //public bool AddCharacter(Unit _character)
    //{
    //    int occupiedSlots = m_slotAssignments.Count;
    //    if (!m_pattern.SupportsSlots(occupiedSlots + 1))
    //        return false;
    //    SlotAssignment sa = new SlotAssignment();
    //    sa.character = _character;
    //    m_slotAssignments.Add(sa);
    //    UpdateSlotAssignments();
    //    return true;
    //}
    //public void RemoveCharacter(Unit _character)
    //{
    //    int index = m_slotAssignments.FindIndex(x => x.character.Equals(_character));
    //    m_slotAssignments.RemoveAt(index);
    //    UpdateSlotAssignments();
    //}
    //public void UpdateSlots()
    //{
    //    Unit leader = m_pattern.leader;
    //    Vector3 anchor = leader.transform.position;
    //    Vector3 slotPos = Vector3.zero;
    //    foreach (SlotAssignment sa in m_slotAssignments)
    //    {
    //        Vector3 charDirft = Vector3.zero;
    //        slotPos = m_pattern.GetSlotLocation(sa.slotIndex);
    //        charDirft = anchor;
    //        charDirft += leader.transform.TransformDirection(slotPos);
    //        charDirft += m_driftOffset;
    //        Unit unit = sa.character;
    //        unit.MoveTo(charDirft);
    //    }
    //}

}
