using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneradorObjetoLoop : MonoBehaviour
{
    [SerializeField] private GameObject objetoPrefab;

    [SerializeField]
    [Range(0.5f, 5f)]
    private float tiempoEspera;

    [SerializeField]
    [Range(0.5f, 5f)]
    private float tiempoIntervalo;

    private int cantidadInvocaciones = 3;

    void Start()
    {
        InvokeRepeating(nameof(GenerarObjetoLoop), tiempoEspera, tiempoIntervalo);
    }

    void GenerarObjetoLoop()
    {
        if(cantidadInvocaciones > 0)
        {
            Instantiate(objetoPrefab, transform.position, Quaternion.identity);
            cantidadInvocaciones--;
        }
        
    }
}