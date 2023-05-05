using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Projectile Properties")]
    [SerializeField] private int _damage;

    [Header("References")]
    public GameObject SpawnParticles;


    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.tag == "Player")
        {
            collider2D.gameObject.GetComponent<Damageable>().DamageGameObject(_damage);
        }
    }
}
