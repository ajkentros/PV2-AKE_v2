using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NuevoPerfilJugador", menuName = "ScriptableObjects/Perfil Jugador")]

public class PerfilJugador : ScriptableObject
{
    // variable experienciaProximoNivel encapsulada y serializable
    [Header("Configuraciones de Experiencia")]

    [SerializeField]
    [Tooltip("Experiencia necesaria para el proximo nivel (10 a 50)")]
    [Range(10, 50)]
    private int experienciaProximoNivel;
    public int ExperienciaProximoNivel { get => experienciaProximoNivel; set => experienciaProximoNivel = value; }

    // variable escalarExperiencia encapsulada y serializable
    [SerializeField]
    [Tooltip("Aumento de la experiencia en cada nivel")]
    [Range(10, 2000)]
    private int escalarExperiencia;
    public int EscalarExperiencia { get => escalarExperiencia; set => escalarExperiencia = value; }


    // variable privada nivel encapculada
    private int nivel;
    public int Nivel { get => nivel; set => nivel = value; }

    //variable privada experiencia encapsulada
    private int experiencia;
    public int Experiencia { get => experiencia; set => experiencia = value; }

    // variables de movimiento serializada, con rango y encapsulada
    [Header("Configuracion de movimiento")]    

    // velocidad
    [SerializeField]
    [Range(5, 20)]
    float velocidad = 5f;
    public float VelocidadHorizontal { get => velocidad; set => velocidad = value; }

    // fuerza del salto
    [SerializeField]
    [Range(5, 50)]
    private float fuerzaSalto = 5f;
    public float FuerzaSalto { get => fuerzaSalto; set => fuerzaSalto = value; }

    // define el tiempo del coyote Time (ventaja en tiempo que tiene el jugador para el movimiento de salto)
    [SerializeField]
    private float coyoteConfig = 0.1f;
    public float CoyoteConfig { get => coyoteConfig; set => coyoteConfig = value; }

    // define la cantidad de saltos seguidos
    [SerializeField]
    private int maxJumpCount = 2;
    public int MaxJumpCount { get => maxJumpCount; set => maxJumpCount = value; }

    //define el incremento de la fuerza de salto
    [SerializeField]
    private float jumpForceIncreaseSpeed = 2f;
    public float JumpForceIncreaseSpeed { get => jumpForceIncreaseSpeed; set => jumpForceIncreaseSpeed = value; }

    // define la máxima fuerza de salto
    [SerializeField]
    private float maxJumpForce = 10f;
    public float MaxJumpForce { get => maxJumpForce; set => maxJumpForce = value; }

    // fuerza aplicada para el Raycast
    [SerializeField]
    private float auxForce = 5f;
    public float AuxForce { get => auxForce; set => auxForce = value; }


    // variables de atributos serializadas y encapsuladas
    [Header("Configuraciones de Atributos")]

    //vida
    [SerializeField]
    [Range(5, 10)]
    private int vida = 5;
    public int Vida { get => vida; set => vida = value; }




    // variables SFX serializadas y encapsuladas
    [Header("Configuraciones SFX")]

    //jumpSFX
    [SerializeField] 
    private AudioClip jumpSFX;
    public AudioClip JumpSFX { get => jumpSFX; }

    //collisionSFX
    [SerializeField] 
    private AudioClip collisionSFX;
    public AudioClip CollisionSFX { get => collisionSFX; }


}
