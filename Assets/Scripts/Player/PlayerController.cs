using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // rigibody prob
    private Rigidbody2D rb2d;
    float horizontal;
    float vertical;

    //Timer for damage
    public float timeInvincible = 0.6f;
    bool isInvincible;
    float invincibleTimer;

    //health
    public int maxHealth = 5;
    int currentHealth;

    public int maxHearts = 3;
    int currentHearts;

    bool isAllowMoving = true;

    public int health
    {
        get { return currentHealth; }
        set { currentHealth = value; }
    }
    public float speed = 2.5f;

    //look direction
    Vector2 lookDirection = new Vector2(0, 0);
    //Animation
    private Animator animator;

    // checkpoint
    private Vector2 checkPoint;


    // shield
    public GameObject magicShield;
    private bool isAllowMagicShield = true;
    public float magicShieldActiveTime = 2.0f;
    public float magicShieldCooldownTime = 3.0f;

    // Attack
    private bool isAllowAttacking = true;
    public float attackingCooldownTime = 1f;

    // Projectile
    public GameObject projectilePrefab;

    // Start is called before the first frame update
    void Start()
    {
        checkPoint = transform.position;
        rb2d = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        currentHearts = maxHearts;
        animator = GetComponent<Animator>();
        HeartSystem.instance.SetValue(currentHearts, maxHearts);
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
            {
                isInvincible = false;
            }
        }

        Vector2 move = new Vector2(horizontal, vertical);
        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        }

        animator.SetFloat("Look X", lookDirection.x);
        animator.SetFloat("Look Y", lookDirection.y);
        animator.SetFloat("Speed", move.magnitude);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isAllowMagicShield == true)
            {
                StartCoroutine(CastSpell());
            }
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            if (isAllowAttacking == true)
            {
                StartCoroutine(Launch());
            }
        }
    }

    void FixedUpdate()
    {
        if (isAllowMoving)
        {
            Vector2 position = transform.position;
            position.x += (2f * horizontal * Time.deltaTime * speed);
            position.y += (2f * vertical * Time.deltaTime * speed);
            rb2d.MovePosition(position);
        }
    }

    //Change Health
    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            if (magicShield.active == true)
            {
                return;
            }
            if (isInvincible)
            {
                return;
            }
            animator.SetTrigger("Hurt");

            //playSound(hitSound);
            isInvincible = true;
            invincibleTimer = timeInvincible;
        }
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);

        if (currentHealth < 0.001)
        {
            currentHearts--;
            HeartSystem.instance.SetValue(currentHearts, maxHearts);
            if (currentHearts > 0)
            {
                transform.position = checkPoint;
                currentHealth = maxHealth;
            }
            else
            {
                this.GetComponent<Renderer>().sortingOrder = -1;
                animator.SetTrigger("Death");
                Debug.Log("Death!");
                rb2d.simulated = false;
            }
        }
        UIHealthBar.instance.SetValue(currentHealth / (float)maxHealth);


    }

    IEnumerator CastSpell()
    {
        animator.SetTrigger("Cast Spell");
        isAllowMagicShield = false;
        magicShield.SetActive(true);

        yield return new WaitForSeconds(magicShieldActiveTime);
        magicShield.SetActive(false);

        yield return new WaitForSeconds(magicShieldCooldownTime);
        isAllowMagicShield = true;
    }

    IEnumerator Launch()
    {
        animator.SetTrigger("Launch");
        isAllowAttacking = false;
        GameObject projectileObject = Instantiate(projectilePrefab, rb2d.position + Vector2.up * 0.5f, Quaternion.identity);
        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.Launch(lookDirection, 300f);
        
        yield return new WaitForSeconds(attackingCooldownTime);
        isAllowAttacking = true;
    }

    public void SetCheckpoint(Vector2 position)
    {
        checkPoint = position;
    }

}
