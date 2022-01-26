using Assets.Scripts.Enermy.Behavior;
using Assets.Scripts.Enermy.Behavior.ActionBehavior;
using System.Collections;
using UnityEngine;

public class SuperBoss : EnermyController
{
    public GameObject nextLevelCircle;
    public float castTimer;
    public float timeDamged;
    public float appearTimer;
    public float attackRange;
    public float rashinRange;
    public float deadtime;

    public Transform rashinPoint;
    public Transform attackPoint;
    public LayerMask playerProjectileMask;
    public LayerMask playerEnemyMask;

    public AudioClip attackSound;
    public AudioClip deadSound;
    public AudioClip appearSound;

    public GameObject backgroundAudio;

    private AudioSource audioSource;
    private int boss_shield;

    private void Awake()
    {
        attackMethod = new SlashWaveAttack();

        actionBehavior = new FollowDefenceActionBehavior(distanceView, rashinRange, attackRange, rashinPoint, playerProjectileMask, StartUp);

        boss_shield = shield;
        shield = 100;

        audioSource = GetComponent<AudioSource>();
    }

    protected override void Launch()
    {
        StartCoroutine(CastWaveAttack());
    }

    private Vector2 GetVector2Enermy()
    {
        Vector2 directionVec = (actionBehavior as FollowDefenceActionBehavior).TargetRigid2d.position - (_rigidbody2D.position - Vector2.down * 1f);
        directionVec.Normalize();

        _animator.SetFloat("HorizontalX", directionVec.x);
        _animator.SetFloat("HorizontalY", directionVec.y);

        return directionVec;
    }

    private IEnumerator CastWaveAttack()
    {
        if (actionBehavior.IsAwake() && !attackMethod.IsAttacking())
        {

            _speed = 0;
            _animator.SetTrigger("Attack"); // --> Attack

            yield return new WaitForSeconds(timeDamged);

            Vector2 directionVec = GetVector2Enermy();

            directionVec = (Vector2)attackPoint.position + directionVec * 0.5f;

            Collider2D[] detectedPlayers = Physics2D.OverlapCircleAll(directionVec, attackRange, playerEnemyMask);
            foreach (Collider2D c in detectedPlayers)
            {
                var player = c.GetComponent<PlayerController>();
                if (player != null)
                {
                    player.ChangeHealth(-atk);
                }
            }

            audioSource.PlayOneShot(attackSound);
            yield return new WaitForSeconds(castTimer - timeDamged);


            attackMethod.UnAttack(ref _speed, ref _animator);
            _speed = speed;

        }

        yield return null;
    }


    protected override void Moving(bool canMove)
    {
        Vector2 directionVec = GetVector2Enermy();

        Vector2 position = _rigidbody2D.position;

        position.x = position.x + Time.deltaTime * _speed * directionVec.x;
        position.y = position.y + Time.deltaTime * _speed * directionVec.y;

        _rigidbody2D.MovePosition(position);
    }


    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerController controller = collision.gameObject.GetComponent<PlayerController>();
        if (controller != null)
        {
            if (actionBehavior.IsAwake())
            {
                controller.ChangeHealth(-atk);
            }
        }
    }

    private void StartUp()
    {
        var backgroundMusic = backgroundAudio.GetComponent<AudioSource>();
        backgroundMusic.Stop();
        backgroundMusic.clip = appearSound;
        backgroundMusic.Play();

        StartCoroutine(ShieldTimer());
    }

    private IEnumerator ShieldTimer()
    {
        yield return new WaitForSeconds(appearTimer);
        appearTimer = 0;
        shield = boss_shield;
        _speed = speed;
        (actionBehavior as FollowDefenceActionBehavior).KeyAttack = true;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        OnCollisionEnter2D(collision);
    }

    protected override void DestroyEnermy()
    {
        _speed = 0;
        shield = 100;
        _animator.SetTrigger("Death");
        
        nextLevelCircle.SetActive(true);
        audioSource.PlayOneShot(deadSound);
        Destroy(gameObject, deadtime);
    }

    // draw circle on editer
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
