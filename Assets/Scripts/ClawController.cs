using UnityEngine;
using System.Collections;

public class ClawController : MonoBehaviour
{
    [Header("Audio Settings")]
    public AudioSource moveAudioSource;
    public AudioSource clawActionAudio;   // Single sound for both Open/Close

    [Header("Claw Parts")]
    public Rigidbody2D topRod;
    public Rigidbody2D lowerRod;
    public GameObject leftClaw;
    public GameObject rightClaw;

    [Header("Claw Rotation Settings")]
    public float maxRotation = 25f;
    public float step = 1f;
    public float delay = 0.02f;
    
    [Header("Top Rod Movement")]
    public float moveSpeed = 5f;
    public float leftBorder = -8f;
    public float rightBorder = 8f;

    [Header("Lower Rod Mouse Aim")]
    public float followStrength = 8f;

    private float horizontalInput;
    private Coroutine rotateCoroutine;

    void Update()
    {
        ReadInput();
        RotateLowerRodToMouse();
        HandleClawInput();
        HandleMovementAudio();
     
    }

    // Used for consistent physics updates
    void FixedUpdate()
    {
        MoveTopRod();
    }

    //---------------- MOVEMENT & ROTATION ----------------
    void ReadInput() => horizontalInput = Input.GetAxis("Horizontal");

    void MoveTopRod()
    {
        Vector3 newPos = transform.position;
        newPos.x += horizontalInput * moveSpeed * Time.deltaTime;
        newPos.x = Mathf.Clamp(newPos.x, leftBorder, rightBorder);
        transform.position = newPos;
    }

    void RotateLowerRodToMouse()
    {
        //Screen to world point helps convert mouse position to game world coordinates
        Vector2 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mouseWorld - lowerRod.position;
        lowerRod.AddForce(direction * followStrength);
    }

    void HandleClawInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //Coroutine function to smoothly rotate claws
            if (rotateCoroutine != null) StopCoroutine(rotateCoroutine);

           if (clawActionAudio != null) 
            {
                clawActionAudio.loop = true; // Set to loop while button is down
                clawActionAudio.Play();
            }

            rotateCoroutine = StartCoroutine(RotateClaws(maxRotation));
        }

        if (Input.GetMouseButtonUp(0))
        {
         
            if (rotateCoroutine != null) StopCoroutine(rotateCoroutine);

           if (clawActionAudio != null) 
            {
                clawActionAudio.Stop(); // Stop immediately when key is released
            }

            rotateCoroutine = StartCoroutine(RotateClaws(0f));
        }
    }


    //IEnumerator to smoothly rotate claws to target angle.
    IEnumerator RotateClaws(float targetZ)
    {
        float startZ = leftClaw.transform.localRotation.eulerAngles.z;
        if (startZ > 180) startZ -= 360; 

        float t = 0;
        while (t < 1)
        {
            t += step * delay;
            float currentZ = Mathf.MoveTowards(startZ, targetZ, t * maxRotation);
            
            leftClaw.transform.localRotation = Quaternion.Euler(0, 0, currentZ);
            rightClaw.transform.localRotation = Quaternion.Euler(0, 0, -currentZ);
            yield return new WaitForSeconds(delay);
        }
    }
    void HandleMovementAudio()
{
    if (Mathf.Abs(horizontalInput) > 0.1f)
    {
        if (!moveAudioSource.isPlaying) moveAudioSource.Play();
        moveAudioSource.volume = Mathf.Lerp(moveAudioSource.volume, 1f, Time.deltaTime * 10f);
    }
    else
    {
        moveAudioSource.volume = Mathf.Lerp(moveAudioSource.volume, 0f, Time.deltaTime * 10f);
        if (moveAudioSource.volume < 0.01f) moveAudioSource.Stop();
    }
}
}