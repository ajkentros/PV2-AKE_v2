using UnityEngine;

public class PlayerController : MonoBehaviour, IDamageable
{
    // Variables a configurar desde el editor
    [Header("Configuracion")]
    [SerializeField] private AudioClip jumpSFX;
    [SerializeField] private AudioClip collisionSFX;


    public PlayerData playerData;

    // Variable para referenciar otro componente del objeto
    private Rigidbody2D rb2D;
    private Animator miAnimator;
    private SpriteRenderer miSprite;
    private CircleCollider2D miCollider2D;
    private AudioSource miAudioSource;

    // Variables de uso interno en el script
    private bool puedoSaltar = true;
    private bool saltando = false;
    private float moverHorizontal;
    private Vector2 direccion;
    private int saltarMask;


    private void OnEnable()
    {
        rb2D = GetComponent<Rigidbody2D>();
        miAudioSource = GetComponent<AudioSource>();
        miAnimator = GetComponent<Animator>();
        miSprite = GetComponent<SpriteRenderer>();
        miCollider2D = GetComponent<CircleCollider2D>();
        saltarMask = LayerMask.GetMask("Pisos", "Plataformas");
    }

    private void Update()
    {
        // Controla el salto del personaje
        if (Input.GetKeyDown(KeyCode.Space) && puedoSaltar)
        {
            puedoSaltar = false;

            if (miAudioSource.isPlaying)
            {
                return;
            }
            Play(jumpSFX);
        }


        // Mueve el personaje horizontalmente y modifica la animación
        moverHorizontal = Input.GetAxis("Horizontal");
        direccion = new Vector2(moverHorizontal, 0f);

        int velocidadX = (int)rb2D.velocity.x;
        miSprite.flipX = velocidadX < 0;
        miAnimator.SetInteger("Velocidad", velocidadX);

        miAnimator.SetBool("EnAire", !EnContactoConPlataforma());
    }

    // Controla la fuerza del salto
    private void FixedUpdate()
    {
        if (!puedoSaltar && !saltando)
        {
            rb2D.AddForce(Vector2.up * playerData.jumpHeight, ForceMode2D.Impulse);
            saltando = true;
        }

        //Apica una fuerza al body del jugador para moverse según dirección y velocidad
        rb2D.AddForce(direccion * playerData.speed);
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

        Play(collisionSFX);
    }

    //Reproduce audio cuando colisiona con otro objeto
    private void Play(AudioClip audioclip)
    {
        miAudioSource.PlayOneShot(audioclip);
    }

    //Devuelve true o false si el jugador toca alguna plataforma
    private bool EnContactoConPlataforma()
    {
        return miCollider2D.IsTouchingLayers(saltarMask);
    }

    //modifica la vida del Player
    public void ModificarVida(float puntos)
    {
        playerData.health += puntos;
        Debug.Log(EstasVivo());
    }

    public void TakeDamage(int damage)
    {
        playerData.health -= damage;
        if (playerData.health <= 0)
        {
            // Lógica para manejar la muerte del jugador
            Debug.Log("MUERTO");
        }
    }

    //devuelve true si quedan vidas en el Player
    private bool EstasVivo()
    {
        return playerData.health > 0;
    }

    //verifica trigger con la Meta
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Meta")) { return; }

        Debug.Log("GANASTE");
    }

}
