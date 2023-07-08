using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest_Base : Command
{
    protected readonly QuestManager questManager;
    public Quest_Base(QuestManager manager)
    {
        questManager = manager;
    }
    public override void Execute()
    {
    
    }

    public override void Update()
    {
    
    }
}
