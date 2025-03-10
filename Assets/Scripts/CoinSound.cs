using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] AudioClip coinSound;  // Sound for collecting the coin
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the collider is the rocket
        if (other.CompareTag("Rocket"))
        {
            // Play the coin sound when collected
            if (audioSource && coinSound)
            {
                audioSource.PlayOneShot(coinSound);
            }

            // Destroy the coin after it's collected
            Destroy(gameObject);
        }
    }
}
