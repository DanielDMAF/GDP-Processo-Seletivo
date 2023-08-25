using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class playerMovimento : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] LayerMask chaoLayer;
    [SerializeField] Transform chaoCheck;

    private bool facingRight;
    public float vel_Mov;
    private float mov_hor;

    public float forca_pulo; //pulo
    private float coyoteDelay = 0.2f; //Coyote time pulo
    private float coyoteTimer;

    private float mov_vert;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Parte flip sprite:
        if ((facingRight && mov_hor < 0) || (!facingRight && mov_hor > 0))
        {
            Flip();
        }
        
        //Parte Pulo:
        if (NoChao())
        {
            coyoteTimer = coyoteDelay;
        }
        else
        {
            coyoteTimer -= Time.deltaTime;
        }
        if (Input.GetButtonDown("Jump") && coyoteTimer > 0) //pula com a adiçao dp coyote timer
        {
            rb.velocity = new Vector2(rb.velocity.x, forca_pulo);
        }
        if (Input.GetButtonUp("Jump") && (rb.velocity.y > 0)) //faz o pulo ser menor caso solte o botao
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
            coyoteTimer = 0f;
        }

        //Parte pegar input dos movimentos
        mov_hor = Input.GetAxis("Horizontal");
        mov_vert = Input.GetAxis("Vertical");
    }
    private void FixedUpdate()
    {
        //Parte movimentar player horizontalmente
        rb.velocity = new Vector2(vel_Mov * mov_hor, rb.velocity.y);
    }
    private bool NoChao()
    {
        return Physics2D.OverlapCircle(chaoCheck.position, 0.2f, chaoLayer);
    }
    private void Flip()
    {
        facingRight = !facingRight;
    }
}
