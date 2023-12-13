using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Imagenes")]
    [SerializeField] Image caughtImage;
    [SerializeField] Image wonImage;
    [Header("Tiempos Imagenes")]
    [SerializeField] float fadeDuration; // duración del fade de la imagen (la imagen va a aparecer poco a poco)
    [SerializeField] float displayImageDuration; // total del tiempo que voy a dejar la imagen en pantalla

    public bool isPlayerAtExit; // para saber si el player ha llegado a la salida
    public bool isPlayerCaught; // para saber si han pillado al player

    [Header("AudioClips")]
    [SerializeField] AudioClip caughtClip;
    [SerializeField] AudioClip wonClip;

    AudioSource audioSource;
    float timer; // contador de tiempo
    bool restartLevel; // para saber si hay que resetear el nivel o no


    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // el player ha llegado a la salida
        if(other.CompareTag("Player"))
        {
            isPlayerAtExit = true;
        }
    }

    void Update()
    {
        if (isPlayerAtExit) Won();
        else if (isPlayerCaught) Caught();
    }

    void Won()
    {
        audioSource.clip = wonClip;
        if (audioSource.isPlaying == false) audioSource.Play();

        timer += Time.deltaTime;
        //aumentar el canal alpha de la imagen poco a poco
        float apha = timer / fadeDuration;
        wonImage.color = new Color(wonImage.color.r, wonImage.color.g, wonImage.color.b, apha);

        // dejar la imagen en pantalla un tiempo
        if(timer > fadeDuration + displayImageDuration)
        {
            Debug.Log("He ganado ;-)");
        }

    }

    // me ha pillado (se acaba la partida)
    void Caught()
    {
        audioSource.clip = caughtClip;
        if (audioSource.isPlaying == false) audioSource.Play();

        timer += Time.deltaTime;
        //aumentar el canal alpha de la imagen poco a poco
        float canalApha = timer / fadeDuration;
        caughtImage.color = new Color(caughtImage.color.r, caughtImage.color.g, caughtImage.color.b, canalApha);

        // dejar la imagen en pantalla un tiempo
        if (timer > fadeDuration + displayImageDuration)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");
        }
    }


}
