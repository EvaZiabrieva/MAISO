using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class Enemy : MonoBehaviour
{
    public float health = 100f;
    public Transform player;
    public float moveSpeed = 10f;
    public float damage = 5f;
    public float timeToDamage = 5f;

    private Rigidbody2D rb;
    private float timer = 0f;

    public void TakeDamage(float damage)
    {
        health -= damage;

        if(health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    private void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (player != null)
        {
            Vector2 direction = player.position - transform.position;
            direction.Normalize();
            rb.MovePosition((Vector2)transform.position + (direction * moveSpeed * Time.deltaTime));
            timer += Time.deltaTime;
        }
    }

    private void OnTriggerStay2D(Collider2D hitInfo)
    {
        Player player = hitInfo.GetComponent<Player>();

        if (player != null && timer >= timeToDamage)
        {
            player.TakeDamage(damage);
            timer = 0f;
        }
    }
}
