using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    [Header("Boss")]
    private int _bossAttackCycle = 1;
    private float _attackDelay;

    [Header("Enemy Properties")]
    [SerializeField] private Color _enemyInactiveColor;
    [SerializeField] private Color _enemyActiveColor;

    [Header("References")]
    [SerializeField] private Damageable _damageable;
    [SerializeField] private Damageable _playerDamageable;
    [SerializeField] private ProjectileController _projectileController;
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private GameObject _enemyPrefab;


    private void Start()
    {
        StartCoroutine(HandleBossAttackCycle());
    }

    private void Update()
    {
        if (_damageable.Health <= 0 || _playerDamageable.Health <= 0) GameManager.GameIsPlaying = false;
    }

    // Handle which attack cycle is happening
    // Only runs when boss is still alive
    private IEnumerator HandleBossAttackCycle()
    {
        yield return new WaitForSeconds(2f);
        while (GameManager.GameIsPlaying)
        {
            if (GameManager.GameIsPlaying)
            {
                yield return new WaitForSeconds(_attackDelay);
                DetermineAttackCycle();
                DetermineAttackDelay();

                _projectileController.SpawnProjectile(ProjectileType.Box, _bossAttackCycle);
                StartCoroutine(SpawnEnemy());
            }
        }
    }

    private void DetermineAttackCycle()
    {
        if (_damageable.Health > 300)
        {
            _bossAttackCycle = 1;
            return;
        }
        else if (_damageable.Health > 150)
        {
            _bossAttackCycle = 2;
            return;
        }
        else if (_damageable.Health > 0)
        {
            _bossAttackCycle = 3;
            return;    
        }
    }

    // Gets the attack speed of the boss, depending on the attack cycle
    private void DetermineAttackDelay()
    {
        if (_bossAttackCycle == 1)
            _attackDelay = 1.5f;
        else
            _attackDelay = 1f;
    }

    private IEnumerator SpawnEnemy()
    {
        int randNum = (int)Random.Range(0, 3);
        // Spawn ghost enemy
        GameObject enemyObj = Instantiate(_enemyPrefab, _spawnPoints[randNum].position, Quaternion.identity);
        SpriteRenderer spriteRenderer = enemyObj.GetComponent<SpriteRenderer>();
        spriteRenderer.color = _enemyInactiveColor;
        yield return new WaitForSeconds(_attackDelay);
        // Set enemy active
        enemyObj.GetComponent<CircleCollider2D>().enabled = true;
        enemyObj.GetComponent<EnemyMovement>().enabled = true;
        spriteRenderer.color = _enemyActiveColor;
    }

    // Determines which projectile attack to use
    private ProjectileType GetRandomProjectileType()
    {
        int randNum = (int)Random.Range(1, 3);
        if (randNum == 1)
            return ProjectileType.Box;
        else if (randNum == 2)
            return ProjectileType.Lazer;
        else
            return ProjectileType.Bullet;
    }
}
