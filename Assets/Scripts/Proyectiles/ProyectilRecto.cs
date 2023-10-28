using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProyectilRecto : Proyectil
{

    protected override void Mover()
    {
        // define la dirección del proyectil recto hacia la izquierda
        Vector2 direction = Vector2.left;
        
        // aplica la velocidad al Rigidbody del proyectil recto
        rb.velocity = direction * speed;
    }
}
