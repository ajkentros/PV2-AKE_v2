using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saltar : MonoBehaviour
{
    private Jugador jugador;


    // Variables de uso interno en el script
    private bool puedoSaltar = true;
    private bool saltando = false;

    // Variable para referenciar otro componente del objeto
    private Rigidbody2D miRigidbody2D;
    private AudioSource miAudioSource;

    // Activa el componente
    private void Awake()
    {
        jugador = GetComponent<Jugador>();
    }

    // Instancia objetos cuando se habilita el nivel
    private void OnEnable()
    {
        miRigidbody2D = GetComponent<Rigidbody2D>();
        miAudioSource = GetComponent<AudioSource>();
        jugador = GetComponent<Jugador>();
    }

    // Codigo ejecutado en cada frame del juego (Intervalo variable)
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && puedoSaltar)
        {
            puedoSaltar = false;

            if (miAudioSource.isPlaying) 
            { 
                return; 
            }
            //miAudioSource.PlayOneShot(jumpSFX);
            Play(jugador.PerfilJugador.JumpSFX);
        }
    }

    private void FixedUpdate()
    {
        if (!puedoSaltar && !saltando)
        {
            miRigidbody2D.AddForce(Vector2.up * jugador.PerfilJugador.FuerzaSalto, ForceMode2D.Impulse);
            saltando = true;
        }
    }

    // Codigo ejecutado cuando el jugador colisiona con otro objeto
    private void OnCollisionEnter2D(Collision2D collision)
    {
        puedoSaltar = true;
        saltando = false;

        if (miAudioSource.isPlaying) 
        { 
            return; 
        }
        //miAudioSource.PlayOneShot(collisionSFX);
        Play(jugador.PerfilJugador.CollisionSFX);
    }

    private void Play (AudioClip audioclip)
    {
        miAudioSource.PlayOneShot(audioclip);
    }

    // construye el Raycast cuando salta
    private bool IsGrounded()
    {
        //
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, rayDistance, groundLayer);
        return hit.collider != null;
    }

}
