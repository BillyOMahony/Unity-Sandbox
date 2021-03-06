﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : Weapon{

    public Transform gunEnd;

    public override void Shoot()
    {
        nextFire = Time.time + fireRate;

        Vector3 rayOrigin = fpsCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));

        RaycastHit hit;

        gameObject.GetComponent<Animation>().Play();

        if (Physics.Raycast(rayOrigin, fpsCam.transform.forward, out hit, weaponRange))
        {
            hit.transform.gameObject.SendMessage("Damage", weaponDamage, SendMessageOptions.DontRequireReceiver);

            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * hitForce);
            }
        }
    }

    public override void MadeActive()
    {
        laserLine = GetComponent<LineRenderer>();
        weaponAudio = GetComponent<AudioSource>();
        fpsCam = GetComponentInParent<Camera>();
        PositionWeapon();
        GetComponent<Crosshair>().DisplayCrosshair();
    }

    void CheckForKeyPress()
    {
        if (Input.GetButtonDown("Fire1") && Time.time > nextFire && weaponActive)
        {
            Shoot();
        }
    }

    void Start()
    {
        if (weaponActive)
        {
            MadeActive();
        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckForKeyPress();
    }
}
