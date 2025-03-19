using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{

    [SerializeField] GameObject MainMenUGO;
    [SerializeField] GameObject OptionsMenuGO;

    private void Start()
    {
       MainMenUGO.SetActive(true);
       OptionsMenuGO.SetActive(false);
    }

    public void OpenLevel() {
        SceneManager.LoadScene("Level01");

        // Application.Quit();
    }
}
