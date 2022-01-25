using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapsController : MonoBehaviour
{

    public int damage = 5;
    public Sprite unHarmedSprite;
    public GameObject projectilePrefab;
    public float timeCastProjectTile = 2f;

    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();

        if (projectilePrefab != null)
        {
            StartCoroutine(Launch());
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (_spriteRenderer.sprite.name == unHarmedSprite.name) // "peaks_3"
        {
            // this state of trap do not take damage
            return;
        }

        PlayerController controller = other.GetComponent<PlayerController>();

        if (controller != null)
        {
            controller.ChangeHealth(-damage);
        }
    }

    private IEnumerator Launch()
    {
        while (true)
        {
            //Quaternion rotation = Quaternion.Euler(0.0F, 0.0F, Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg);
            //GameObject projectileObject = Instantiate(projectilePrefab, (Vector2)transform.position + Vector2.up * 0.5f, Quaternion.identity);
            //TrapProjectTile projectile = projectileObject.GetComponent<TrapProjectTile>();
            //projectile.Launch(Vector2.down, 100f, damage);

            // .........
            yield return new WaitForSeconds(timeCastProjectTile);
        }
    }
}
