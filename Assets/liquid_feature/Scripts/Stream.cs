using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Stream : MonoBehaviour
{
    [SerializeField] private ParticleSystem particles;    
    private LineRenderer lineRenderer = null;

    private Vector3 targetPosition = Vector3.zero;
    public float widthMultiplier = 1.0f;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.widthMultiplier = widthMultiplier;
    }


    private void Start()
    {
        MoveToPosition(0, transform.position);
        MoveToPosition(1, transform.position);
        
        
    }

    public void Begin()
    {
        StartCoroutine(BeginPour());
    }

    private IEnumerator BeginPour()
    {
        while(gameObject.activeSelf)
        {
            targetPosition = FindEndPoint();
            MoveToPosition(0, transform.position);
            MoveToPosition(1, targetPosition);
            particles.transform.position = targetPosition;
            

            yield return null;    
        }
        
    }

    // shoot a ray downwards to fins endpoint
    private Vector3 FindEndPoint()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, Vector3.down);

        Physics.Raycast(ray, out hit, 10.0f);
        Vector3 endPoint = hit.collider ? hit.point : ray.GetPoint(10.0f);
//
        return endPoint;
        //
    }

    private void MoveToPosition(int index, Vector3 targetPosition)
    {
        lineRenderer.SetPosition(index, targetPosition);
        
    }
}
