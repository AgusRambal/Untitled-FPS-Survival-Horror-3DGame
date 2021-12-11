using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScript : MonoBehaviour
{
    public GameObject loadButton;
    public GameObject fadeOut;
    public AudioSource buttonSound;
    public GameObject loadText;
    public AudioSource menuMusic;
    public int loadInt;

    private void Start()
    {
        loadInt = PlayerPrefs.GetInt("AutoSave");

        if (loadInt > 0)
        {
            loadButton.SetActive(true);
        }
    }

    public void LoadGameButton()
    {
        StartCoroutine(LoadGameStart());
    }

    IEnumerator LoadGameStart()
    {
        fadeOut.SetActive(true);
        buttonSound.Play();
        yield return new WaitForSeconds(3);
        loadText.SetActive(true);
        menuMusic.Stop();
        SceneManager.LoadScene(loadInt);
    }

}
