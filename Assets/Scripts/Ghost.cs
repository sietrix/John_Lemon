using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    [SerializeField] GameManager gameManager;

    // array de posiciones para la patrulla del enemigo
    [SerializeField] Transform[] positions;
    [SerializeField] int speed;

    Vector3 posToGo; // almacena la posici�n hacia la que se dirige el fantasma
    int i; // index para controlar en que posici�n del array estoy

    Ray ray; // rayo
    RaycastHit hit; // punto de impacto del rayo
    void Start()
    {
        i = 0;
        posToGo = positions[i].position;
    }

    void Update()
    {
        Move();
        ChangePosition();
        Rotate();
    }

    private void FixedUpdate()
    {
        // subo el origen del rayo 1 metro a lo largo del eje Y con respecto al punto de pivote del objeto
        ray.origin = new Vector3(transform.position.x, transform.position.y +1, transform.position.z);
        ray.direction = transform.forward;

        if(Physics.Raycast(ray, out hit))
        {
            if(hit.collider.CompareTag("Player")) 
            {
                gameManager.isPlayerCaught = true;
            }
        }

        Debug.DrawRay(ray.origin, ray.direction * 10, Color.red);
    }

    void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, posToGo, speed * Time.deltaTime);
    }

    void ChangePosition()
    {
        // si el fantasma ya ha llegado a su destion
        if(Vector3.Distance(transform.position, posToGo) <= Mathf.Epsilon)
        {
            if (i == positions.Length - 1)
            {
                i = 0; // vuelvo al principio del array
            }
            else
            {
                i++;
            }

            posToGo = positions[i].position;
        }
    }

    void Rotate()
    {
        transform.LookAt(posToGo);
    }
}
