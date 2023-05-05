using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    [Header("Boss")]
    private int _bossAttackCycle = 1;
    private float _attackDelay;

    [Header("References")]
    [SerializeField] private Damageable _damageable;
    [SerializeField] private ProjectileController _projectileController;


    private void Start()
    {
        StartCoroutine(HandleBossAttackCycle());
    }

    private void Update()
    {
        if (_damageable.Health <= 0) GameManager.GameIsPlaying = false;
    }

    // Handle which attack cycle is happening
    // Only runs when boss is still alive
    private IEnumerator HandleBossAttackCycle()
    {
        yield return new WaitForSeconds(2f);
        while (GameManager.GameIsPlaying)
        {
            yield return new WaitForSeconds(_attackDelay);
            DetermineAttackDelay();
            if (_bossAttackCycle == 1)
            {
                BossAttackCycleOne();
            }
        }
    }

    // Gets the attack speed of the boss, depending on the attack cycle
    private void DetermineAttackDelay()
    {
        if (_bossAttackCycle == 1)
            _attackDelay = 1.5f;
        else if (_bossAttackCycle == 2)
            _attackDelay = 1f;
        else if (_bossAttackCycle == 2)
            _attackDelay = 0.5f;
    }

    private void BossAttackCycleOne()
    {
        ProjectileType projectileType = GetRandomProjectileType();
        _projectileController.SpawnProjectile(projectileType, _bossAttackCycle);
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
