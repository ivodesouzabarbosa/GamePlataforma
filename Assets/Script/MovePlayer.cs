using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovePlayer : MonoBehaviour
{
    [SerializeField] float _speed;
    [SerializeField] float _forceJump;

    Rigidbody2D _rig;

    [SerializeField] Vector3 _move;

    bool _facingRight;

    bool _groundCheck;

    // Start is called before the first frame update
    void Start()
    {
        _rig = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        _rig.velocity = new Vector2(_move.x * _speed, _rig.velocity.y);

        if(_move.x > 0 && _facingRight == true)
        {
            flip();
        }
        else if (_move.x < 0 && _facingRight == false)
        {
            flip();
        }
    }

    public void SetMove(InputAction.CallbackContext value)
    {
        _move = value.ReadValue<Vector3>();
    }

    public void SetJump(InputAction.CallbackContext value)
    {
        if (_groundCheck)
        {
            _rig.velocity = new Vector2(_rig.velocity.x, 0);
            _rig.AddForce(transform.up * _forceJump, ForceMode2D.Impulse);
        }
    }


    void flip()
    {
        _facingRight = !_facingRight;
        float x = transform.localScale.x;
        x*= -1;

        transform.localScale = new Vector2(x, transform.localScale.y);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _groundCheck = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _groundCheck = false;
        }
    }
}
