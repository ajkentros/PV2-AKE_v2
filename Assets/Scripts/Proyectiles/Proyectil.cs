using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Proyectil : MonoBehaviour
{
    // define variable protegida float speed = velocidad del proyectil
    [SerializeField]
    [Range(1f, 30f)]
    protected float speed = 10f;

    // define variable del tipo Rigibody2D rb = toma el rigibody del componente
    protected Rigidbody2D rb;

    // toma el rigibody del componente y lo asigna a la variable rb
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // activa el método Move() mientras el objeto esté habilitado
    private void OnEnable()
    {
        Mover();
    }

    // protege el método Mover y lo define abstracto para que pueda ser reescrito por otro script
    protected abstract void Mover();
}
