using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Player2Movement : MonoBehaviour
{
    //private Rigidbody2D rigidbody2D;
    //private SpriteRenderer sprite;

    //[SerializeField] private float speed = 6f;


    // Start is called before the first frame update
    void Start()
    {

    }
    private void Awake()
    {
        //rigidbody2D = GetComponent<Rigidbody2D>();
        //sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //float _horizontal = Input.GetAxisRaw("Horizontal");
        //rigidbody2D.velocity = new Vector2(_horizontal * speed, rigidbody2D.velocity.y);
        //if(_horizontal > 0)
        //{
        //    sprite.flipX = false;
        //}
        //else if(_horizontal < 0)
        //{
        //    sprite.flipX = true;
        //}
        //if (Input.GetKey(KeyCode.Space)) 
        //{
        //    rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, speed);
        //}
    }
}
