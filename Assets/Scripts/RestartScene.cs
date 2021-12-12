using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class RestartScene : MonoBehaviour
{
    public AudioSource buttonSound;
    private void Start()
    {
       Cursor.lockState = CursorLockMode.None;
    }

    public void cambioBTN() //Utilizo el boton para cambiar a la escena eleigiendo su nombre y pongo en 1 el tiempo del juego debido a un bug con la pausa
    {
        SceneManager.LoadScene(1);
        buttonSound.Play();
        //Time.timeScale = 1f;
    }

    public void Menu()
    {
        SceneManager.LoadScene(0);
        buttonSound.Play();
    }
}
