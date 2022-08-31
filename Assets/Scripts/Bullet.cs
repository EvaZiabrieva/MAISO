using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float damage = 20;
    [SerializeField] private Element element;

    private void Start()
    {
        rb.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Enemy enemy = hitInfo.GetComponent<Enemy>();
        if(enemy != null)
        {
            enemy.TakeDamage(damage * DamageCalculation.GetDamageScale(element, enemy.Element));
            Debug.Log(DamageCalculation.GetDamageScale(element, enemy.Element) + " " + element + " " + enemy.Element);
        }
        Destroy(gameObject);
    }
}
