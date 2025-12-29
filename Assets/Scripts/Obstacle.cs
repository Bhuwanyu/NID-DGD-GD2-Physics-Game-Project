using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float minSize = 0.5f;
    public float maxSize = 3.0f;
    Rigidbody2D rb;
    public float minSpeed = 50f;
    public float maxSpeed = 200f;
    public float minRotationSpeed = 10f;
    public float maxRotationSpeed = 200f;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        float randomSize = Random.Range(minSize,maxSize);
        transform.localScale = new Vector3(randomSize,randomSize,1);
        rb = GetComponent<Rigidbody2D>();
        float randomSpeed = Random.Range(minSpeed,maxSpeed)/randomSize;
        float randomTorque = Random.Range(minRotationSpeed,maxRotationSpeed);
        rb.AddTorque(randomTorque);
        Vector2 randomDirection = Random.insideUnitCircle;
        rb.AddForce(randomDirection * randomSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
