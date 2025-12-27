using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ClickManager : MonoBehaviour
{
    public static ClickManager instance;
    public LayerMask groundMask;
    public NavMeshAgent agent;
    public GameObject clickFX;

    public Queue<Command> clickQueue = new Queue<Command>();
    private Command activeCommand;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit, 99f, groundMask))
            {
                Instantiate(clickFX, hit.point + new Vector3(0f, 0.5f, 0f), Quaternion.identity);
                Command command = new Command(hit.point, agent);
                clickQueue.Enqueue(command);
            }
        }

        ProcessQueue();
    }

    private void ProcessQueue()
    {
        if (activeCommand != null && !activeCommand.isFinished) return;

        if (clickQueue.Count == 0) return;

        activeCommand = clickQueue.Dequeue();
        activeCommand.Execute();
    }
}
