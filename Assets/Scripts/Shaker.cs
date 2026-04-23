using UnityEngine;
using UnityEngine.UI;
using Oculus.Interaction;

public class Shaker : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Rigidbody shaker;
    [SerializeField] public Slider progressBar; 
    public GameObject canvas; 
    private float shakingTime = 0f;
    private float shakingIntensity = 2f;
    public bool isDoneShaking = false; 
    private Vector3 lastPos;
    void Start()
    {
        shaker = GetComponent<Rigidbody>();
        lastPos = transform.position;
        canvas.SetActive(false);
        progressBar.minValue = 0f; 
        progressBar.maxValue = 1f; 
        progressBar.value = 0f; 
    }

    // Update is called once per frame
    void Update()
    {
        float speed = (transform.position - lastPos).magnitude / Time.deltaTime;
        lastPos = transform.position;

        if (speed > shakingIntensity)
        {
            shakingTime += Time.deltaTime;
        } else
        {
            shakingTime += 0f;
        }

        shakingTime = Mathf.Clamp(shakingTime, 0f, 3f);
        progressBar.value = shakingTime / 3f; 

        if (shakingTime >= 3f)
        {
            isDoneShaking = true;
            canvas.SetActive(false);
        }
    }
}
