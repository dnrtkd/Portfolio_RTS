using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IObserver 
{
    void Notify(ISubject _subject);
}
