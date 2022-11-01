using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollextble : MonoBehaviour
{
    public int amount = 1;
    int collideCount;
    public ParticleSystem cureEffect;
    private void OnTriggerEnter2D(Collider2D other)
    {
        collideCount = collideCount + 1;
        Debug.Log($"和当前物体发生碰撞的是:{other},当前是第{collideCount}次碰撞");

        RubyController rubyController = other.GetComponent<RubyController>();
        if (rubyController != null)
        {
            if (rubyController.health < rubyController.maxHealth)
            {
                rubyController.ChangeHealth(amount);
                Destroy(gameObject);
                Instantiate(cureEffect,transform.position,Quaternion.identity);

            }
            else
            {
                Debug.Log("无需加血");
            }

            

        }
        else {
            Debug.LogError("rubyController组件未获取到");
        
        }

        

    }
    // Start is called before the first frame update
    
    
}
