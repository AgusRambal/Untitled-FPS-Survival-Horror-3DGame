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
    public float TargetDistance;
    public int DamageAmount = 10;

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
        if (disparos >= 1 && grabShells.isLoaded == true)
        {
            isFiring = true;
            Shotgun.GetComponent<Animation>().Play("Shot");
            MuzzleFlash.SetActive(true);
            MuzzleFlash.GetComponent<Animation>().Play("MuzzleAnim");
            disparos--;
            balas.text = disparos.ToString();
            ShotgunShot.Play();
            yield return new WaitForSeconds(1f);
            isFiring = false;
        }

        else
        {
            empty.Play();
        }
    }
}
