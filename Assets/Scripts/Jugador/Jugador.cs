using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jugador : MonoBehaviour
{
    // variable perfilJugador del Tipo PerfilJugador, serielizada y encapsulada
    [SerializeField]
    private PerfilJugador perfilJugador;
    public PerfilJugador PerfilJugador { get => perfilJugador; }


    //modifica la vida del Player
    public void ModificarVida(float puntos)
    {
        perfilJugador.Vida += puntos;
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
