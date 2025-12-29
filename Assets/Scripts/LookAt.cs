using UnityEngine;

public class LookAt : MonoBehaviour
{
    public float followStrength = 20f;
    Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        Vector2 mouseWorld =Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector2 direction = mouseWorld - rb.position;
        rb.AddForce(direction * followStrength);
    }
}