using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPoolObj
{
    public abstract void Open(Vector3 pos, Enums.TEAM team);
    public abstract void Close();
    public abstract bool isReturn();
    public abstract void OnUpdate();
}
