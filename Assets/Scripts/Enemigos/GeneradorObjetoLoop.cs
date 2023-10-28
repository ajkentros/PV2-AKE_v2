using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneradorObjetoLoop : MonoBehaviour
{
    // define un variable GameObject objetoPrefab
    [SerializeField] private GameObject objetoPrefab;

    // define una variable float tiempoEspera para comenzar a generar un objeto
    [SerializeField]
    [Range(0.5f, 5f)]
    private float tiempoEspera;

    // define una variable float tiempoIntervalo para generar un objeto después de otro
    [SerializeField]
    [Range(0.5f, 5f)]
    private float tiempoIntervalo;

    void Start()
    {
        InvokeRepeating(nameof(GenerarObjetoLoop), tiempoEspera, tiempoIntervalo);
    }

    // instancia el objeto (un prefab) en la posición y rotación
    void GenerarObjetoLoop()
    {
        Instantiate(objetoPrefab, transform.position, Quaternion.identity);
    }
}