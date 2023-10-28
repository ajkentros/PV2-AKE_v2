using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProyectilParabolico : Proyectil
{
    // define la variable privada float launchAngle = �ngulo de disparo del proyectil
    [SerializeField]
    [Range(0f, 90f)]
    private float launchAngle = 45f;

    // mueve el proyectil parab�lico siguiendo una trayectoria parab�lica
    protected override void Mover()
    {
        // convierte �ngulos a radianes
        float launchAngleInRadians = launchAngle * Mathf.Deg2Rad;
       
        // define un vector2 launchVelocity para el lanzamiento del proyectil parab�lico (usa el coseno para la componente x; usa el seno para la componente y)
        Vector2 launchVelocity = new(Mathf.Cos(launchAngleInRadians) * speed, Mathf.Sin(launchAngleInRadians) * speed);

        // aplica la velocidad inicial al objeto proyectil parab�lico
        rb.velocity = launchVelocity;

    }
}
