using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Win : MonoBehaviour
{
    public AudioSource Dialogue4;
    public bool ganaste = false;
    public GameObject fadeOut;

    private void OnTriggerEnter(Collider other)
    {
        Dialogue4.Play();
        fadeOut.SetActive(true);
        StartCoroutine(Final());
    }

    IEnumerator Final()
    {
        yield return new WaitForSeconds(7f);
        SceneManager.LoadScene(3);
    }
}
