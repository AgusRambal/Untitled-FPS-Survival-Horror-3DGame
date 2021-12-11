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
    public bool isLoaded = false;
    public bool Taken = false;

     void Update()
    {
        TheDistance = PlayerCasting.DistanceFromTarget;
    }

    void OnMouseOver()
    {
        if (TheDistance  <= 10)
        {
            ActionDisplay.SetActive(true);
            ActionText.SetActive(true);
            Aim.SetActive(false);
        }

        if (Input.GetButtonDown("Action")) 
        {
            if (TheDistance <= 10)
            {
                Taken = true;
                GrabbingShells.Play();
                isLoaded = true;
                Shells.SetActive(false);
                ActionDisplay.SetActive(false);
                ActionText.SetActive(false);
                Aim.SetActive(true);
                Destroy(Shells);
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
