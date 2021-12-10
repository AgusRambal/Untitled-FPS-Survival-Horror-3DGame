using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GrabShells : MonoBehaviour
{
    public float TheDistance;
    public GameObject ActionDisplay;
    public GameObject ActionText;
    public GameObject Shells;
    public GameObject Aim;
    public AudioSource GrabbingShells;

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
            if (TheDistance <= 3)
            {
                this.GetComponent<BoxCollider>().enabled = false;
                ActionDisplay.SetActive(false);
                ActionText.SetActive(false);
                Aim.SetActive(false);
                GrabbingShells.Play();
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
