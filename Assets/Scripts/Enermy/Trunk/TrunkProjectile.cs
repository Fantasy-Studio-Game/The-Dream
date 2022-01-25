
using UnityEngine;

public class TrunkProjectile : MonoBehaviour
{
    public float rangeBullet = 5.0f;
    
    private Rigidbody2D _rigidbody2D;
    private Vector2 _originPosition;
    private Animator _animator;

    private int _atk = 10;
    private float _time_destroy = 1f;
    private bool _destroyed = false;    

    //public ParticleSystem hitParticleSystem;

    // Awake is called before the first frame update
    void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _originPosition = _rigidbody2D.position;

    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(_rigidbody2D.position, _originPosition) > rangeBullet)
        {
            _animator.SetTrigger("Destroy");
            _destroyed = true;
        }

        if (_destroyed)
        {
            _time_destroy -= Time.deltaTime;
        }

        if (_time_destroy < 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //ParticleSystem particle = Instantiate(hitParticleSystem, _rigidbody2D.position, Quaternion.identity);
        //particle.Play();
        Debug.Log(collision.gameObject.name);
        PlayerController player = collision.gameObject.GetComponentInParent<PlayerController>();

        if (player != null)
        {
            player.ChangeHealth(-_atk);
        }

        _animator.SetTrigger("Destroy");
        _destroyed = true;
    }

    public void Launch(Vector2 direction, float force, int atk)
    {
        _rigidbody2D.AddForce(direction * force);
        _atk = atk;
    }
}