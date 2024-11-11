using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class CameraEdgeColliders : MonoBehaviour
{
    public Camera mainCamera; // Assign the Main Camera in the Inspector
    public GameObject leftCollider; // Assign the LeftCollider GameObject in the Inspector
    public GameObject rightCollider; // Assign the RightCollider GameObject in the Inspector

    void Start()
    {
        PositionColliders();
    }

    void PositionColliders()
    {
        // Calculate the positions
        Vector3 leftEdge = mainCamera.ViewportToWorldPoint(new Vector3(0, 0.5f, mainCamera.nearClipPlane));
        Vector3 rightEdge = mainCamera.ViewportToWorldPoint(new Vector3(1, 0.5f, mainCamera.nearClipPlane));

        // Position colliders
        leftCollider.transform.position = new Vector3(leftEdge.x -0.5f, mainCamera.transform.position.y, 0);
        rightCollider.transform.position = new Vector3(rightEdge.x +0.5f, mainCamera.transform.position.y, 0);

        // Adjust the size of the colliders to cover the viewport height
        BoxCollider2D leftBoxCollider = leftCollider.GetComponent<BoxCollider2D>();
        BoxCollider2D rightBoxCollider = rightCollider.GetComponent<BoxCollider2D>();

        if (leftBoxCollider != null && rightBoxCollider != null)
        {
            float height = mainCamera.orthographicSize * 2;
            leftBoxCollider.size = new Vector2(1, height);
            rightBoxCollider.size = new Vector2(1, height);
        }
    }
}