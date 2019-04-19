using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {

    public GameObject audioOn;
    public GameObject audioOff;
    public Text txtHighScore;
    public GameObject btnQuit;

	// Use this for initialization
	void Start () {
        setSoundState();

        txtHighScore.text = PlayerPrefs.GetFloat("HighScore", 0).ToString("0000");
    }
	
	// Update is called once per frame
	void Update () {

    }

    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void toggleSound()
    {
        if (PlayerPrefs.GetInt("Muted", 0) == 0)
        {
            PlayerPrefs.SetInt("Muted", 1);
        }
        else
        {
            PlayerPrefs.SetInt("Muted", 0);
        }

        setSoundState();
    }

    private void setSoundState()
    {
        if(PlayerPrefs.GetInt("Muted", 0) == 0)
        {
            AudioListener.volume = 1;
            audioOn.SetActive(true);
            audioOff.SetActive(false);
        }
        else
        {
            AudioListener.volume = 0;
            audioOn.SetActive(false);
            audioOff.SetActive(true);
        }
    }

    public void quitGame()
    {
        Application.Quit();
    }
}
