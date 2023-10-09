using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneradorObjetoAleatorio : MonoBehaviour
{
    [SerializeField] private GameObject[] objetosPrefabs;

    [SerializeField]
    [Range(0.5f, 5f)]
    private float tiempoEspera;

    [SerializeField]
    [Range(0.5f, 5f)]
    private float tiempoIntervalo;

    [SerializeField]
    [Range(10f, 60f)]
    private float tiempoVisible;

    void Start()
    {
        // Activa OnBecameVisible al inicio
        OnBecameVisible();
        // Llama a OnBecameInvisible después de 30 segundos
        Invoke(nameof(OnBecameInvisible), tiempoVisible);

    }

    private void Update()
    {
       
    }


    private void OnBecameInvisible()
    {
        Debug.Log("El SpriteRenderer deja de ser visible por las cámaras en la escena");
        CancelInvoke(nameof(GenerarObjetoAleatorio));
 
    }

    private void OnBecameVisible()
    {
        Debug.Log("El SpriteRenderer es visible por las cámaras en la escena");
        InvokeRepeating(nameof(GenerarObjetoAleatorio), tiempoEspera, tiempoIntervalo);
    }

    void GenerarObjetoAleatorio()
    {
        
        int indexaleatorio = Random.Range(0, objetosPrefabs.Length);
        GameObject prefabaleatorio = objetosPrefabs[indexaleatorio];
        Instantiate(prefabaleatorio, transform.position, Quaternion.identity);
        
        
        
    }
}