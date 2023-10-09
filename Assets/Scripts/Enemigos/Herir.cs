using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Herir : MonoBehaviour
{
    // Variables a configurar desde el editor
    [Header("Configuracion")]
    [SerializeField] float puntos = 5f;

    //verifica colisión con el Player
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //si colisiona con el Player => modifica la vida del Player restando puntos
        if (collision.gameObject.CompareTag("Player"))
        {
            Jugador jugador = collision.gameObject.GetComponent<Jugador>();
            //PlayerController jugadorTipo2 = collision.gameObject.GetComponent<PlayerController>();
            jugador.ModificarVida(-puntos);
            //jugadorTipo2.ModificarVida(-puntos);
            Debug.Log(" PUNTOS DE DAÑO REALIZADOS AL JUGADOR " + puntos);
        }
    }
}
