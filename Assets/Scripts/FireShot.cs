using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static GrabShells;
using static GrabShells2;

public class FireShot : MonoBehaviour
{
    public Animator weaponAnimator;
    public GameObject Shotgun;
    public GameObject MuzzleFlash;
    public AudioSource ShotgunShot;
    public bool isFiring = false;
    public TextMeshProUGUI balas;
    [HideInInspector]
    private float disparos = 0;
    public GrabShells grabShells;
    public GrabShells2 grabShells2;
    public AudioSource empty;
    public float TargetDistance;
    public int DamageAmount = 10;
    public float range = 100f;

    public Camera fpscam;

    private void Start()
    {
        balas.text = 0.ToString();
    }

    void Update()
    {

        if (grabShells.Taken == true)
        {
            disparos = disparos + 2;
            grabShells.Taken = false;
            balas.text = disparos.ToString();
        }

        if (grabShells2.Taken2 == true)
        {
            disparos = disparos + 2;
            grabShells2.Taken2 = false;
            balas.text = disparos.ToString();
        }

        if (Input.GetButtonDown("Fire1"))
        {
            if (isFiring == false)
            {
                StartCoroutine(FiringShotgun());
            }
        }

        if (isFiring == false)
        {
            MuzzleFlash.SetActive(false);
        }
    }
    
    IEnumerator FiringShotgun()
    {
        if (disparos >= 1 && grabShells.isLoaded == true)
        {
            isFiring = true;
            Shoot();
            Shotgun.GetComponent<Animation>().Play("Shot");
            MuzzleFlash.SetActive(true);
            MuzzleFlash.GetComponent<Animation>().Play("MuzzleAnim");
            disparos--;
            balas.text = disparos.ToString();
            ShotgunShot.Play();

            yield return new WaitForSeconds(0.8f);
            isFiring = false;
        }

        else if (disparos >= 1 && grabShells2.isLoaded2 == true)
        {
            isFiring = true;
            Shoot();
            Shotgun.GetComponent<Animation>().Play("Shot");
            MuzzleFlash.SetActive(true);
            MuzzleFlash.GetComponent<Animation>().Play("MuzzleAnim");
            disparos--;
            balas.text = disparos.ToString();
            ShotgunShot.Play();

            yield return new WaitForSeconds(0.8f);
            isFiring = false;
        }

        else
        {
            empty.Play();
        }
    }

    void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(fpscam.transform.position, fpscam.transform.forward, out hit, range)) 
        {
            Debug.Log(hit.transform.name);

            EnemyScript enemy = hit.transform.GetComponent<EnemyScript>();

            if (enemy != null)
            {
                enemy.TakeDamage(DamageAmount);
            }

        }
    }
}
