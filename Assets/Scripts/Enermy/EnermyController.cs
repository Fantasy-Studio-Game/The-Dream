using Assets.Scripts.Enermy.Action;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Note: this class is a element in Strategy Pattern,
// so, to use it, We must use other class inherit this class 
public class EnermyController : MonoBehaviour
{
    public GameObject projectilePrefab;

    public bool isCanFire = false;
    public bool isTowardLeft = true;
    public float speed = 1.0f;
    public float distanceMove = 5.0f;
    public float distanceView = 3.0f;
    public float timerFireCast = 2.0f;
    public float maxHealth = 100;
    public int shield = 10;
    public int atk = 10;

    //---------------------------------------------

    protected IAttack attackMethod;

    //---------------------------------------------
    private Rigidbody2D _rigidbody2D;
    private Animator _animator;

    private int _direction;
    private float _distanceMove;
    private float _speed;
    private float _timerFireCast;
    private bool _isAwake = false;
    private float _health;
    private bool _isShoot = false;



    // Trigger function
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();

        _distanceMove = distanceMove / 2;
        _health = maxHealth;

        // stop to idle
        _speed = 0;
        _timerFireCast = 0;

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
        // if enermy is casting fire
        if (_timerFireCast > 0)
        {
            _timerFireCast -= Time.deltaTime;

            if  (!_isShoot && (timerFireCast - _timerFireCast > 0.6))
            {
                Launch();
                _isShoot = true;
            }

            return;
        }
        if (_health <= 0)
        {
            Destroy(gameObject);
        }

        RaycastHit2D detectedPayer = Physics2D.Raycast(_rigidbody2D.position + Vector2.up * 0.1f, new Vector2(_direction, 0), distanceView, LayerMask.GetMask("Player"));
        if (detectedPayer.collider != null)
        {            
            if (_isAwake)
            {
                attackMethod.Attack(ref _timerFireCast, ref _speed, ref _isShoot, ref _animator);
            }
            else
            {
                // start enermy only once
                _animator.SetTrigger("Awake"); // --> Move

                // run run
                _isAwake = true;
                _speed = speed;
            }
        }
        else
        {
            Moving();
        }

        
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
    void Launch()
    {
        int anglesRotate = 0;

        if (_direction == -1)
        {
            anglesRotate = 180;
        }

        GameObject projectileObject = Instantiate(projectilePrefab, _rigidbody2D.position + Vector2.up * 0.23f, Quaternion.Euler(0, 0, anglesRotate));

        TrunkProjectile projectile = projectileObject.GetComponent<TrunkProjectile>();
        projectile.Launch(new Vector2(_direction, 0), 100, atk);

    }

    void Moving()
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

        if(_isAwake && _speed == 0)
        {
            _speed = speed;
        }
    }


    protected void Attack()
    {
        attackMethod.Attack(ref _timerFireCast, ref _speed, ref _isShoot, ref _animator);
    }

    /*
     * if (isCanFire)
        {
            _timerFireCast = timerFireCast;
            _speed = 0;
            _isShoot = false;
        }
        else
        {
            _speed = speed * 2;
        }

        _animator.SetBool("Attack", true); // --> Attack
     */


    // public process event method
    public void GetDamage(int damage)
    {
        _health -= damage * (100 - shield) / 100;

        _animator.SetTrigger("Hit");
    }

}
