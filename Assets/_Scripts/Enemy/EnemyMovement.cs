using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 4f;

    [Header("References")]
    private Transform _playerTransform;
    private Damageable _damageable;


    private void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        _playerTransform = playerObj.transform;
        _damageable = playerObj.GetComponent<Damageable>();
    }

    private void Update()
    {
        if (!GameManager.GameIsPlaying) return;
        MoveToPlayer();
    }

    private void MoveToPlayer()
    {
        Vector3 playerPos = _playerTransform.position;
        float speed = moveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, playerPos, speed);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player") _damageable.DamageGameObject(1);
    }
}
