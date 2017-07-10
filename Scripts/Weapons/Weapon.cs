using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Weapon : MonoBehaviour{

    public bool weaponActive;

    public float weaponDamage;
    public float fireRate;
    public float weaponRange;
    public float hitForce;

    protected Camera fpsCam;
    protected AudioSource weaponAudio;
    protected LineRenderer laserLine;
    protected float nextFire;

    public abstract void Shoot();
}
