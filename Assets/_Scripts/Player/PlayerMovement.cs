using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float _moveSpeed = 20.0f;
    private float _horizontal, _vertical;
    private Vector3 _playerInput;

    [Header("References")]
    [SerializeField] private Rigidbody2D rb;


    private void FixedUpdate ()
    {
      if (!GameManager.GameIsPlaying) return;

      GetInputs();
      MovePlayer();
    }

    private void GetInputs()
    {
      // Get the player's horizontal and vertical inputs for this frame
      _horizontal = Input.GetAxisRaw("Horizontal");
      _vertical = Input.GetAxisRaw("Vertical"); 

      //Store user input as a movement vector
      _playerInput = new Vector3(_horizontal, _vertical, 0);
    }

    private void MovePlayer()
    {
      // Adds the horizontal and vertical input to the current player position
      rb.MovePosition(transform.position + _playerInput * Time.deltaTime * _moveSpeed);
    }
}
