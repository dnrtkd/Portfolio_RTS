using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//밀집 대형
public class CloseFormation : FormationPattern
{
    public override Vector3 GetDriftOffset(List<SlotAssignment> slotAssignments)
    {
        return base.GetDriftOffset(slotAssignments);
    }
    public override Vector3 GetSlotLocation(int slotIndex)
    {
        
        return base.GetSlotLocation(slotIndex);
    }
}
