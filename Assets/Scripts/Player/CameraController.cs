using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Room Camera Settings")]
    [SerializeField] private float speed;
    private float currentPosX;
    private Vector3 velocity = Vector3.zero;

    [Header("Follow Player Settings")]
    [SerializeField] private Transform player;
    [SerializeField] private float aheadDistance = 2f;
    [SerializeField] private float cameraSpeed = 3f;

    [Header("Camera Bounds")] 
    [SerializeField] private float minX; 
    [SerializeField] private float maxX; 
    private float lookAhead;

    private void Update()
    {
        // Posisi target kamera mengikuti pemain
        float targetX = player.position.x + lookAhead;

        // Batasi agar kamera tidak melewati batas kiri dan kanan
        targetX = Mathf.Clamp(targetX, minX, maxX);

        // Update posisi kamera
        transform.position = new Vector3(targetX, transform.position.y, transform.position.z);

        // Efek smooth mengikuti pergerakan pemain
        lookAhead = Mathf.Lerp(lookAhead, (aheadDistance * player.localScale.x), Time.deltaTime * cameraSpeed);
    }

    public void MoveToNewRoom(Transform _newRoom)
    {
        currentPosX = _newRoom.position.x;
    }
}
