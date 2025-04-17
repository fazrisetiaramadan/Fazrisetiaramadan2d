using UnityEngine;

public class FloatingObject : MonoBehaviour
{
    public float moveSpeed = 2f;     
    public float moveDistance = 1f;  

    private float startY;

    void Start()
    {
        startY = transform.position.y; 
    }

    void Update()
    {
        // Hitung posisi Y baru dengan gerakan naik turun
        float newY = startY + Mathf.PingPong(Time.time * moveSpeed, moveDistance * 2) - moveDistance;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
