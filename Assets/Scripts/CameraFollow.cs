using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] int smoothing; // velocidad de seguimiento de la c�mara

    Vector3 offset; // distancia inicial que hay entre la camara y el player
    void Start()
    {
        offset = transform.position - player.position;
    }

    // Aqu� se suele calcular la posici�n de la c�mara
    // este metodo se ejecuta despues de el Update y FixedUpdate
    void LateUpdate()
    {
        // posici�n a la que quiero mover la c�mara
        Vector3 desiredPosition = player.position + offset;

        // muevo la c�mara
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothing * Time.deltaTime);
    }
}
