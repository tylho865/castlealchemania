using UnityEngine;
using UnityEngine.UIElements;

public class arrowFollow : MonoBehaviour
{
    public Transform player;
    public float smoothSpeed = 0.125f; // how fast camera follows
    public Vector3 offset; // default offset from the player
    private Vector3 defaultScale;
    private Renderer arrowRenderer;

    // offsets for each direction
    private Vector3 rightOffset = new Vector3(1, 0, 0);
    private Vector3 leftOffset = new Vector3(-1, 0, 0);
    private Vector3 upOffset = new Vector3(0, 1, 0);
    private Vector3 downOffset = new Vector3(0, -1, 0);
    private Vector3 upRightOffset = new Vector3(0.707f, 0.707f, 0); 
    private Vector3 upLeftOffset = new Vector3(-0.707f, 0.707f, 0);
    private Vector3 downRightOffset = new Vector3(0.707f, -0.707f, 0);
    private Vector3 downLeftOffset = new Vector3(-0.707f, -0.707f, 0);

    void Start()
    {
        defaultScale = transform.localScale;
        arrowRenderer = GetComponent<Renderer>();
        arrowRenderer.enabled = false; // make arrow invisible when game starts
    }

    void LateUpdate()
    {
        if (player != null)
        {
            // calculate the move distance
            Vector3 desiredPosition = player.position + offset;
            Debug.Log(player.position.y);
            // Keep the y position
            desiredPosition.y = player.position.y;
            // Move the player
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }
        if (Input.GetKeyDown(KeyCode.E)){
            arrowRenderer.enabled = false;
        }
        // West, South West, North West haha kanye reference
        if (Input.GetKeyDown(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow))
        {
            transform.rotation = Quaternion.Euler(0, 0, 90);
            offset = leftOffset;
            arrowRenderer.enabled = true;
        }
        else if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.DownArrow))
        {
            transform.rotation = Quaternion.Euler(0, 0, 135);
            offset = downLeftOffset;
            arrowRenderer.enabled = true;
        }
        else if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.UpArrow))
        {
            transform.rotation = Quaternion.Euler(0, 0, 45);
            offset = upLeftOffset;
            arrowRenderer.enabled = true;
        }

        // East, East South, East West
        else if (Input.GetKeyDown(KeyCode.RightArrow) && !Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow))
        {
            transform.rotation = Quaternion.Euler(0, 0, 270);
            offset = rightOffset;
            arrowRenderer.enabled = true;
        }
        else if (Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.UpArrow))
        {
            transform.rotation = Quaternion.Euler(0, 0, 315);
            offset = upRightOffset;
            arrowRenderer.enabled = true;
        }
        else if (Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.DownArrow))
        {
            transform.rotation = Quaternion.Euler(0, 0, 225);
            offset = downRightOffset;
            arrowRenderer.enabled = true;
        }

        // North 
        else if (Input.GetKeyDown(KeyCode.UpArrow) && !Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            offset = upOffset;
            arrowRenderer.enabled = true;
        }

        // South
        else if (Input.GetKeyDown(KeyCode.DownArrow) && !Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
        {
            transform.rotation = Quaternion.Euler(0, 0, 180);
            offset = downOffset;
            arrowRenderer.enabled = true;
        }

        // update the arrow's position
        if (player != null)
        {
            Vector3 desiredPosition = player.position + offset;
            transform.position = desiredPosition;
        }
    }
}
