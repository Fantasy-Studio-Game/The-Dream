using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // rigibody prob
    private Rigidbody2D rb2d;

    //Timer for damage
    public float timeInvincible = 0.6f;
    bool isInvincible;
    float invincibleTimer;

    //health
    public int maxHealth = 20;
    int currentHealth;

    public int maxHearts = 5;
    int currentHearts;

    bool isAllowMoving = true;

    public int health
    {
        get { return currentHealth; }
        set { currentHealth = value; }
    }

    public int hearts
    {
        get { return currentHearts; }
        set { currentHearts = value; }
    }

    private float currentStamina;
    public float maxStamina = 100;
    public float staminaRate = 15;


    public float speed = 2.5f;

    //look direction
    Vector2 lookDirection = new Vector2(0, -1);
    //Animation
    private Animator animator;

    // checkpoint
    private Vector2 checkPoint;


    // shield
    public GameObject magicShield;
    private bool isAllowMagicShield = true;
    private int currentShields = 1;
    public float magicShieldActiveTime = 2.0f;
    public float magicShieldCooldownTime = 3.0f;

    // Attack
    private bool isAllowAttacking = true;
    public float attackingStamina = 20f;
    public float attackingCooldownTime = 1f;

    // shadowStrike
    private bool isAllowShadowStrike = true;
    public float shadowStrikeStamina = 30f;
    public float shadowStrikeCooldownTime = 1.5f;

    // Projectile
    public GameObject projectilePrefab;

    //Skill
    public GameObject shadowStrikePrefab;


    // teleport
    private bool isAllowTeleport = true;

    //input actions
    PlayerInputActions inputActions;
    Vector2 currentInput;

    // sound
    AudioSource audioSource;
    public AudioSource footStepSource;
    public AudioClip deathMusic;
    public BackgroundMusic bgMusic;

    // UI
    public GameObject gameOverMenu;
    

    // Start is called before the first frame update
    void Start()
    {
        checkPoint = transform.position;
        rb2d = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        currentHearts = maxHearts;
        currentStamina = 0;
        animator = GetComponent<Animator>();
        HeartSystem.instance.SetValue(currentHearts, maxHearts);
        audioSource = GetComponent<AudioSource>();

        inputActions = new PlayerInputActions();
        inputActions.Player.Enable();
        inputActions.Player.Movement.performed += OnMovement;
        inputActions.Player.Movement.canceled += OnMovement;
        inputActions.Player.Launch.performed += OnLaunch;
        inputActions.Player.Shield.performed += OnShield;
        inputActions.Player.Strike.performed += OnStrike;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentStamina < maxStamina)
        {
            currentStamina += staminaRate * Time.deltaTime;
            UIStaminaBar.instance.SetValue(currentStamina / (float)maxStamina);
        }
        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
            {
                isInvincible = false;
            }
        }
    }

    void FixedUpdate()
    {
        if (isAllowMoving)
        {
            if (currentInput.magnitude > 0.01)
            {
                Vector2 position = transform.position;
                position.x += (2f * currentInput.x * speed * Time.deltaTime);
                position.y += (2f * currentInput.y * speed * Time.deltaTime);
                rb2d.MovePosition(position);
            }

            Vector2 move = new Vector2(currentInput.x, currentInput.y);
            if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
            {
                if (!footStepSource.isPlaying)
                    footStepSource.Play();
                lookDirection.Set(move.x, move.y);
                lookDirection.Normalize();
            }
            else
            {
                footStepSource.Stop();
            }


            animator.SetFloat("Look X", lookDirection.x);
            animator.SetFloat("Look Y", lookDirection.y);
            animator.SetFloat("Speed", move.magnitude);
        }
    }

    public void AddHeart(int amount)
    {
        currentHearts += amount;
        HeartSystem.instance.SetValue(currentHearts, maxHearts);
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
                OnDeath();
            }
        }
        UIHealthBar.instance.SetValue(currentHealth / (float)maxHealth);


    }

    IEnumerator Shield()
    {
        animator.SetTrigger("Cast Spell");
        isAllowMagicShield = false;
        currentShields--;
        magicShield.SetActive(true);

        yield return new WaitForSeconds(magicShieldActiveTime);
        magicShield.SetActive(false);

        yield return new WaitForSeconds(magicShieldCooldownTime);
        isAllowMagicShield = true;
    }

    IEnumerator Launch()
    {
        currentStamina -= attackingStamina;
        UIStaminaBar.instance.SetValue(currentStamina / (float)maxStamina);

        animator.SetTrigger("Launch");
        isAllowAttacking = false;
        Quaternion rotation = Quaternion.Euler(0.0F, 0.0F, Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg);
        GameObject projectileObject = Instantiate(projectilePrefab, rb2d.position + Vector2.up * 0.5f, rotation);
        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.Launch(lookDirection, 300f);

        yield return new WaitForSeconds(attackingCooldownTime);
        isAllowAttacking = true;
    }

    IEnumerator Strike()
    {
        currentStamina -= shadowStrikeStamina;
        UIStaminaBar.instance.SetValue(currentStamina / (float)maxStamina);

        animator.SetTrigger("Launch");
        isAllowShadowStrike = false;
        Quaternion rotation = Quaternion.Euler(0.0F, 0.0F, Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg);
        GameObject strikeObject = Instantiate(shadowStrikePrefab, rb2d.position + Vector2.up * 0.4f, rotation);
        ShadowStrike strike = strikeObject.GetComponent<ShadowStrike>();
        strike.Launch(lookDirection, 200f);

        yield return new WaitForSeconds(shadowStrikeCooldownTime);
        isAllowShadowStrike = true;
    }

    public void SetCheckpoint(Vector2 position)
    {
        checkPoint = position;
    }

    public void Teleport(Vector2 position)
    {
        if (isAllowTeleport == true)
        {
            StartCoroutine(TeleportAction(position));
        }

    }

    IEnumerator TeleportAction(Vector2 position)
    {
        animator.SetTrigger("Cast Spell");
        isAllowTeleport = false;
        transform.position = position;
        yield return new WaitForSeconds(2f);
        isAllowTeleport = true;
    }

    // input actions
    void OnMovement(InputAction.CallbackContext context)
    {
        if (this == null)
        {
            return;
        }
        if (context.performed)
        {
            currentInput = context.ReadValue<Vector2>();

        }

        if (context.canceled)
        {
            currentInput = Vector2.zero;
        }
    }

    void OnLaunch(InputAction.CallbackContext context)
    {
        if (this == null)
        {
            return;
        }
        if (isAllowAttacking == true && currentStamina > attackingStamina)
        {
            StartCoroutine(Launch());
        }

    }

    void OnStrike(InputAction.CallbackContext context)
    {
        if (this == null)
        {
            return;
        }
        if (isAllowShadowStrike == true && currentStamina > shadowStrikeStamina)
        {
            StartCoroutine(Strike());
        }

    }

    void OnShield(InputAction.CallbackContext context)
    {
        if (this == null)
        {
            return;
        }
        if (isAllowMagicShield == true && currentShields > 0)
        {
            StartCoroutine(Shield());
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        rb2d.velocity = Vector2.zero;
        rb2d.angularVelocity = 0f;
    }

    public void AddShield(int num)
    {
        currentShields += num;
    }

    void OnDeath () {
        gameOverMenu.SetActive(true);
        bgMusic.ChangeBGMusic(deathMusic);
    }
}
