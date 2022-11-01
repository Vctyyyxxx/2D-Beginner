using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController2 : MonoBehaviour
{
    public float speed;
    public bool vertical;
    Rigidbody2D rigidbody2d;
    public float changeTime = 3.0f;
    float timer;
    int direction = 1;
    Animator animator;

    public ParticleSystem smokeEffect;


    bool broken = true;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        timer = changeTime;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!broken)
        {
            return;
        }
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            direction = -direction;
            timer = changeTime;
        }
    }
    private void FixedUpdate()
    {
        Vector2 position = rigidbody2d.position;
        if (vertical)
        {
            position.y = position.y + Time.deltaTime * speed*direction;
            animator.SetFloat("Move X",0);
            animator.SetFloat("Move Y",direction);
        }
        else {
            position.x = position.x + Time.deltaTime * speed*direction;
            animator.SetFloat("Move X", direction);
            animator.SetFloat("Move Y", 0);
        }
        rigidbody2d.MovePosition(position);
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        RubyController rubyController = other.gameObject.GetComponent<RubyController>();
        if (rubyController != null)
        {
            rubyController.ChangeHealth(-1);
        }
    }

    public void Fix()
    {
        broken = false;
        rigidbody2d.simulated = false;
        animator.SetTrigger("Fixed");
        //Destroy(smokeEffect);
        smokeEffect.Stop();
    }
}
