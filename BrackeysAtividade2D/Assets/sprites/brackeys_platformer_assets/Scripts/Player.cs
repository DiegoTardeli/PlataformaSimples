using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float velocidade = 40;
    public float forcaDoPulo = 4;

    private bool noChao = false;
    private bool correndo = false;
    private bool rolando = false;

    private SpriteRenderer sprite;
    private Rigidbody2D rb;
    private Animator animator;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        correndo = false;
        rolando = false;

        // Movimento para esquerda
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += new Vector3(-velocidade * Time.deltaTime, 0, 0);
            sprite.flipX = true;
            correndo = true;
        }

        // Movimento para direita
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += new Vector3(velocidade * Time.deltaTime, 0, 0);
            sprite.flipX = false;
            correndo = true;
        }

        // Pulo
        if (Input.GetKeyDown(KeyCode.Space) && noChao)
        {
            rb.AddForce(new Vector2(0, forcaDoPulo), ForceMode2D.Impulse);
        }

        // Rolamento (exemplo com Shift)
        if (Input.GetKey(KeyCode.LeftShift))
        {
            rolando = true;
        }

        // Atualiza os parâmetros do Animator
        animator.SetBool("Correndo", correndo);
        animator.SetBool("Rolamento", rolando);
    }

    void OnCollisionEnter2D(Collision2D colisao)
    {
        if (colisao.gameObject.CompareTag("Chao"))
        {
            noChao = true;
        }
    }

    void OnCollisionExit2D(Collision2D colisao)
    {
        if (colisao.gameObject.CompareTag("Chao"))
        {
            noChao = false;
        }
    }
}