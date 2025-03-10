using UnityEngine;

public class RotateObject : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 10f; // Rotation speed

    void Update()
    {
        // Apply rotation to the object around its Y axis (or adjust to other axes if needed)
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }
}
