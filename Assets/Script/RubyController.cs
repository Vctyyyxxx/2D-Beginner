using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyController : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float timeInvincible = 2.0f;
    bool isInvincible;
    public int maxHealth = 5;
    float invincibleTimer;


    public int health { get { return currentHealth; } }
    internal int currentHealth;

    Rigidbody2D rigidbody2d;
    float horizontal;
    float vertical;
    // ���ٶȱ�¶������ʹ��ɵ�
    public float speed = 0.1f;

    Animator animator;
    Vector2 lookDirection = new Vector2(1,0);
    Vector2 move;

    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();

        currentHealth = maxHealth;

        animator = GetComponent<Animator>();

    }
 


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
        move = new Vector2(horizontal, vertical);
        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        }

        animator.SetFloat("Look X", lookDirection.x);
        animator.SetFloat("Look Y", lookDirection.y);
        animator.SetFloat("Speed", move.magnitude);

        if (Input.GetKeyDown(KeyCode.C))//||Input.GetAxis("Fire1")!=0
        {
            Launch();
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            RaycastHit2D hit = Physics2D.Raycast(rigidbody2d.position + Vector2.up*0.2f,lookDirection,1.5f,LayerMask.GetMask("NPC"));
            if (hit.collider != null)
            {
                Debug.Log($"������ײ���Ķ����ǣ�{hit.collider.gameObject}");
                NonPlayerCharacter npc = hit.collider.GetComponent<NonPlayerCharacter>();
                if (npc != null) 
                {
                    npc.DisplayDialog();
                }
            }
        }
        
    }



    private void FixedUpdate()
    {
        Vector2 position = transform.position;
        position.x = position.x + speed * horizontal * Time.deltaTime;
        position.y = position.y + speed * vertical * Time.deltaTime;
        rigidbody2d.position = position;
     
    }

   

    public void ChangeHealth(int amount) {

        if (amount < 0)
        {
            if (isInvincible)
            {
                return;
            }

            isInvincible = true;

            invincibleTimer = timeInvincible;

            animator.SetTrigger("Hit");
        }

        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        UIHealthBra.Instance.SetValue(currentHealth/(float)maxHealth);
        Debug.Log("��ǰ����ֵ��" + currentHealth + "/" + maxHealth);
    }
 
    void Launch()
    {
        GameObject projectileObject = Instantiate(projectilePrefab, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);
        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.Launch(lookDirection,300);
        animator.SetTrigger("Launch");
    }
}
