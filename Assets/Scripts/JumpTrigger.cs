using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class JumpTrigger : MonoBehaviour
{
    public AudioSource screamer;
    public GameObject jumpCam;
    public GameObject enemy;
    public AudioSource Noises;

    private void Start()
    {
        jumpCam.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        GetComponent<BoxCollider>().enabled = false;
        screamer.Play();
        Noises.Stop();
        jumpCam.SetActive(true);
        enemy.SetActive(false);
        StartCoroutine(GameOver());

    }

    IEnumerator GameOver()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(1);
    }
}
