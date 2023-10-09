using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{


    // Variables de uso interno en el script
    private float moverHorizontal;
    private Vector2 direccion;

    // Variable para referenciar otro componente del objeto
    private Rigidbody2D miRigidbody2D;
    private Animator miAnimator;
    private SpriteRenderer miSprite;
    private CircleCollider2D miCollider2D;
    private Jugador jugador;

    private int saltarMask;

    // Codigo ejecutado cuando el objeto se activa en el nivel
    private void OnEnable()
    {
        miRigidbody2D = GetComponent<Rigidbody2D>();
        miAnimator = GetComponent<Animator>();
        miSprite = GetComponent<SpriteRenderer>();
        miCollider2D = GetComponent<CircleCollider2D>();
        saltarMask = LayerMask.GetMask("Pisos", "Plataformas");
        jugador = GetComponent<Jugador>();
    }

    // Codigo ejecutado en cada frame del juego (Intervalo variable)
    private void Update()
    {
        moverHorizontal = Input.GetAxis("Horizontal");
        direccion = new Vector2(moverHorizontal, 0f);

        int velocidadX = (int)miRigidbody2D.velocity.x;
        miSprite.flipX = velocidadX < 0;
        miAnimator.SetInteger("Velocidad", velocidadX);

        miAnimator.SetBool("EnAire", !EnContactoConPlataforma());
    }

    //Apica una fuerza al body del jugador para moverse según dirección y velocidad
    private void FixedUpdate()
    {
        miRigidbody2D.AddForce(direccion * jugador.PerfilJugador.VelocidadHorizontal);
    }

    //Devuelve true o false si el jugador toca alguna plataforma
    private bool EnContactoConPlataforma()
    {
        return miCollider2D.IsTouchingLayers(saltarMask);
    }

}
