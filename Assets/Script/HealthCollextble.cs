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
        Debug.Log($"�͵�ǰ���巢����ײ����:{other},��ǰ�ǵ�{collideCount}����ײ");

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
                Debug.Log("�����Ѫ");
            }

            

        }
        else {
            Debug.LogError("rubyController���δ��ȡ��");
        
        }

        

    }
    // Start is called before the first frame update
    
    
}
