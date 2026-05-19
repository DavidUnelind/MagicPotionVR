using UnityEngine;
using UnityEngine.UI;

public class Stick : MonoBehaviour
{
    [Header("UI")]
    public Slider progressBar;
    public GameObject canvas;

    [Header("Stir Settings")]
    public Transform cauldronCenter;
    public float requiredTime = 3f;
    public float angleThreshold = 5f;

    private float stirringTime = 0f;
    public bool isDoneShaking = false;

    private bool inLiquid = false;
    private Vector3 lastDir;

    void Start()
    {
        canvas.SetActive(false);

        progressBar.minValue = 0f;
        progressBar.maxValue = 1f;
        progressBar.value = 0f;

        lastDir = Vector3.zero;
    }

    void Update()
    {
        if (!inLiquid || isDoneShaking) return;

        Vector3 fromCenter = transform.position - cauldronCenter.position;
        fromCenter.y = 0f;

        if (fromCenter.sqrMagnitude < 0.01f)
            return;

        Vector3 currentDir = fromCenter.normalized;

        if (lastDir == Vector3.zero)
        {
            lastDir = currentDir;
            return;
        }

        float angleDelta = Vector3.SignedAngle(lastDir, currentDir, Vector3.up);
        lastDir = currentDir;

        float stirStrength = Mathf.Abs(angleDelta);

        if (stirStrength > angleThreshold)
        {
            canvas.SetActive(true);
            stirringTime += Time.deltaTime * Mathf.Clamp01(stirStrength / 45f);
        }

        stirringTime = Mathf.Clamp(stirringTime, 0f, requiredTime);
        progressBar.value = stirringTime / requiredTime;

        if (stirringTime >= requiredTime)
        {
            isDoneShaking = true;
            canvas.SetActive(false);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Cauldron"))
        {
            inLiquid = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Cauldron"))
        {
            inLiquid = false;
            lastDir = Vector3.zero;
        }
    }

    public void ResetShaker()
    {
        stirringTime = 0f;
        isDoneShaking = false;
        progressBar.value = 0f;
        canvas.SetActive(false);
    }
}