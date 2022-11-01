using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 0.1f;
    Rigidbody2D rigidbody2d;
    Vector2 position;
    float initY;
    float direction;
    public float distance = 4;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        position = transform.position;
        initY = position.y;
        direction = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        MovePosition();
    }

    private void MovePosition()
    {
        if (position.y - initY < distance && direction > 0)
        {
            position.y += speed;
        }
        if (position.y - initY >= distance && direction > 0)
        {
            direction = -1.0f;
        }
        if (position.y - initY > 0 && direction < 0)
        {
            position.y -= speed;
        }
        if (position.y - initY <= 0 && direction < 0)
        {
            direction = 1.0f;
        }
        rigidbody2d.position = position;
    }

}
