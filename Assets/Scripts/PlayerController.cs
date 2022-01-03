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
    public float timeInvincible = 2.0f;
    bool isInvincible;
    float invincibleTimer;

    //health
    public int maxHealth = 5;
    int currentHealth;
    public int health
    {
        get { return currentHealth; }
        set { currentHealth = value; }
    }
    public float speed = 3.5f;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
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
    }

    void FixedUpdate()
    {
        Vector2 position = transform.position;
        position.x += (2f * horizontal * Time.deltaTime * speed);
        position.y += (2f * vertical * Time.deltaTime * speed);
        rb2d.MovePosition(position);
    }

    //Change Health
    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            //animator.SetTrigger("Hit");
            if (isInvincible)
            {
                return;
            }
            //playSound(hitSound);
            isInvincible = true;
            invincibleTimer = timeInvincible;
        }
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        Debug.Log(currentHealth + "/" + maxHealth);
        //UIHealthBar.instance.SetValue(currentHealth / (float)maxHealth);
    }
}
