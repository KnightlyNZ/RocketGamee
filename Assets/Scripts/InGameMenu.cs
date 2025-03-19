using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameMenu : MonoBehaviour
{

    [SerializeField]  GameObject PauseMenu;
    [SerializeField] GameObject GameSettings;
    // Start is called before the first frame update
    void Start()
    {
        PauseMenu.SetActive(false);
        GameSettings.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Escape)) {
            Time.timeScale = 0.0f;
            PauseMenu.SetActive(true);

        }
    }

    public void ResumeGame() {
        Time.timeScale = 1.0f;
        PauseMenu.SetActive(false);
    }

    public void QuitGameMenu() {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("MainMenuScene");
    }

    public void MuteValueChange() {
        Debug.Log("Mute / Unmute");
        Camera camera = FindFirstObjectByType<Camera>();
        if (camera != null) {
            AudioListener cameraAudio = camera.GetComponent<AudioListener>();
            cameraAudio.enabled = cameraAudio.enabled? false :true;
            // cameraAudio.enabled = false;
        }

    }
    

}
