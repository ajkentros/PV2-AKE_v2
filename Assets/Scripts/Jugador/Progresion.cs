using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Progresion : MonoBehaviour
{
    // variable perfilJugador del tipo PerfilJugador, serielizada y encapsulada
    private Jugador jugador;

    private void Awake()
    {
        jugador = GetComponent<Jugador>();
    }

    // Gestiona como gana experiencia el jugador
    public void GanarExperiencia(int nuevaExperiencia)
    {
        // incrementa la experiencia actual sumando el valor de la experiencia lograda
        jugador.PerfilJugador.Experiencia += nuevaExperiencia;

        // si la experiencia >= que la experienciaProximoNivel => se sube el nivel con SubirNivel()
        if (jugador.PerfilJugador.Experiencia >= jugador.PerfilJugador.Experiencia)
        {
            SubirNivel();
        }

    }

    private void SubirNivel()
    {
        // escala el nivel, se pasa la experiencia a cero, se incrementa la cantidad de experiencia requerida para el nuevo nivel
        jugador.PerfilJugador.Nivel++;
        jugador.PerfilJugador.Experiencia -= jugador.PerfilJugador.ExperienciaProximoNivel;
        jugador.PerfilJugador.ExperienciaProximoNivel += jugador.PerfilJugador.EscalarExperiencia;
    }
}
