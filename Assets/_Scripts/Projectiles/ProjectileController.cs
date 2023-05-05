using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ProjectileType
{
    Box,
    Lazer,
    Bullet
}

public class ProjectileController : MonoBehaviour
{
    [Header("Projectiles")]
    [SerializeField] private GameObject _boxPrefab;
    [SerializeField] private GameObject _lazerPrefab;
    [SerializeField] private GameObject _bulletPrefab;
    private float _projectileSpawnDelay;

    [Header("References")]
    [SerializeField] private GameObject _playerObj;


    // Handles spawning projectiles
    public void SpawnProjectile(ProjectileType projectileType, int bossAttackCycle)
    {
        // Handles speed of projectiles appearing
        if (bossAttackCycle == 1)
            _projectileSpawnDelay = 1.5f;
        else if (bossAttackCycle == 2)
            _projectileSpawnDelay = 2f;

        // Determines the projectile code that will run
        if (projectileType == ProjectileType.Box)
            StartCoroutine(SpawnBoxProjectile());
        else if (projectileType == ProjectileType.Lazer)
            StartCoroutine(SpawnLazerProjectile());
        else
            StartCoroutine(SpawnBulletProjectile());
    }

    // Spawn box projectile
    private IEnumerator SpawnBoxProjectile()
    {
        // Spawn ghost projectile
        Vector3 playerPos = _playerObj.transform.position;
        GameObject projectile = Instantiate(_boxPrefab, playerPos, Quaternion.identity);
        yield return new WaitForSeconds(_projectileSpawnDelay);
        // Set projectile active
        projectile.GetComponent<BoxCollider2D>().enabled = true;
        // Remove projectile
        yield return new WaitForSeconds(1f);
        Destroy(projectile);
    }

    // Spawn lazer projectile
    private IEnumerator SpawnLazerProjectile()
    {
        // Spawn ghost projectile
        Vector3 playerPos = _playerObj.transform.position;
        GameObject projectile = Instantiate(_boxPrefab, playerPos, Quaternion.identity);
        yield return new WaitForSeconds(_projectileSpawnDelay);
        // Set projectile active
        projectile.GetComponent<BoxCollider2D>().enabled = true;
        // Remove projectile
        yield return new WaitForSeconds(1f);
        Destroy(projectile);
    }

    // Spawn bullet projectile
    private IEnumerator SpawnBulletProjectile()
    {
        // Spawn ghost projectile
        Vector3 playerPos = _playerObj.transform.position;
        GameObject projectile = Instantiate(_boxPrefab, playerPos, Quaternion.identity);
        yield return new WaitForSeconds(_projectileSpawnDelay);
        // Set projectile active
        projectile.GetComponent<CircleCollider2D>().enabled = true;
        // Remove projectile
        yield return new WaitForSeconds(1f);
        Destroy(projectile);
    }
}
