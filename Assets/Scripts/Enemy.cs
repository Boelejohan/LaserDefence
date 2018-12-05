using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health = 100f;

    
    void OnTriggerEnter2D(Collider2D col)
    {
        Projectile missile = col.GetComponent<Projectile> ();
        if (missile)
        {
            health -= missile.GetDamage();
            if (health <= 0)
            {
                Die ();
            }
            missile.Hit ();
        }
    }
    void Die()
    {
        Destroy(gameObject);
    }
}
