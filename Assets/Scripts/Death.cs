using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Death : MonoBehaviour {

    public AudioClip m_deathSound;

    private AudioSource m_audio;
    private GameObject m_GameManager;

    void Start()
    {
        m_audio = GetComponent<AudioSource>();
        // TO-DO 1 Buscar al GameManager y cachearlo
        m_GameManager = GameObject.FindGameObjectWithTag("GameManager");
        
    }

    public virtual void OnDeath()
    {

        m_audio.clip = m_deathSound;
        m_audio.Play();
        Debug.Log("Tu mueres");
        // TO-DO 2 Respaunear usando el GameManager con el mensaje RespawnPlayer.
        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        this.m_GameManager.GetComponent<GameManager>().RespawnPlayer();
    }
}
