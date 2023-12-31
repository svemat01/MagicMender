using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum PlayerItemAnimation
{
    None, // 0
    Hammer, // 1
}
public class PlayerController : MonoBehaviour
{

    public float PlayerMoney = 0.0f;
    public static PlayerController Instance { get; private set; }
    public Rigidbody2D rb { get; private set; }
    public float moveSpeed = 5f;

    public Animator animator;
    public bool freeze = false;
    private Vector2 movement;
    
    public PlayerItemAnimation Item = PlayerItemAnimation.None;
    public AudioSource audioSource;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        PlayerMoney = 100.0f;
        Instance = this;
        audioSource = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
        movement = Vector2.zero;
    }

    // Update is called once per frame
    private void Update()
    {
        if (freeze)
        {
            movement = Vector2.zero;
            return;
        }
        // use input to set movement vector
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        
        // set animator parameters
        animator.SetFloat("Speed", movement.sqrMagnitude);
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);

        // Play walking audio if moving
        if (movement.sqrMagnitude > 0.0f)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            audioSource.Stop();
        }

        if(PlayerMoney <= 0.0f)
        {

            SceneManager.LoadScene("GameOver");

        }
    }
    
    private void FixedUpdate()
    {
        // move the player
        rb.MovePosition(rb.position + movement * (moveSpeed * Time.fixedDeltaTime));
    }
    
    public void EquipItem(PlayerItemAnimation Item)
    {
        animator.SetInteger("Item", (int)Item);
    }
}
