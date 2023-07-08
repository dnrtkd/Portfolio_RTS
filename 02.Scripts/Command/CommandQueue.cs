using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//명령을 큐로 저장하는 자료구조
public class CommandQueue : ICommand
{
    //현재 명령
    protected ICommand currCommand;
    protected Queue<ICommand> commandList = new Queue<ICommand>();
    //명령 숫자 제한
    protected int maxNumber;

    public CommandQueue()
    {
        this.maxNumber = 100;
    }
    public CommandQueue(int maxNumber)
    {
        this.maxNumber = maxNumber;
    }

    public void Cancel()
    {
        if (currCommand != null)
            currCommand.Cancel();
        commandList.Clear();
    }

    public ICommand GetCurrCommand()
    {
        return currCommand;
    }
    public void Execute()
    {
        
    }

    public bool isFinished()
    {
        if (commandList.Count <= 0 && currCommand == null)
            return true;
        return false;
    }

    public void Add(ICommand _command)
    {
        if (_command == null || maxNumber == commandList.Count)
            return;
        commandList.Enqueue(_command);
    }

    public void Update()
    {
        if (isFinished())
            return;

        if (currCommand == null || currCommand.isFinished())
        {
            if(commandList.Count>0)
            {
                currCommand = commandList.Dequeue();
            }
            else
            {
                currCommand = null;
            }

            if (currCommand != null)
                currCommand.Execute();
        }
        if (currCommand != null)
            currCommand.Update();
    }


}
