using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class Character : MonoBehaviour
{
    NavMeshAgent agent;

    Queue<Command> commandQueue = new Queue<Command>();
    Command activeCommand;

    public static event Action<Character> OnCharacterClicked; 
    public static event Action<Character> OnCharacterRegistered;
    public static event Action<Character> OnCharacterDeregistered;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void OnMouseDown()
    {
        OnCharacterClicked?.Invoke(this);
    }

    public void QueueCommand(Vector3 dest)
    {
        Command command = new Command(dest, agent);
        QueueCommand(command);
    }

    public void QueueCommand(Command command)
    {
        if(commandQueue.Count == 0) OnCharacterRegistered?.Invoke(this);
        commandQueue.Enqueue(command);
    }

    public void ProcessQueue()
    {
        if (activeCommand != null && !activeCommand.isFinished) return;

        if (commandQueue.Count == 0)
        {
            OnCharacterDeregistered?.Invoke(this);
            return;
        }

        activeCommand = commandQueue.Dequeue();
        activeCommand.Execute();
    }
}
