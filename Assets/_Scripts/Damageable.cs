using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Damageable : MonoBehaviour
{
    [Header("Object Properties")]
    public int Health;

    [Header("References")]
    private GameObject _playerObj;
    private GameObject _bossObj;
    [SerializeField] private GameObject _winScreen;


    private void Start()
    {
        _playerObj = GameObject.FindGameObjectWithTag("Player");
        _bossObj = GameObject.FindGameObjectWithTag("Boss");
    }

    public void DamageGameObject(int damage)
    {
        Health -= damage;
        if (_playerObj.GetComponent<Damageable>().Health <= 0)
        { 
            SceneManager.LoadScene("Game");
            return;
        }
        if (_bossObj.GetComponent<Damageable>().Health <= 0)
        {
            GameManager.GameIsPlaying = false;
            foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                Destroy(enemy);
            }
            _winScreen.SetActive(true);
        }
        if (Health <= 0) HandleGameObjectDestroyed();
    }

    // Destroys game object and plays any animations or particles
    private void HandleGameObjectDestroyed()
    {
        Destroy(this.gameObject);
    }
}
