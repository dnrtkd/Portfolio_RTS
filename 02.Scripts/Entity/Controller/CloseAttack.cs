using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseAttack : UnitAttack
{    
    protected override void Attack(Actor target)
    {
        int ind=Random.Range(0, audioClipKeys.Count - 1);
        SoundManager.Instance.PlayEffect(audioClipKeys[ind], transform.position);

        target.Damaged(attValue);
    }   
}
