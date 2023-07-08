using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

public class KeyboardManager : SubjectMono
{
    public static KeyboardManager Instance;
    
    private void Awake()
    {
        Instance = this;
    }

    [SerializeField]
    UI_SkillBar skillbar;

    public List<ActorSkill> actorSkills=new();

    private void Start()
    {
        skillbar.Connect(this);
    }

    public void Add(List<ActorSkill> skills)
    {               
        foreach(var skill in skills)
        {
            bool flag = false;

            foreach(var item in actorSkills)
                if(item.skillName == skill.skillName)
                {
                    item.action += skill.action;
                    flag = true;
                    break;
                }
            if(flag==false)
                actorSkills.Add(skill);
        }

        Notify();
    }
    public void Remove(List<ActorSkill> skills)
    {        
        foreach (var skill in skills)
        {            
            foreach (var item in actorSkills)
                if (item.skillName == skill.skillName)
                {
                    item.action -= skill.action;
                    if (item.action==null)
                        actorSkills.Remove(item);
                    break;
                }           
        }

        Notify();
    }
    public void RemoveAll()
    {
        actorSkills.Clear();
        Notify();
    }
    private void Update()
    {
        if (actorSkills.Count == 0)
            return;

        for(int i=0;i<actorSkills.Count;++i)
        {
            if (Input.GetKeyDown(actorSkills[i].keyCode))
            {
                if (actorSkills[i].action != null)
                {
                    actorSkills[i].action.Invoke();
                    Notify();
                }
            }
        }        
    }
}
[System.Serializable]
public abstract class ActorSkill 
{        
    public string skillName;
    public KeyCode keyCode;    
    public string iconName;
    public UnityAction action;
}
[System.Serializable]
public class UnitSkill:ActorSkill
{
    protected Unit unit;
    public UnitSkill(KeyCode key ,Unit unit, string skillName, 
        string iconName, UnityAction ac) 
    {
        this.keyCode = key;
        this.unit = unit;
        this.skillName = skillName;
        this.iconName = iconName;
        this.action = ac;
    }        
}
[System.Serializable]
public class FarmerSkill : ActorSkill
{
    protected Farmer farmer;
    public FarmerSkill(KeyCode key, Farmer farmer, string skillName,
        string iconName, UnityAction ac)
    {
        this.keyCode = key;
        this.farmer = farmer;
        this.skillName = skillName;
        this.iconName = iconName;
        this.action = ac;
    }
}
[System.Serializable]
public class BuildingSkill : ActorSkill
{
    protected Building building;
    public BuildingSkill(KeyCode key, Building building, string skillName,
        string iconName, UnityAction ac)
    {
        this.keyCode = key;
        this.building = building;
        this.skillName = skillName;
        this.iconName = iconName;
        this.action = ac;
    }
}
[System.Serializable]
public class ProBuildingSkill:ActorSkill
{
    protected ProBuilding building;
    Enums.UnitType unitType;
    public ProBuildingSkill(KeyCode key, ProBuilding building, string skillName,
        string iconName, Enums.UnitType type)
    {
        this.keyCode = key;
        this.building = building;
        this.skillName = skillName;
        this.iconName = iconName;
        this.unitType = type;
        this.action = () => { building.Production.EnQueue(unitType); };
    }
}
