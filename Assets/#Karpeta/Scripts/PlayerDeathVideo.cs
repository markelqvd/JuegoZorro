using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class PlayerDeathVideo : MonoBehaviour
{
    [Header("Video y UI")]
    public GameObject deathCanvas;
    public VideoPlayer videoPlayer;
    public VideoClip deathIntroClip;
    public VideoClip deathLoopClip;

    [Header("Checkpoint y jugador")]
    public Transform checkpoint;
    public GameObject playerController;

    private bool isDead = false;
    private bool readyToRespawn = false;

    private void Start()
    {
        deathCanvas.SetActive(false);
    }

    void Update()
    {
        if (isDead && readyToRespawn && Input.GetKeyDown(KeyCode.E))
        {
            Respawn();
        }
    }

    public void Die()
    {
        isDead = true;
        readyToRespawn = false;

        // Activar video y UI
        deathCanvas.SetActive(true);
        playerController.SetActive(false);

        // Reproducir animación de muerte (intro)
        videoPlayer.clip = deathIntroClip;
        videoPlayer.isLooping = false;
        videoPlayer.Play();

        // Cuando termina la intro, cambia al video loop
        videoPlayer.loopPointReached += PlayLoopVideo;
    }

    void PlayLoopVideo(VideoPlayer vp)
    {
        videoPlayer.clip = deathLoopClip;
        videoPlayer.isLooping = true;
        videoPlayer.Play();
        readyToRespawn = true;

        // Para que no se dispare infinitamente si vuelve a acabar el loop
        videoPlayer.loopPointReached -= PlayLoopVideo;
    }

    void Respawn()
    {
        isDead = false;
        readyToRespawn = false;

        // Ocultar UI y parar el video
        deathCanvas.SetActive(false);
        videoPlayer.Stop();

        // Mover al checkpoint y reactivar controles
        transform.position = checkpoint.position;
        playerController.SetActive(true);
    }
}
