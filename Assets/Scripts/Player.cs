using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rigid = null;
    private const float JUMP_POWER = 7.0f;
    private bool isLanded = true;

    void Awake()
    {
        this.rigid = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (GameManager.instance.isGameStart == true)
        {
            if (isLanded == true && Input.GetButtonDown("Jump") == true)
            {
                this.rigid.AddForce(Vector2.up * JUMP_POWER, ForceMode2D.Impulse);
                isLanded = false;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (GameManager.instance.isGameStart == true)
        {
            this.isLanded = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
}
