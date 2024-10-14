using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class RinoBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D rigidbody2D;
    BoxCollider2D boxCollider2D;
    SpriteRenderer spriteRenderer;
    Animator animator;

    

    int par_impar;
    float time_mov;
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        time_mov = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time > time_mov)
        {

            if((par_impar % 2 )== 0)
            {
                                                  //x
                rigidbody2D.velocity = new Vector2(10,0);
                spriteRenderer.flipX = true;
            }
            else
            {
                rigidbody2D.velocity = new Vector2(-10,0);
                spriteRenderer.flipX = false;
            }
            par_impar++;

            time_mov += 5f;

        }
    }
    
   

    void RinoDestroy()
    {
        Destroy(gameObject);
    }
}
