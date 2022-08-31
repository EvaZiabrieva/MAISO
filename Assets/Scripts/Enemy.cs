using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float maxHealth = 200f;
    [SerializeField] private float currentHealth;
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private FieldOfView fieldOfView;

    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float damage = 5;
    [SerializeField] private float timeToDamage = 5f;

    [SerializeField] private float radius = 5f;
    [SerializeField] private float shootDistance = 1f;

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private LayerMask playerRaycastMask = 1;
    [SerializeField] private Element element;

    public Element Element => element;


    private Rigidbody2D rb;
    private float timer = 0f;

    private void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        rb = GetComponent<Rigidbody2D>();
        fieldOfView.SetRadius(radius);
    }
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    private void Update()
    {
        Collider2D collider = Physics2D.OverlapCircle(transform.position, radius);
        Player player = collider.GetComponent<Player>();
        bool isPlayerVisible = false;

        if (player != null)
        {
            Vector2 direction = player.transform.position - transform.position;
            direction.Normalize();
            RaycastHit2D hit = Physics2D.Raycast((Vector2)transform.position + direction, direction, radius, playerRaycastMask);
            isPlayerVisible = hit.collider.GetComponent<Player>() != null;
            
            if (isPlayerVisible) 
            {
                rb.MovePosition((Vector2)transform.position + (direction * moveSpeed * Time.deltaTime));
                if (timer >= timeToDamage)
                {
                    GameObject bullet = Instantiate(bulletPrefab, transform.position + (Vector3)direction * shootDistance, Quaternion.identity);
                    bullet.transform.up = direction;
                    timer = 0f;
                }
                    
                timer += Time.deltaTime;
            }
        }

        fieldOfView.SetTriggerded(isPlayerVisible);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        Player player = collision.collider.GetComponent<Player>();

        if (player != null && timer >= timeToDamage)
        {
            player.TakeDamage(damage);
            timer = 0f;
        }
    }
}
