using UnityEngine;

public class LandingPad : MonoBehaviour
{
    [SerializeField] private AudioClip successSound; // Assign sound in Inspector
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>(); // Get the AudioSource component
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Rocket")) // Ensure Rocket has "Rocket" tag
        {
            Rigidbody rocketRB = collision.gameObject.GetComponent<Rigidbody>();
            
            // Play sound only if the rocket is moving (not at start)
            if (rocketRB.velocity.magnitude > 0.5f) 
            {
                if (!audioSource.isPlaying) 
                {
                    audioSource.PlayOneShot(successSound); // Play landing sound
                }
            }
        }
    }
}
