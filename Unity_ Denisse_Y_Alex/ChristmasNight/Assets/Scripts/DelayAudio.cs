using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayedAudio : MonoBehaviour
{
    public AudioSource audioSource; 

    void Start()
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
        
        StartCoroutine(WaitAndPlayAudio(10f)); // Espera 10 segundos antes de reproducir
    }

    IEnumerator WaitAndPlayAudio(float delay)
    {
        yield return new WaitForSeconds(delay); // Espera el tiempo especificado
        audioSource.Play(); // Inicia la reproducción del audio
    }
}