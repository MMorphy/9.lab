using UnityEngine;
using System.Collections;
using System;

public class PlayerShooting : MonoBehaviour
{

    public int damagePerShot = 20;
    public float timeBetweenBullets = 0.15f;
    public float range = 100f;
    public GameObject rifle;

    float timer;
    Ray shootRay;
    RaycastHit shootHit;
    int shootableMask;
    ParticleSystem gunParticles;
    LineRenderer gunLine;
    Light gunLight;
    float effectsDisplayTime = 0.2f;

    public AudioSource audioSource;

    public void Awake()
    {
        // Create a layer mask for the Shootable layer.
        shootableMask = LayerMask.GetMask("Shootable");

        // Set up the references.
        gunParticles = GetComponent<ParticleSystem>();
        gunLine = GetComponent<LineRenderer>();
        gunLight = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveTarget();

        // Add the time since Update was last called to the timer.
        timer += Time.deltaTime;

        // If the Fire1 button is being press and it's time to fire...
        if (Input.GetButton("Fire1") && timer >= timeBetweenBullets)
        {
            // ... shoot the gun.

            Shoot();
            audioSource.Play();
        }

        // If the timer has exceeded the proportion of timeBetweenBullets that the effects should be displayed for...
        if (timer >= timeBetweenBullets * effectsDisplayTime)
        {
            // ... disable the effects.
            DisableEffects();
        }
    }

    private void MoveTarget()
    {
        RaycastHit hit; // declare the RaycastHit variable
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            // what ever lookat(hit.point);
            Quaternion targetRotation = Quaternion.LookRotation(hit.point);

            // Smoothly rotate towards the target point.
            rifle.transform.LookAt(new Vector3(hit.point.x, rifle.transform.position.y, hit.point.z));
        }

    }

    private void DisableEffects()
    {
        // Disable the line renderer and the light.
        gunLine.enabled = false;
        gunLight.enabled = false;
    }

    private void Shoot()
    {
        // Reset the timer.
        timer = 0f;

        // Enable the light.
        gunLight.enabled = true;

        // Stop the particles from playing if they were, then start the particles.
        gunParticles.Stop();
        gunParticles.Play();

        // Enable the line renderer and set it's first position to be the end of the gun.
        gunLine.enabled = true;
        gunLine.SetPosition(0, transform.position);

        // Set the shootRay so that it starts at the end of the gun and points forward from the barrel.
        shootRay.origin = transform.position;
        shootRay.direction = transform.forward;

        // Perform the raycast against gameobjects on the shootable layer and if it hits something...
        if (Physics.Raycast(shootRay, out shootHit, range, shootableMask))
        {
            // Set the second position of the line renderer to the point the raycast hit.
            gunLine.SetPosition(1, shootHit.point);
            Destroy(shootHit.transform.gameObject);
            PlayerHealth.instance.IncrementPoints();
        }
        // If the raycast didn't hit anything on the shootable layer...
        else
        {
            // ... set the second position of the line renderer to the fullest extent of the gun's range.
            gunLine.SetPosition(1, shootRay.origin + shootRay.direction * range);
        }
    }
}
