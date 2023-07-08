using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceCtrl : ActorCtrl
{
    public override void Init()
    {
        base.Init();
        m_owner = GetComponent<Resource>();
    }
}
