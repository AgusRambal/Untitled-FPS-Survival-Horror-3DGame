using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GrabShells2 : MonoBehaviour
{
    public float TheDistance;
    public GameObject ActionDisplay;
    public GameObject ActionText;
    public GameObject Shells;
    public GameObject Aim;
    public AudioSource GrabbingShells;
    public AudioSource Dialogue3;
    public bool isLoaded2 = false;
    public bool Taken2 = false;

    void Update()
    {
        TheDistance = PlayerCasting.DistanceFromTarget;
    }

    void OnMouseOver()
    {
        if (TheDistance <= 3)
        {
            ActionDisplay.SetActive(true);
            ActionText.SetActive(true);
            Aim.SetActive(false);
        }

        if (Input.GetButtonDown("Action"))
        {
            if (TheDistance <= 3)
            {
                Taken2 = true;
                GrabbingShells.Play();
                Dialogue3.Play();
                isLoaded2 = true;
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
