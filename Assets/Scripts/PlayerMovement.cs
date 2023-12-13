using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public int turnSpeed;

    Rigidbody rb;
    Animator anim;
    AudioSource audioSource;
    Vector3 movement;
    float horizontal;
    float vertical;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        InputPlayer();
        Animating();
        AudioSteps();
    }

    private void FixedUpdate()
    {
        Rotation();
    }

    // metodo que se usa cuando la velocidad del personaje está
    // implicita en la animación en vez de (speeed * Time.DetaTime)
    // se usa anim.deltaPosition.magnitude
    private void OnAnimatorMove()
    {
        rb.MovePosition(transform.position + (movement * anim.deltaPosition.magnitude));
    }

    void InputPlayer()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        movement = new Vector3(horizontal, 0, vertical);
        movement.Normalize();

    }

    void Animating()
    {
        if (horizontal != 0 || vertical != 0)
            anim.SetBool("IsMoving", true);
        else
            anim.SetBool("IsMoving", false);
    }

    void Rotation()
    {
        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, movement, turnSpeed * Time.deltaTime, 0);
        Quaternion rotation = Quaternion.LookRotation(desiredForward);
        rb.MoveRotation(rotation);
    }
    
    void AudioSteps()
    {
        if (horizontal != 0 || vertical != 0)
        {
            if(audioSource.isPlaying == false)
                audioSource.Play();
        }
        else
            audioSource.Stop();
    }


}
