using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rigid = null;
    private const float JUMP_POWER = 7.0f;
    private bool isLanded = true;
    private AudioSource audio = null;

    void Awake()
    {
        this.rigid = GetComponent<Rigidbody2D>();
        this.audio = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (GameManager.instance.isGameOver == true)
        {
            this.rigid.velocity = Vector2.zero;
            this.rigid.gravityScale = 0f;
            return;
        }

        if (GameManager.instance.isGameStart == true)
        {
            this.rigid.gravityScale = 1.3f;

            if (isLanded == true && Input.GetButtonDown("Jump") == true)
            {
                this.rigid.AddForce(Vector2.up * JUMP_POWER, ForceMode2D.Impulse);
                this.audio.Play();
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
        GameManager.instance.isGameOver = true;
    }
}
