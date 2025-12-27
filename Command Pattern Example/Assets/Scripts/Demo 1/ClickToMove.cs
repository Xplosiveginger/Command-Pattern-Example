using UnityEngine;
using UnityEngine.AI;

public class ClickToMove : MonoBehaviour
{
    [SerializeField] LayerMask groundMask;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] GameObject clickFX;

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Vector3 dest = Vector3.zero;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit, 999f, groundMask))
            {
                dest = hit.point;
                Instantiate(clickFX, hit.point + new Vector3(0f, 0.5f, 0f), Quaternion.identity);
                agent.SetDestination(dest);
            }
        }
    }
}
