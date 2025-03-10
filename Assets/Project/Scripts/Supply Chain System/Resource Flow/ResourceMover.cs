using System.Collections.Generic;
using UnityEngine;

public class ResourceMover : MonoBehaviour
{
    [SerializeField]private List<Vector3> path = new List<Vector3>();
    private int currentTargetIndex = 0;
    private Resource resource;

    public void Initialize(List<Vector3> pathPoints)
    {
        path = pathPoints;
        currentTargetIndex = 0;
    }

    private void Awake()
    {
        resource = GetComponent<Resource>();
    }

    private void Update()
    {
        if (path == null || path.Count == 0)
            return;

        Vector3 targetPos = path[currentTargetIndex];
        targetPos.z = 0;
        transform.position = Vector3.MoveTowards(transform.position, targetPos, resource.moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPos) < 0.1f && currentTargetIndex != path.Count-1)
        {
            currentTargetIndex++;
            // Once the resource reaches the end of the path, you can implement additional logic if needed.
        }
    }
}
