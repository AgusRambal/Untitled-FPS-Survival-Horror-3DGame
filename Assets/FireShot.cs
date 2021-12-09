using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireShot : MonoBehaviour
{
    public Animator weaponAnimator;
    public GameObject Shotgun;
    public GameObject MuzzleFlash;
    public AudioSource ShotgunShot;
    public bool isFiring = false;
    

    
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (isFiring == false)
            {
                StartCoroutine(FiringShotgun());
            }
        }
    }

    IEnumerator FiringShotgun()
    {
        isFiring = true;
        Shotgun.GetComponent<Animation>().Play("Shot");
        MuzzleFlash.SetActive(true);
        MuzzleFlash.GetComponent<Animation>().Play("MuzzleAnim");
       
        //HAY QUE PONER IF DE QUE PASA SI NO TENGO BALAS

        ShotgunShot.Play();
        yield return new WaitForSeconds(0.5f);
        isFiring = false;
    }
}
