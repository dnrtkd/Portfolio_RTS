using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Command : ICommand
{
    protected bool isFinish;
    public virtual void Cancel()
    {
        isFinish = true;
    }
    public virtual bool isFinished()
    {
        return isFinish;
    }
    public abstract void Execute();        
    public abstract void Update();    
}
