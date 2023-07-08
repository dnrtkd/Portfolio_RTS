using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class RTS_EventSystem :MonoBehaviour
{
    [SerializeField]
    UI_Text gameStateBar;
    bool isRuuning;
    public enum RtsEventType
    {
        BE_ATTACKED,
        LACK_RESOURCES,
        FINISH,
        PAUSE
    }

    public List<RTS_Event> rtsEvents = new();
    
    public void Publish(RtsEventType type)
    {
        var _even = rtsEvents.Find(x => x.type == type);        

        if (_even == null)
            return;

        if (isRuuning == true)
            return;

        gameStateBar.Set(_even.context);
        isRuuning = true;
        StopAllCoroutines();
        StartCoroutine(CoPublish());

        if(_even !=null)
            _even.action.Invoke();
    }

    IEnumerator CoPublish()
    {
        yield return new WaitForSeconds(2f);
        gameStateBar.Set("");
        isRuuning = false;
        yield break;
    }
}


[System.Serializable]
public class RTS_Event
{
    public RTS_EventSystem.RtsEventType type;
    public string context;
    public UnityEvent action;        
}




