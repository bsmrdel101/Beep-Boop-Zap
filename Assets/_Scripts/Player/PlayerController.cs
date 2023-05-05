using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Gun Properties")]
    [SerializeField] private float _bulletSpeed = 6f;
    [SerializeField] private float _atkSpeed = 0.4f;
    private bool _canFire = true;
    private float _offset = 0.8f;
    private Rect _screenRect = new Rect(0,0, Screen.width, Screen.height);

    [Header("SFX")]
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _zapSfx;

    [Header("References")]
    [SerializeField] private Transform _pivotPoint;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _gunBarrel;
    [SerializeField] private Camera _playerCamera;


    private void Start()
    {
        GameManager.GameIsPlaying = true;
    }

    private void Update()
    {
        if (!GameManager.GameIsPlaying) return;
        HandleGunRotation();
        HandleShootGun();
    }

    private void HandleGunRotation()
    {
        Vector3 difference = _playerCamera.ScreenToWorldPoint(Input.mousePosition) - _pivotPoint.transform.position;
        difference.Normalize();
        float rotation_z = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        _pivotPoint.transform.rotation = Quaternion.Euler(0f, 0f, rotation_z + _offset);
    }

    private void HandleShootGun()
    {
        if (Input.GetMouseButton(0) && _canFire)
        {
            _canFire = false;
            // Get the position of the mouse on the screen.
            Vector3 screenMousePos = Input.mousePosition;
            // Turn that screen position into the actual position in the world.
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(screenMousePos);  

            // Find out the direction between the player and the mouse pointer.
            Vector2 direction = mousePos - (Vector2)transform.position;
            // Normalize the direction and multiply by bullet speed.
            direction.Normalize();
            direction *= _bulletSpeed;


            GameObject bullet = Instantiate(_bulletPrefab, _gunBarrel.position, Quaternion.identity);
            
            // Find the BulletScript prefab on that spawned bullet, and set it's velocity component.
            bullet.GetComponent<Bullet>().Velocity = direction;

            _audioSource.PlayOneShot(_zapSfx);

            StartCoroutine(ReloadGun());
        }
    }

    private IEnumerator ReloadGun()
    {
        yield return new WaitForSeconds(_atkSpeed);
        _canFire = true;
    }
}
