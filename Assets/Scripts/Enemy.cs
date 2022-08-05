using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health = 100f;
    public Transform player;
    public float moveSpeed = 10f;
    private Rigidbody2D rb;

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
        Vector2 direction = player.position - transform.position;
        direction.Normalize();
        rb.MovePosition((Vector2)transform.position + (direction * moveSpeed * Time.deltaTime));
    }
}
