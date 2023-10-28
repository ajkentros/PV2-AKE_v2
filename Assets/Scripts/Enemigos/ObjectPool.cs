using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    // define una variable rpivada del tipo GameObject objectPrefab = tipo de proyectil
    [SerializeField] private GameObject objectPrefab;

    //define una variabjle privada entera poolSize = cantidad de obejtos proyectiles
    [SerializeField] private int poolSize = 5;

    // define una lista de GameObject pooledObjects = con los objetos proyectiles
    private List<GameObject> pooledObjects;

    // instancia la lista y la completa
    void Start()
    {
        // instancia pooledObject como una lista nueva
        pooledObjects = new List<GameObject>();

        // recorre la lista, de acuerdo al tamaño definido como poolSize
        //  instancia un GameObject obj con el GameObject objectPrefab (proyectil)
        //  desactiva el objeto obj
        //  adiciona el objeto obj en la lista pooledObjects
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(objectPrefab);
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }
    }

    // Retorna el objeto de la lista pooledObject que no está activo
    public GameObject GetPooledObject()
    {
        // recorre los objetos en la lista pooledObjects
        // si el objeto obj, no está activo en a jerarquía => retorna el objeto a la jerarquía
        // sino retorna nullo
        foreach (GameObject obj in pooledObjects)
        {
            if (!obj.activeInHierarchy)
            {
              return obj;
            }
        }

        return null;
    }

}
