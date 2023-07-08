using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotAssignment
{
    public int slotIndex;
    public Unit character;

    public SlotAssignment()
    {
        slotIndex = -1;
        character = null;
    }
}
public class FormationPattern 
{
    private Vector3 m_center;
    protected Vector3 m_startPosition;
    protected Unit m_leader;
    protected float m_spacing;
    protected int m_unitCount;
    public Vector3 Center { get { return m_center; } }
    public Unit Leader { get { return m_leader; } }
    public float Spacing { get { return m_spacing; } }

    public void Init()
    {
        //if (leader == null)
            //leader = transform.gameObject;
    }
    public void SetCenter(Vector3 _pos)
    {
        m_center = _pos;
    }
    public void SetLeader(Unit _unit)
    {
        m_leader = _unit;
    }
    public virtual Vector3 GetSlotLocation(int slotIndex)
    {
        return Vector3.zero;
    }
    public virtual Vector3 GetDriftOffset(List<SlotAssignment> slotAssignments)
    {
        return Vector3.zero;
    }
    public virtual Vector3 GetStartPosition(int _unitCount)
    {
        return Vector3.zero;
    }
    public virtual int[,] CalulatePrefer(Vector3[] _unitPos,Vector3[] _desPos,int _unitcount)
    {
        return null;
    }
}
