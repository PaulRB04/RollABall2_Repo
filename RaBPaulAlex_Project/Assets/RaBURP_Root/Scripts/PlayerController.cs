using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Variables de referencia privada
    private float horInput; //Referencia al input horizontal del teclado
    private float verInput; //Referencia al input vertical del teclado

    [Header("General References")]
    public Rigidbody playerRb; //Almac�n del Rigidbody del player. Me permite moverlo
    public AudioSource playerAudio; //Referencia al reproductor de sonidos del player

    [Header("Movement Variables")]
    public float speed;

    [Header("Jump Variables")]
    public float jumpForce;
    public float jumpForceTrampolin;
    public float jumpForceMegaTrampolin;
    public bool isGrounded;
    public bool isGroundedTrampolin;
    public bool isGroundedMegaTrampolin;

    [Header("Sound Library")]
    public AudioClip[] soundLibrary; //"Estanter�a" de sonidos del player

    [Header("Respawn Configuration")]
    public GameObject respawnPoint; //Ref al objeto que marca el punto de respawn (transform)
    public float fallLimit; //Valor en -y que al alcanzarlo se ejecutar� el respawn


    // Start is called before the first frame update
    void Start()
    {
        isGrounded = true;
    }

    // Update is called once per frame
    void Update()
    {
        //Almacenar de manera constante el input de teclado en los ejes X e Y
        horInput = Input.GetAxis("Horizontal");
        verInput = Input.GetAxis("Vertical");
        Jump();
        JumpTrampolin();
        JumpMegaTrampolin();
        if (transform.position.y < fallLimit) { Respawn(); }
        
    }

    private void FixedUpdate()
    {
        //Aqu� se codea/llama a acciones que dependan de la f�sica CONSTANTE
        //VelocityMove();
        ForceMove();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            isGroundedTrampolin = false;
            isGroundedMegaTrampolin = false;
        }
        
       if (collision.gameObject.CompareTag("Trampolin"))
        {
            isGroundedTrampolin = true;
            isGrounded = false;
            isGroundedMegaTrampolin = false;
        }

       if (collision.gameObject.CompareTag("MegaTrampolin"))
        {
            isGroundedMegaTrampolin = true;
            isGroundedTrampolin = false;
            isGrounded = false;
        }

        if (collision.gameObject.CompareTag("Spike"))
        {
            transform.position = respawnPoint.transform.position;
        }

        if (collision.gameObject.CompareTag("PlataformaMovible"))
        {
            transform.parent = collision.transform;
            isGrounded = true;
        }

       
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("PlataformaMovible"))
        {
            transform.parent = null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            playerAudio.PlayOneShot(soundLibrary[2]);
        }
    }

    void VelocityMove()
    {
        //Movimiento basado en afectar al velocity: "Motor" que imita la capacidad de moverse de un ser vivo
        playerRb.velocity = new Vector3(horInput * speed, playerRb.velocity.y, verInput * speed);
    }

    void ForceMove()
    {
        //Movimiento basado en aplicar fuerza de empuje en dos ejes: X/Z
        playerRb.AddForce(Vector3.right * horInput * speed);
        playerRb.AddForce(Vector3.forward * verInput * speed);
    }
    
    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded == true)
        {
            playerAudio.PlayOneShot(soundLibrary[0]);
            isGrounded = false;
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

    }

    void JumpTrampolin()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGroundedTrampolin == true)
        {
            playerAudio.PlayOneShot(soundLibrary[0]);
            isGroundedTrampolin = false;
            playerRb.AddForce(Vector3.up * jumpForceTrampolin, ForceMode.Impulse);
        }
    }

    void JumpMegaTrampolin()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGroundedMegaTrampolin == true)
        {
            playerAudio.PlayOneShot(soundLibrary[0]);
            isGroundedMegaTrampolin = false;
            playerRb.AddForce(Vector3.up * jumpForceMegaTrampolin, ForceMode.Impulse);
        }

    }


    void Respawn()
    {
        playerAudio.PlayOneShot(soundLibrary[1]);
        //Cambia la posici�n del player por la posici�n del punto de respawn
        transform.position = respawnPoint.transform.position;
        //Impide que la pelota siga moviendose tras el respawn
        playerRb.velocity = Vector3.zero * speed;

    }




}
