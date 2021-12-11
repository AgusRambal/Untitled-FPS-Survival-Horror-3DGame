using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSelection : MonoBehaviour
{
    public GameObject newGameText;
    public GameObject OptionsText;
    public GameObject CreditsText;
    public GameObject QuitText;
    public GameObject BackButton;
    public GameObject creditsText;
    public GameObject loadTextButton;
    public AudioSource buttonSound;
    public GameObject fadeOut;
    public GameObject loadText;
    public GameObject loadFakeText;
    public AudioSource menuMusic;

    public void NewGame()
    {
        StartCoroutine(NewGameStart());
    }

    public void Options()
    {
        buttonSound.Play();
    }

    public void Credits()
    {
        newGameText.SetActive(false);
        OptionsText.SetActive(false);
        CreditsText.SetActive(false);
        QuitText.SetActive(false);
        loadTextButton.SetActive(false);
        loadFakeText.SetActive(false);

        BackButton.SetActive(true);
        creditsText.SetActive(true);

        buttonSound.Play();
    }

    public void Back()
    {
        BackButton.SetActive(false);
        newGameText.SetActive(true);
        OptionsText.SetActive(true);
        CreditsText.SetActive(true);
        QuitText.SetActive(true);
        creditsText.SetActive(false);
        loadTextButton.SetActive(true);
        loadFakeText.SetActive(true);

        buttonSound.Play();
    }

    public void QuitApp()
    {
        buttonSound.Play();
        Debug.Log("Quit");
        Application.Quit();
    }

    IEnumerator NewGameStart()
    {
        fadeOut.SetActive(true);
        buttonSound.Play();
        yield return new WaitForSeconds(3);
        loadText.SetActive(true);
        menuMusic.Stop();
        SceneManager.LoadScene(1);
    }
}
