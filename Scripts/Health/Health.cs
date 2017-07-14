using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Health : MonoBehaviour {

    public float InitialHealth;
    public float CurrentHealth;
    public bool Alive;

    public abstract void Damage(float damage);

}
