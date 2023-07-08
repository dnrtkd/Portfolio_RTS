using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Base_Manager : MonoBehaviour
{
    public abstract void Init();
    public abstract void OnUpdate();
    public abstract void Clear();
}
