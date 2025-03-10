using UnityEngine;

public class RotateCoin : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 10f; // Rotation speed
    [SerializeField] Vector3 rotationAxis = new Vector3(0, 1, 0); // The axis to rotate around, default Y axis

    void Update()
    {
        // Apply rotation around the specified axis
        transform.Rotate(rotationAxis * rotationSpeed * Time.deltaTime);
    }
}
