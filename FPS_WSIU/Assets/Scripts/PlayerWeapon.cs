using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{

    [SerializeField]
    private int bulletsInMag = 30;
    [SerializeField]
    private int weaponDmg = 1;
    [SerializeField]
    private float weaponRange = 50f;
    [SerializeField]
    private float hitForce = 100f;
    [SerializeField]
    private float fireRate = 0.9f;

    private AudioSource weaponAudio;
    public AudioClip weaponShot;
    public AudioClip weaponReload;

    public Transform gunEnd;

    private Camera fpsCam;
    private WaitForSeconds shotDuration = new WaitForSeconds(.07f);
    private WaitForSeconds reloadDuration = new WaitForSeconds(3);

    private LineRenderer laserLine;
    private float nextFire;

    public Text uiText;

     void Start()
    {
        laserLine = GetComponent<LineRenderer>();
        weaponAudio = GetComponent<AudioSource>();
        fpsCam = GetComponentInParent<Camera>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && Time.time > nextFire && bulletsInMag > 0)
        {
            nextFire = Time.time + fireRate;

            StartCoroutine(ShotEffect());

            Vector3 rayOrigin = fpsCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;

            laserLine.SetPosition(0, gunEnd.position);

            bulletsInMag -= 1;
            Debug.Log("bulletsInMag: " + bulletsInMag);
            if (Physics.Raycast (rayOrigin, fpsCam.transform.forward, out hit, weaponRange ))
            {
                laserLine.SetPosition(1, hit.point);

                EnemyController healthPoints = hit.collider.GetComponent<EnemyController>();

                if (healthPoints != null)
                {
                    healthPoints.Damage(weaponDmg);
                }

            }
            else
            {
                laserLine.SetPosition(1, rayOrigin + (fpsCam.transform.forward * weaponRange));
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("Reload");
            bulletsInMag = 30;
            Debug.Log("bulletsInMag: " + bulletsInMag);
            StartCoroutine(ReloadEffect());
        }

        uiText.text = "Bullets: " + bulletsInMag.ToString() + "/30";
    }

    private IEnumerator ShotEffect()
    {
        weaponAudio.clip = weaponShot;
        weaponAudio.Play();

        laserLine.enabled = true;
        yield return shotDuration;
        laserLine.enabled = false;
    }

    private IEnumerator ReloadEffect()
    {
        weaponAudio.clip = weaponReload;
        weaponAudio.Play();
        yield return reloadDuration;
    }
}
