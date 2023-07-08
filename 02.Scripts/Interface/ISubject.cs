using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISubject 
{
    void Clear();

    void Attach(IObserver _observer);



    void Detach(IObserver _observer);

    void Notify();
   
}
