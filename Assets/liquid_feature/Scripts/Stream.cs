using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;

public class Stream : MonoBehaviour
{
    [SerializeField] private ParticleSystem particles;
    private Recipe recipe;
    [SerializeField] private string color;

    private LineRenderer lineRenderer = null;

    private Vector3 targetPosition = Vector3.zero;
    public float widthMultiplier;
    private float scaleFactor;

    public void Init(Recipe newRecipe)
    {
        recipe = newRecipe;
    }
    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        scaleFactor = transform.lossyScale.x;
        lineRenderer.widthMultiplier = widthMultiplier * scaleFactor;

        var main = particles.main;
        main.startSizeMultiplier = scaleFactor;
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

        float value = 100.0f * scaleFactor;
        Physics.Raycast(ray, out hit, value);
        Vector3 endPoint = hit.collider ? hit.point : ray.GetPoint(value);

        if (hit.collider && hit.collider.CompareTag("Cauldron"))
        {
            Debug.Log("Hit the cauldron!");
            recipe.addIngredient(color);
        }

        return endPoint;
    }

    private void MoveToPosition(int index, Vector3 targetPosition)
    {
        lineRenderer.SetPosition(index, targetPosition);
    }
}