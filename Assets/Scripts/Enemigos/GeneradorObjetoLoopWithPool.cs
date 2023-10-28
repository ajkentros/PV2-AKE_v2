using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneradorObjetoLoopWithPool : MonoBehaviour
{
    // define una variable float tiempoEspera del generador para comenzar a disparar los proyectiles
    [SerializeField]
    [Range(0.5f, 5f)]
    private float tiempoEspera;

    // define una variable float tiempoIntervalo del generador entre cada proyectil disparado
    [SerializeField]
    [Range(0.5f, 5f)]
    private float tiempoIntervalo;

    // define una variable float para el tiempo de vida del proyectil
    [SerializeField]
    [Range(0f, 60f)]
    private float timeOfLive; 

    // define un variable objectPoll del tipo ObjectPool 
    private ObjectPool objectPool;

    // asigna a la variable objectPool la referencia al componente del tipo ObjectPool
    private void Awake()
    {
        objectPool = GetComponent<ObjectPool>();
    }

    // invoca al m�todo GenerarObjetoLoop seg�n el tiempoEspera y tiempoIntervalo
    void Start()
    {
        InvokeRepeating(nameof(GenerarObjetoLoop), tiempoEspera, tiempoIntervalo);
    }

    // reutiliza objetos
    void GenerarObjetoLoop()
    {
        // obtiene un objeto de objectPool utilizando el m�todo GetPooledObject()
        GameObject pooledObject = objectPool.GetPooledObject();

        // si el GameObject polledObject no es null => se puede continuar:
        //      establece la posici�n del objeto obtenido (pooledObject) = a la posici�n del objeto que contiene este script (transform.position)
        //      establece la rotaci�n del objeto obtenido en la identidad(Quaternion.identity) = rotaci�n nula
        //      se activa el objeto obtenido = es visible y puede interactuar con otros objetos en la escena
        //      desactiva el proyectil despu�s de cierto tiempo (timeOfLive)
        if (pooledObject != null)
        {
            pooledObject.transform.SetPositionAndRotation(transform.position, Quaternion.identity);
            pooledObject.SetActive(true);

            
            StartCoroutine(DesactivarProyectil(pooledObject));
        }
    }

    private IEnumerator DesactivarProyectil(GameObject proyectil)
    {
        yield return new WaitForSeconds(timeOfLive);
        proyectil.SetActive(false);
    }
}
