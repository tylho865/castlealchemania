using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; 
    public float smoothSpeed = 0.125f; //how fast camera follows
    public Vector3 offset; // offset from the player

    private float fixedY; // fixed y position

    void Start()
    {
        // set the fixed y position
        fixedY = transform.position.y;
    }

    void LateUpdate()
    {
        if (player != null)
        {
            // calculuate the move distance
            Vector3 desiredPosition = player.position + offset;
            Debug.Log(player.position.y);
            //keep the y position
            desiredPosition.y = fixedY;
            // keep the z position
            desiredPosition.z = transform.position.z;

            // move the player
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }
    }
}
