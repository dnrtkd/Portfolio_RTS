using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UI_SkillBar :MonoBehaviour, IObserver
{
    KeyboardManager keyboard;

    [SerializeField]
    List<UI_Button> buttons;

    private void Awake()
    {
        Delete();
    }
    public void Notify(ISubject _subject)
    {
        Refresh();        
    }

    void Refresh()
    {
        Delete();

        var actorSkills = keyboard.actorSkills;
        for(int i=0;i<actorSkills.Count;i++)
        {
            string iconName = actorSkills[i].iconName;            
            buttons[i].Set(iconName, actorSkills[i].action);            
        }
    }

    void Delete()
    {
        foreach (var button in buttons)
            button.SetOff();
    }
    public void Connect(KeyboardManager keboard)
    {
        this.keyboard = keboard;
        keyboard.Attach(this);        
    }
    
}
