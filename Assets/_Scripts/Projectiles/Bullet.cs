using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bullet : MonoBehaviour
{
    [Header("Bullet")]
    [HideInInspector] public Vector2 Velocity;
    [SerializeField] private int _damage = 1;

    [Header("Bossbar")]
    private Slider _healthBar;


    private void Start()
    {
        _healthBar = GameObject.Find("HealthBar").GetComponent<Slider>();
    }

    private void Update()
    {
        // Move the bullet in the direction of our velocity
        transform.position += new Vector3(Velocity.x, Velocity.y) * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" || other.tag == "Bullet") return;

        Damageable damageable = other.GetComponent<Damageable>();
        if (damageable) damageable.DamageGameObject(_damage);
        if (other.tag == "Boss") _healthBar.value -= _damage;
        Destroy(this.gameObject);
    }
}
