using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Saltar : MonoBehaviour
{
    [SerializeField] float rayDistance;         // variable para la distancia del rayo
    [SerializeField] LayerMask groundLayer;     // variable para detectar las capas del nivel
                        //[SerializeField] float coyoteConfig = 0.1f; // define el tiempo del coyote Time (ventaja en tiempo que tiene el jugador para el movimiento de salto)
                         //[SerializeField] int maxJumpCount = 2;      // define la cantidad de saltos seguidos
    [SerializeField] GameObject jumpTrail;      // GameObject referencia al trail del salto
                       // [SerializeField] float maxJumpForce = 10f;  // define la máxima fuerza de salto
                        //[SerializeField] float jumpForceIncreaseSpeed = 2f; //define el incremento de la fuerza de salto


    private Jugador jugador;        // variable del tipo Jugador
    private float coyoteTime;       // define el tiempo actual + tiempo de coyote time
    private readonly float amplitudVector = 25f;   // define parámetro del vector que afecta al Rigibody Jugador, en x e y


    // Variables de uso interno en el script
    private bool puedoSaltar = true;        // puede saltar = true
    private bool saltando = false;          // está saltando = true
    private int jumpCount;                  // cuenta los saltos seguidos
    private bool canDash;                   // define una variable para manejar una mecánica de dash
    private bool canDown;                   // define una variable para menejar el impulso vertical hacia abajo
                                            // private float auxForce = 5f;            // fuerza aplicada para el Raycast

    // Variable para referenciar otro componente del objeto
    private PerfilJugador perfilJugador;
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

        // obtienela referencia al perfil del jugador
        perfilJugador = jugador.PerfilJugador;
    }

    // Codigo ejecutado en cada frame del juego (Intervalo variable)
    void Update()
    {
        // toma el valor bool del estado del Raycast
        puedoSaltar = IsGrounded();
        // activa o desactiva el trail según el flag pyuedoSaltar
        jumpTrail.SetActive(!puedoSaltar);

        // si puedo saltar => coyote time = tiempo de juego (en segundo) + coyoteCOnfig (tiempo de ventaja) = margen de salto solo se calcula cuando el jugador está habilitado para saltar, y reincia la cantidad de saltos máximos con junpCount = 0
        if (puedoSaltar)
        {
            //coyoteTime = Time.time + coyoteConfig;
            coyoteTime = Time.time + perfilJugador.CoyoteConfig;
            jumpCount = 0;
        }

        // si se hace clic en la tecla space => la fuerza = al mínimo entre la fuerza * incremento * el tiempo transcurrido de juego, o el máximo de fuerza definido
        if (Input.GetKey(KeyCode.Space))
        {
            //auxForce = Mathf.Min(auxForce + jumpForceIncreaseSpeed * Time.deltaTime, maxJumpForce);
            //Debug.Log(auxForce);

            perfilJugador.AuxForce = Mathf.Min(perfilJugador.AuxForce + perfilJugador.JumpForceIncreaseSpeed * Time.deltaTime, perfilJugador.MaxJumpForce);
            Debug.Log(perfilJugador.AuxForce);
        }

        /*
            si la tecla space y la puedeoSaltar = true =>
                modifica el valor de la orden de salto, saltando = true, el Dash y el Down
                si el audio del salto está activo => retornar el valor true (lo que aplica la fuerza de salto)
        */
        //if (Input.GetKeyUp(KeyCode.Space) && (Time.time <= coyoteTime) && (jumpCount < maxJumpCount))
        if (Input.GetKeyUp(KeyCode.Space) && (Time.time <= coyoteTime) && (jumpCount < perfilJugador.MaxJumpCount))
        {
            saltando = true;
            canDash = true;
            canDown = true;

            jumpCount++;

            Debug.Log("Saltando");

            if (miAudioSource.isPlaying) 
            { 
                return; 
            }
            //miAudioSource.PlayOneShot(jumpSFX);
            Play(jugador.PerfilJugador.JumpSFX);
        }

        // si se hace clic en la tecla E => se desactiva al canDash
        if (canDash && !puedoSaltar && Input.GetKeyDown(KeyCode.E))
        {
            canDash = false;
        }

        // si se hace clic en la tecla R => se desactiva canDown
        if (canDown && !puedoSaltar && Input.GetKeyDown(KeyCode.R))
        {
            canDown = false;
        }
    }

    private void FixedUpdate()
    {
        // si saltando = true => se aplica la fuerza de salto al rigibody del jugador y saltando = true
        if (saltando)
        {
            Debug.Log("Aplicar fuerza de salto");
            miRigidbody2D.AddForce(Vector2.up * Mathf.Max(jugador.PerfilJugador.FuerzaSalto, perfilJugador.AuxForce), ForceMode2D.Impulse);
            saltando = false;
        }

        // si canDash = True => muevo el jugador hacia el eje horizontal y activo el canDash
        if (!canDash)
        {
            Debug.Log("DASH");
            miRigidbody2D.velocity = new Vector2(amplitudVector * Input.GetAxisRaw("Horizontal"), miRigidbody2D.velocity.y);
            canDash = true;
        }

        // si canDown = True => muevo el jugador hacia abajo y activo el canDown
        if (!canDown)
        {
            Debug.Log("DOWN");
            miRigidbody2D.velocity = new Vector2(miRigidbody2D.velocity.x, -amplitudVector);
            canDown = true;
        }
    }

    // Codigo ejecutado cuando el jugador colisiona con otro objeto
    private void OnCollisionEnter2D(Collision2D collision)
    {
        /*
         si está ejecutando el sonido => activa el perfil del jugador : fuerza del salto
         
         */
        if (miAudioSource.isPlaying) 
        { 
            return; 
        }
        //miAudioSource.PlayOneShot(collisionSFX);
        Play(jugador.PerfilJugador.CollisionSFX);
    }

    // play aduio cuando choca el jugador
    private void Play (AudioClip audioclip)
    {
        miAudioSource.PlayOneShot(audioclip);
    }

    // cosntruye el raycast
    private bool IsGrounded()
    {
        // define variable hit del tipo Raycast usando el método Physics2D.Raycast
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, rayDistance, groundLayer);


        // retorna un valor bool = true si el collider del Raycast es disntinto de null
        return hit.collider != null;
    }

    // gizmo genérico para el Raycast
    private void OnDrawGizmos()
    {
        // define el color el dibujo del gizmo y el dibujo de un rayo (DrawRay)
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, Vector2.down * rayDistance);
    }
}
