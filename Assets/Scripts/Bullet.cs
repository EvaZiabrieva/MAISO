using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float damage = 20f;
    [SerializeField] private Element element;

    private void Start()
    {
        rb.velocity = transform.up * speed;
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Player player = hitInfo.GetComponent<Player>();
        Enemy enemy = hitInfo.GetComponent<Enemy>();
        if (player != null)
        {
            player.TakeDamage(damage * DamageCalculation.GetDamageScale(element, player.Element));
        }
        else if(enemy != null)
        {
            enemy.TakeDamage(damage * DamageCalculation.GetDamageScale(element, enemy.Element));
        }
        Destroy(gameObject);
    }
}
