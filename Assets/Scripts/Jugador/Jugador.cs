using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Jugador : MonoBehaviour
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

    //invoca al evento OnLiveChanged:toma las vidas del jugador
    //invoca al evento OnTextChanged:muestra las vidas del jugador en la UI
    private void Start()
    {
        OnLivesChanged.Invoke(perfilJugador.Vida);
        OnTextChanged.Invoke(perfilJugador.Vida.ToString());
    }

    //modifica la vida del Player
    //invoca al evento OnTextChanged:muestra las vidas del jugador en la UI
    public void ModificarVida(int puntos)
    {
        perfilJugador.Vida += puntos;
        OnTextChanged.Invoke(perfilJugador.Vida.ToString());
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
}
