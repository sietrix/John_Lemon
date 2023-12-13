using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] int smoothing; // velocidad de seguimiento de la cámara

    Vector3 offset; // distancia inicial que hay entre la camara y el player
    void Start()
    {
        offset = transform.position - player.position;
    }

    // Aquí se suele calcular la posición de la cámara
    // este metodo se ejecuta despues de el Update y FixedUpdate
    void LateUpdate()
    {
        // posición a la que quiero mover la cámara
        Vector3 desiredPosition = player.position + offset;

        // muevo la cámara
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothing * Time.deltaTime);
    }
}
