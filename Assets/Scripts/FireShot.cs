using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static GrabShells;

public class FireShot : MonoBehaviour
{
    public Animator weaponAnimator;
    public GameObject Shotgun;
    public GameObject MuzzleFlash;
    public AudioSource ShotgunShot;
    public bool isFiring = false;
    public TextMeshProUGUI balas;
    public float disparos = 2;
    public GrabShells grabShells;
    public AudioSource empty;
    
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (isFiring == false)
            {
                StartCoroutine(FiringShotgun());
            }

            if (grabShells.isLoaded == false)
            {
                empty.Play();
            }

        }
    }

    IEnumerator FiringShotgun()
    {
        if (disparos >= 1 && grabShells.isLoaded == true)
        {
            isFiring = true;
            Shotgun.GetComponent<Animation>().Play("Shot");
            MuzzleFlash.SetActive(true);
            MuzzleFlash.GetComponent<Animation>().Play("MuzzleAnim");
            disparos--;
            balas.text = disparos.ToString();
            //HAY QUE PONER IF DE QUE PASA SI NO TENGO BALAS
            ShotgunShot.Play();
            yield return new WaitForSeconds(0.5f);
            isFiring = false;
        }
    }
}
