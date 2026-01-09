using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// アニメーション８話 4:30
/// </summary>
public class PlayerController : MonoBehaviour
{
    Rigidbody2D rigidbody2d;
    Animator animator;
    float moveSpeed = 5;
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        ///animator = GetComponent<Animator>();
    }
    void Update()
    {
        movePlayer();
    }

    void movePlayer()
    {
        Vector2 dir = Vector2.zero;
        string trigger = "";

        if(Input.GetKey(KeyCode.UpArrow))
        {
            dir += Vector2.up;
            trigger = "isUp";
        }
        if(Input.GetKey(KeyCode.DownArrow))
        {
            dir -= Vector2.up;
            trigger = "isDown";
        }
        if(Input.GetKey(KeyCode.RightArrow))
        {
            dir += Vector2.right;
            trigger = "isRight";
        }
        if(Input.GetKey(KeyCode.LeftArrow))
        {
            dir -= Vector2.right;
            trigger = "isLeft";
        }
        if(Vector2.zero == dir)return;
        rigidbody2d.position += dir.normalized * moveSpeed * Time.deltaTime;
        ///animator.SetTrigger(trigger);
    }

}