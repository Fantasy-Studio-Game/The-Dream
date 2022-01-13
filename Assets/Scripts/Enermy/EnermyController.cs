using Assets.Scripts.Enermy.Behavior;
using Assets.Scripts.Enermy.Behavior.ActionBehavior;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Note: this class is a element in Strategy Pattern,
// so, to use it, We must use other class inherit this class 
public class EnermyController : MonoBehaviour
{

    public bool isTowardLeft = true;
    public float speed = 1.0f;
    public float distanceMove = 5.0f;
    public float distanceView = 3.0f;
    public float timerAttackCast = 2.0f;
    public float maxHealth = 100;
    public int shield = 10;
    public int atk = 10;

    //---------------------------------------------

    protected IAttack attackMethod;
    protected IActionBehavior actionBehavior;

    protected Animator _animator;
    protected Rigidbody2D _rigidbody2D;

    protected int _direction;

    //---------------------------------------------

    private float _distanceMove;
    private float _speed;
    private float _timerAttackCast;
    private float _health;



    // Trigger function
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();

        _distanceMove = distanceMove / 2;
        _health = maxHealth;

        // stop to idle
        _speed = 0;
        _timerAttackCast = 0;

        if (isTowardLeft)
        {
            _direction = -1;
        }
        else
        {
            _direction = 1;
        }
        _animator.SetFloat("Horizontal", _direction);
    }


    void FixedUpdate()
    {

        if (attackMethod.IsAttacking(ref _timerAttackCast, ref _speed, Launch))
        {
            return;
        }

        if (_health <= 0)
        {
            Destroy(gameObject);
        }

        actionBehavior.BehaveInContext(_direction, ref _speed, _rigidbody2D, ref _animator, Attack, Moving);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerController player = collision.gameObject.GetComponent<PlayerController>();

        if (player != null)
        {
            player.ChangeHealth(-atk);
        }
    }

    // general function
    protected virtual void Launch()
    {
        // subclass does
    }

    void Moving(bool canMove)
    {        
        if (canMove)
        {
            _distanceMove -= Time.deltaTime * _speed;

            if (_distanceMove < 0)
            {
                _direction = -_direction;
                _distanceMove = distanceMove;
            }

            Vector2 position = _rigidbody2D.position;
            position.x = position.x + Time.deltaTime * _speed * _direction;

            _animator.SetFloat("Horizontal", _direction);

            _rigidbody2D.MovePosition(position);

            if (_speed == 0)
            {
                _speed = speed;
            }
            
        }

    }


    protected void Attack()
    {
        attackMethod.Attack(ref _timerAttackCast, ref _speed, ref _animator);
    }



    // public process event method
    public void GetDamage(int damage)
    {
        _health -= damage * (100 - shield) / 100;

        _animator.SetTrigger("Hit");
    }

}
