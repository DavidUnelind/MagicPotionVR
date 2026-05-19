using UnityEngine;
using UnityEngine.UI;
using Oculus.Interaction;

public class Shaker : MonoBehaviour
{
    private Rigidbody shaker;

    [SerializeField] public Slider progressBar;
    [SerializeField] public AudioClip shakerSound;

    public GameObject canvas;

    private float shakingTime = 0f;

    private float shakingIntensity = 0.5f;

    public bool isDoneShaking = false;

    private Vector3 lastPos;

    void Start()
    {
        shaker = GetComponent<Rigidbody>();

        shaker.collisionDetectionMode = CollisionDetectionMode.Continuous;

        lastPos = transform.position;

        canvas.SetActive(false);

        progressBar.minValue = 0f;

        progressBar.maxValue = 1f;

        progressBar.value = 0f;
    }

    void Update()
    {
        float speed =
            (transform.position - lastPos).magnitude / Time.deltaTime;

        lastPos = transform.position;

        if (speed > shakingIntensity)
        {
            canvas.SetActive(true);

            shakingTime += Time.deltaTime;
        }

        shakingTime = Mathf.Clamp(shakingTime, 0f, 3f);

        progressBar.value = shakingTime / 3f;

        if (shakingTime >= 3f)
        {
            isDoneShaking = true;

            canvas.SetActive(false);
        }
    }

    public void ResetShaker()
    {
        shakingTime = 0f;

        isDoneShaking = false;

        progressBar.value = 0f;

        canvas.SetActive(false);
    }
}