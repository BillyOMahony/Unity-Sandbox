using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : Health {

    void Start()
    {
        CurrentHealth = InitialHealth;
    }

    public override void Damage(float damage)
    {
        CurrentHealth -= damage;
        if (CurrentHealth <= 0 && Alive)
        {
            Alive = false;
            gameObject.GetComponent<Animation>().Play();
        }
    }
	
}
