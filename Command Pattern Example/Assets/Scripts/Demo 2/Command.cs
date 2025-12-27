using UnityEngine;
using UnityEngine.AI;

public class Command
{
    public Vector3 dest;
    public NavMeshAgent agent;

    public bool isFinished => agent.remainingDistance <= 0.1f;

    public Command(Vector3 dest, NavMeshAgent agent)
    {
        this.dest = dest;
        this.agent = agent;
    }

    public void Execute()
    {
        agent.SetDestination(dest);
    }
}
