using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Curar : MonoBehaviour
{
    [Header("Configuracion")]
    [SerializeField] int puntos = 1;

    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Player"))
        {
            Jugador jugador = other.GetComponent<Jugador>();
            jugador.ModificarVida(puntos);
            Debug.Log(" PUNTOS DE DAÑO REALIZADOS AL JUGADOR " + puntos);
        }
    }
}
