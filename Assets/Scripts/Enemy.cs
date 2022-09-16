using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class Enemy : Character
{
    [SerializeField] private FieldOfView fieldOfView;

    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float damage = 5;
    [SerializeField] private float timeToDamage = 5f;

    [SerializeField] private float radius = 5f;
    [SerializeField] private float shootDistance = 1f;

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private LayerMask playerRaycastMask = 1;


    private Rigidbody2D rb;
    private float timer = 0f;

    protected override void Start()
    {
        base.Start();

        rb = GetComponent<Rigidbody2D>();
        fieldOfView.SetRadius(radius);
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
        Character character = collision.collider.GetComponent<Character>();

        if (character != null && timer >= timeToDamage)
        {
            character.TakeDamage(damage);
            timer = 0f;
        }
    }
}
