using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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

    private void Start()
    {
        /*
         * invoca al evento OnLiveChanged:toma las vidas del jugador
         * invoca al evento OnTextChanged:muestra las vidas del jugador en la UI
        */
        OnLivesChanged.Invoke(perfilJugador.Vida);
        OnTextChanged.Invoke(GameManager.Instance.GetScore().ToString());
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
        Debug.Log(EstasVivo());
    }

    //devuelve true si quedan vidas en el Player
    private bool EstasVivo()
    {
        return perfilJugador.Vida > 0;
    }

    //verifica trigger con la Meta
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Meta")) { return; }

        Debug.Log("GANASTE");
    }

    public void TakeDamage(int damage)
    {
        ModificarVida(damage);
    }
}
