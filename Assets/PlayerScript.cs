using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{

    Rigidbody2D rigidbody2D;
    BoxCollider2D boxCollider2D;
    Animator animator;
    public LayerMask layerMask;
    public LayerMask layerMaskEnemy;

    public  GameObject rino;
    bool controller,hit;

    SpriteRenderer spriteRenderer;


    int dobleSalto = 0;
   


    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();    
        boxCollider2D = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
       
        controller = true;
        hit = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (controller)
        {
                
            if (Input.GetKey(KeyCode.RightArrow))
            {
                rigidbody2D.velocity = new Vector2(2,rigidbody2D.velocity.y);
                spriteRenderer.flipX = false;
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                rigidbody2D.velocity = new Vector2(-2,rigidbody2D.velocity.y);
                spriteRenderer.flipX = true;
            }

        // Verifica si el jugador puede saltar (máximo 2 saltos)
        //                                          0  < 2


        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Debug.Log("Salto");
            dobleSalto++;
            if(dobleSalto < 2)
            {
           
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, 10);
            }  
        }
      


        if(!hit)
        {
               // Animaciones del jugador
            if(isGrounded() && !Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow))
            {
                animator.SetInteger("estado", 0);
            }
            if(isGrounded() && (Input.GetKey(KeyCode.RightArrow)))
            {
                animator.SetInteger("estado", 1);

                Debug.Log("estoy presionando derecha o izquierda y estoy caminando");  
            } 
            if(isGrounded() && (Input.GetKey(KeyCode.LeftArrow)))
            {
                animator.SetInteger("estado", 1);
                Debug.Log("estoy presionando izquierda y estoy caminando");
            }
            
            //SHIT IZQUIERDO 
            if (isGrounded() && Input.GetKey(KeyCode.LeftShift) &&  Input.GetKey(KeyCode.RightArrow))
            {
                rigidbody2D.velocity = new Vector2(4,rigidbody2D.velocity.y);
                Debug.Log("correr derecha");
                animator.SetInteger("estado", 2);
            }
            if (isGrounded() && Input.GetKey(KeyCode.LeftShift) &&  Input.GetKey(KeyCode.LeftArrow))
            {

                rigidbody2D.velocity = new Vector2(-4,rigidbody2D.velocity.y);
                animator.SetInteger("estado", 2);
            }

            if(!isGrounded() && rigidbody2D.velocity.y > -1)
            {
                animator.SetInteger("estado",4);
            }

            if(!isGrounded() && dobleSalto == 1)
            {
                animator.SetInteger("estado",5);
            }

            //caida 
            if(rigidbody2D.velocity.y < -1.82 && (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.LeftArrow)))
            {
                animator.SetInteger("estado",3);   
            }
            

            // if(Input.GetKeyDown(KeyCode.UpArrow) && saltosRealizados < dobleSalto)
            // {
            //    
            // }

            // if(!isGrounded() && (!Input.GetKey(KeyCode.LeftShift) || !Input.GetKey(KeyCode.LeftArrow)))
            // {
            //     animator.SetInteger("estado",3);
            // }

            

        }

     
      
    
        // Resetea los saltos cuando el jugador esté en el suelo
        if (isGrounded())
        {
            dobleSalto = 0;
        }
    }


    }


    void isDestroyed()
    {

        transform.position = new Vector2(-7, -3);
        animator.SetTrigger("appear");

      //  SceneManager.LoadScene(0);
    }

    void isAppear()
    {
        
        animator.SetInteger("estado", 0);
    }


    void controllerTrue()
    {
        controller = true;
        hit = false;

    }

    // Verifica si el jugador está en el suelo
    bool isGrounded()
    {
        return Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0f, Vector2.down, .2f, layerMask);
    }

     bool isHitEnemy()
    {
        return Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0f, Vector2.down, .2f, layerMaskEnemy);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "spikes")
        {
            Debug.Log("Estoy tocando las espinas");
            controller = false;
            animator.SetTrigger("destroy");
        }

        if (collision.gameObject.tag == "rino")
        {
            if (isHitEnemy())
            {
                rino.GetComponent<Animator>().SetTrigger("destroy");
            }
            else
            {
                animator.SetTrigger("destroy");
            }

        }

       
    }

     private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.tag == "rino")
        {
            animator.SetTrigger("destroy");

        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "fire")
        {


          //  controller = false;
            animator.SetTrigger("hit");
            Debug.Log("Estoy tocando el fuego");

            if(spriteRenderer.flipX)
            {
                rigidbody2D.velocity = new Vector2(10, 1);
            }
            else 
            {
                rigidbody2D.velocity = new Vector2(-10, 1);
            }
        }
    }

}
