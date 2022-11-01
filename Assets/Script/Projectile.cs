using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Rigidbody2D rigidbody2d;
    public ParticleSystem hitEffect;
    // Start is called before the first frame update
    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        EnemyController2 enemyController2 = collision.collider.GetComponent<EnemyController2>();
        if (enemyController2 != null)
        {
            enemyController2.Fix();
        }
        Debug.Log($"³ÝÂÖ×Óµ¯Åö×²µ½ÁË:{collision.gameObject}");
        Destroy(gameObject);
        Instantiate(hitEffect,transform.position,Quaternion.identity);
    }

    public void Launch(Vector2 direction,float force)
    {
        rigidbody2d.AddForce(direction*force);
    }
    // Update is called once per frame
    void Update()
    {
        if (transform.position.magnitude > 100.0f)
        {
            Destroy(gameObject);
        }
    }
}
