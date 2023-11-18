using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Jugador : MonoBehaviour, IDamageable
{
    // variable perfilJugador del Tipo PerfilJugador, serializada y encapsulada
    [SerializeField]
    private PerfilJugador perfilJugador;
    public PerfilJugador PerfilJugador { get => perfilJugador; }

     //evento: cambia las vidas del jugador
    [SerializeField]
    private UnityEvent<int> OnLivesChanged;

    //evento: cambia el texto de la UI
    [SerializeField]
    private UnityEvent<string> OnTextChanged;

    // referencia al GameManager para luego acceder al evento victoria OnVictory

    //private GameManager gameManager;
#pragma warning restore IDE0052 // Quitar miembros privados no leídos




    private void Start()
    {
        /*
         * invoca al evento OnLiveChanged:toma las vidas del jugador
         * invoca al evento OnTextChanged:muestra las vidas del jugador en la UI
        */
        OnLivesChanged.Invoke(perfilJugador.Vida);
        Debug.Log("Jugador.cs cantidad de VIDAS " + perfilJugador.Vida);
        OnTextChanged.Invoke(GameManager.Instance.GetScore().ToString());

        // obtiene la referencia al GameManager
        //gameManager = GameManager.Instance;
    }



    //modifica la vida del Player
    public void ModificarVida(int puntos)
    {
        /*
        suma vida = puntos al perfil (scriptableObject) del jugador
        adiciona los puntos*100 al GameManager
        invoca al evento OnTextChanged:muestra las vidas del jugador en la UI
        invoca al evento OnLivesChanged: cambia la vida del jugadoren el perfilJugador
        muestra en consola
        */
        perfilJugador.Vida += puntos;
        GameManager.Instance.AddScore(puntos * 100);
        OnTextChanged.Invoke(GameManager.Instance.GetScore().ToString());
        OnLivesChanged.Invoke(perfilJugador.Vida);

    }

    //devuelve true si quedan vidas en el Player
    private bool EstasVivo()
    {
        return perfilJugador.Vida > 0;
    }

    //verifica trigger con la Meta
    private void OnTriggerEnter2D(Collider2D collision)
    {
        /*
            si el jugador coliciona con la Meta =>
                muestra mensaje en el HUD
        */

        if (!collision.gameObject.CompareTag("Meta")) { return; }

        Debug.Log("GANASTE");


        // llama al evento de victoria
        GameEvents.TriggerVictory();
    }

    public void TakeDamage(int damage)
    {
        ModificarVida(damage);
    }

    private void Update()
    {
        // si el Jugador no estás vivo o el score (obtenido de game manager) < 0 =>
        //      se llama al evento GameOver


        if (!EstasVivo() || GameManager.Instance.GetScore() < 0)
        {
            GameEvents.TriggerGameOver();
        }
        else
        {
            Debug.Log(EstasVivo());
        }
    }
}
