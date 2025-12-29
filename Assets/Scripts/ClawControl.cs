using UnityEngine;
using System.Collections;

public class ClawControl : MonoBehaviour
{
    [Header("Claw Parts")]
    public GameObject leftClaw;
    public GameObject rightClaw;

    [Header("Rotation Settings")]
    public float maxRotation = 25f;
    public float step = 1f;
    public float delay = 0.02f;

    [Header("Movement Settings")]
    public float moveSpeed = 10f;
    public float leftBorder = -8f;
    public float rightBorder = 8f;

    private Coroutine rotateCoroutine;
    private Camera mainCam;

    void Start()
    {
        mainCam = Camera.main;
    }

    void Update()
    {
        HandleMovement();
        HandleRotation();
    }

    // ---------------- MOVEMENT ----------------
    void HandleMovement()
    {
        // A = -1, D = +1
    float inputX = Input.GetAxis("Horizontal");

    Vector3 newPos = transform.position;
    newPos.x += inputX * moveSpeed * Time.deltaTime;

    // Clamp within borders
    newPos.x = Mathf.Clamp(newPos.x, leftBorder, rightBorder);

    transform.position = newPos;
    }

    // ---------------- ROTATION ----------------
    void HandleRotation()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (rotateCoroutine != null)
                StopCoroutine(rotateCoroutine);

            rotateCoroutine = StartCoroutine(CloseClaws());
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (rotateCoroutine != null)
                StopCoroutine(rotateCoroutine);

            rotateCoroutine = StartCoroutine(OpenClaws());
        }
    }

    IEnumerator CloseClaws()
    {
        for (float z = 0f; z <= maxRotation; z += step)
        {
            leftClaw.transform.localRotation =
                Quaternion.Euler(0f, 0f, z);

            rightClaw.transform.localRotation =
                Quaternion.Euler(0f, 0f, -z);

            yield return new WaitForSeconds(delay);
        }
    }

    IEnumerator OpenClaws()
    {
        for (float z = maxRotation; z >= 0f; z -= step)
        {
            leftClaw.transform.localRotation =
                Quaternion.Euler(0f, 0f, z);

            rightClaw.transform.localRotation =
                Quaternion.Euler(0f, 0f, -z);

            yield return new WaitForSeconds(delay);
        }
    }
}
