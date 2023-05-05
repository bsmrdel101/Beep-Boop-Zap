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
    [SerializeField] private Color _inactiveColor;
    [SerializeField] private Color _activeColor;
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
            _projectileSpawnDelay = 1f;
        else
            _projectileSpawnDelay = 0.8f;

        // TEMP: For now it just spawns the box projectile
        StartCoroutine(SpawnBoxProjectile());
    }

    // Spawn box projectile
    private IEnumerator SpawnBoxProjectile()
    {
        // Spawn ghost projectile
        Vector3 playerPos = _playerObj.transform.position;
        GameObject projectile = Instantiate(_boxPrefab, playerPos, Quaternion.identity);
        SpriteRenderer spriteRenderer = projectile.GetComponent<SpriteRenderer>();
        Projectile projectileScript = projectile.GetComponent<Projectile>();
        spriteRenderer.color = _inactiveColor;
        yield return new WaitForSeconds(_projectileSpawnDelay);
        // Set projectile active
        projectile.GetComponent<BoxCollider2D>().enabled = true;
        spriteRenderer.color = _activeColor;
        projectileScript.SpawnParticles.SetActive(true);
        // Remove projectile
        yield return new WaitForSeconds(1f);
        Destroy(projectile);
    }

    // Spawn lazer projectile
    private IEnumerator SpawnLazerProjectile()
    {
        // Spawn ghost projectile
        Vector3 playerPos = _playerObj.transform.position;
        GameObject projectile = Instantiate(_lazerPrefab, playerPos, Quaternion.identity);
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
        GameObject projectile = Instantiate(_bulletPrefab, playerPos, Quaternion.identity);
        yield return new WaitForSeconds(_projectileSpawnDelay);
        // Set projectile active
        projectile.GetComponent<CircleCollider2D>().enabled = true;
        // Remove projectile
        yield return new WaitForSeconds(1f);
        Destroy(projectile);
    }
}
