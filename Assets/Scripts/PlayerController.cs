﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController player;
    public float speed;

    public delegate void OnInventoryDown();
    public OnInventoryDown onInventoryDown;

    public Inventory inventory;

    private Animator animator;
    // Use this for initialization
    void Start ()
    {
        if(player == null)
        {
            player = this;
        }
        if(player != this)
        {
            Destroy(this.gameObject);
        }

        animator = GetComponent<Animator>();
        inventory = GetComponent<Inventory>();
    }

    // Update is called once per frame
    void Update ()
    {
        HandleMovement();

        HandleInput();
    }

    private void HandleMovement()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        Vector3 translation = new Vector3(x, y, 0f);
        animator.SetBool("isWalking", translation.magnitude > 0f);
        if (translation.magnitude > 0f)
        {
            animator.SetFloat("x", x);
            animator.SetFloat("y", y);

            transform.Translate(translation.normalized * translation.magnitude * Time.deltaTime * speed);
        }
    }

    private void HandleInput()
    {
        if(Input.GetButtonDown("Inventory"))
        {
            if(onInventoryDown != null)
            {
                onInventoryDown.Invoke();
            }
            else
            {
                inventory.Display(!inventory.isUiActive);
            }
        }
    }
}