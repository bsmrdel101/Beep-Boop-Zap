using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    [Header("Object Properties")]
    public int Health;


    public void DamageGameObject(int damage)
    {
        Health -= damage;
        if (Health <= 0) HandleGameObjectDestroyed();
    }

    // Destroys game object and plays any animations or particles
    private void HandleGameObjectDestroyed()
    {
        Destroy(this.gameObject);
    }
}
