using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GrabShells : MonoBehaviour
{
    public float TheDistance;
    public GameObject ActionDisplay;
    public GameObject ActionText;
    public GameObject Shells;
    public GameObject Aim;
    public AudioSource GrabbingShells;
    public TextMeshProUGUI balas;
    public bool isLoaded = false;

     void Update()
    {
        TheDistance = PlayerCasting.DistanceFromTarget;
    }

    void OnMouseOver()
    {
        if (TheDistance  <= 100)
        {

            ActionDisplay.SetActive(true);
            ActionText.SetActive(true);
            Aim.SetActive(false);
        }

        if (Input.GetButtonDown("Action")) 
        {
            if (TheDistance <= 100)
            {
                isLoaded = true;
                Shells.SetActive(false);
                ActionDisplay.SetActive(false);
                ActionText.SetActive(false);
                Aim.SetActive(true);
                GrabbingShells.Play();
                Destroy(Shells);
                balas.text = 2.ToString();
            }
        }
    }

    void OnMouseExit()
    {
        ActionDisplay.SetActive(false);
        ActionText.SetActive(false);
        Aim.SetActive(true);
    }
}
