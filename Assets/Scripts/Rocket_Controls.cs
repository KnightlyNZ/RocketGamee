using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket_Controls : MonoBehaviour
{
    [SerializeField] float ThrustForce;
    [SerializeField] float RotationForce;
    [SerializeField] AudioClip EngineSound;
    [SerializeField] AudioClip GameOverSound;
    [SerializeField] AudioClip CoinSound;  // Coin sound
    [SerializeField] float FuelAmount = 100f;
    [SerializeField] float FuelConsumptionRate = 1f;
    [SerializeField] float FuelReplenishAmount = 20f;

    Rigidbody RocketRB;
    AudioSource RocketAudio;

    bool RocketCrashed = false;
    int Coins = 0;

    bool IsThrusting = false; // Track if spacebar is being held

    void Start()
    {
        RocketRB = GetComponent<Rigidbody>();
        RocketAudio = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (!RocketCrashed)
        {
            RocketInputControls();
            HandleFuelConsumption();
        }
    }

    private void RocketInputControls()
    {
        // If spacebar is pressed, start applying thrust and playing the engine sound
        if (Input.GetKey(KeyCode.Space) && FuelAmount > 0)
        {
            if (!IsThrusting)  // Only start the sound when space is first pressed
            {
                RocketAudio.loop = true;  // Loop the engine sound
                RocketAudio.clip = EngineSound;
                RocketAudio.Play();
                IsThrusting = true;
            }
            RocketRB.AddRelativeForce(Vector3.forward * Time.deltaTime * ThrustForce);
        }
        else if (IsThrusting) // Stop the sound when spacebar is released
        {
            RocketAudio.Stop();
            IsThrusting = false;
        }

        // Rotate the rocket
        if (Input.GetKey(KeyCode.RightArrow))
        {
            RocketRB.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
            transform.Rotate(Vector3.up * Time.deltaTime * RotationForce);
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            RocketRB.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
            transform.Rotate(Vector3.down * Time.deltaTime * RotationForce);
        }

        RocketRB.constraints = RigidbodyConstraints.None;
    }

    private void HandleFuelConsumption()
    {
        if (Input.GetKey(KeyCode.Space) && FuelAmount > 0)
        {
            FuelAmount -= FuelConsumptionRate * Time.deltaTime;
            FuelAmount = Mathf.Max(FuelAmount, 0);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Handle fuel collection
        if (other.CompareTag("Fuel"))
        {
            FuelAmount += FuelReplenishAmount;
            FuelAmount = Mathf.Min(FuelAmount, 100f);
            Debug.Log("Fuel Collected! Current Fuel: " + FuelAmount);
            Destroy(other.gameObject); // Destroy the fuel item
        }
        
        // Handle coin collection
        else if (other.CompareTag("Coin"))
        {
            Coins += 1;
            Debug.Log("Coin Collected! Total Coins: " + Coins);
            // Play the coin collection sound (plays over the engine sound)
            if (RocketAudio && CoinSound)
            {
                RocketAudio.PlayOneShot(CoinSound); // Coin sound plays over the engine sound
            }
            Destroy(other.gameObject); // Destroy the coin item
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Respawn":
                Debug.Log("Game Over");
                RocketCrashed = true;
                RocketAudio.PlayOneShot(GameOverSound); // Play game over sound
                StartCoroutine(ReloadGame());
                break;
            case "Finish":
                Debug.Log("Level Complete");
                RocketCrashed = true;
                RocketAudio.Stop();
                StartCoroutine(LoadNextLevel());
                break;
            default:
                Debug.Log("No tags");
                break;
        }
    }

    // Coroutine to reload the current level (for failure)
    IEnumerator ReloadGame()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Coroutine to load the next level (for success)
    IEnumerator LoadNextLevel()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
