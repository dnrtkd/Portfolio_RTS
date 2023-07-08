using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Subject : ISubject
{
    protected List<IObserver> m_observerList = new List<IObserver>();
    public virtual void Clear()
    {
        m_observerList.Clear();
    }

    public virtual void Attach(IObserver _observer)
    {
        if (null == _observer)
            return;

        if (m_observerList.Contains(_observer))
            return;
        m_observerList.Add(_observer);
    }

    public virtual void Detach(IObserver _observer)
    {
        if (null == _observer)
            return;
        m_observerList.Remove(_observer);
    }
    public virtual void Notify()
    {
        for (int i = 0; i < m_observerList.Count; i++)
        {
            IObserver _observer = m_observerList[i];
            if (null == _observer)
                continue;
            _observer.Notify(this);
        }
    }
}
