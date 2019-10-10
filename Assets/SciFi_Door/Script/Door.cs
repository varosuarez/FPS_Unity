using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {

    /// <summary>
    /// Estados posibles de la puerta
    /// </summary>
    public enum State { CLOSED, OPENING, OPEN, CLOSING}
    /// <summary>
    /// animaci�n de abrir la puerta
    /// </summary>
    public string m_open;
    /// <summary>
    /// animaci�n de cerar la puerta
    /// </summary>
    public string m_close;
    /// <summary>
    /// GameObject que contiene el componente Animation con las animaciones de la puerta
    /// </summary>
    public Animation m_animation;
    /// <summary>
    /// GameObject donde est� el audio source con el sonido de la puerta
    /// </summary>
    public AudioSource m_audio;
    /// <summary>
    /// Duraci�n de la animaci�n
    /// </summary>
    public float animationDuration;

    
   /// <summary>
   /// Tiempo de animaci�n restante
   /// </summary>
    private float m_remainingTime;
    /// <summary>
    /// Estado actual de la puerta
    /// </summary>
    private State m_state = State.CLOSED;
    private int m_numElementsInTrigger;
    private float triggerErrorTime;


    void OnTriggerEnter ( Collider obj  ){
        m_numElementsInTrigger++;
        Open();
        
    }


    // ## TO-DO 3: Tiempo de seguridad que permanecer� abierta si hay alg�n error al contabilizar objetos dentro del trigger.
    /// <summary>
    /// Arregla el problema de un mal conteo de elementos. Siempre que haya algo en el trigger el contador de tiempo de rescate de error tendra el valor de la duraci�n de la animaci�n
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerStay(Collider other)
    {
        triggerErrorTime = animationDuration;
    }

    void OnTriggerExit ( Collider obj  ){
        m_numElementsInTrigger--;
        Close();
    }

    public State DoorState
    {
        get { return m_state; }
    }

    public void Open()
    {
        //Solo abrimos la puerta si est� cerrada
        // ## TO-DO 1: Abrimos la puerta.
        if (m_state == State.CLOSED && m_numElementsInTrigger > 0)
        {
            m_state = State.OPENING;
            m_animation.Play(m_open);
            m_audio.Play();
            m_remainingTime = animationDuration;
        }
    }

    public void Close()
    {
        //solo cerramos la puerta si est� abierta
        // ## TO-DO 2: Cerramos la puerta
        if (m_state == State.OPEN && m_numElementsInTrigger <= 0)
        {
            m_state = State.CLOSING;
            m_animation.Play(m_close);
            m_audio.Play();
            m_remainingTime = animationDuration;
        }
    }

    private void RestoreDoor()
    {
        //Cuando estamos en los estados de transici�n, opening y closing al acabar el tiempo de la animaci�n hacemos una serie de comprobaciones:
        if (m_state == State.OPENING)
        {
            //Si est� abriendose, la puerta pasa a estado abierta. Pero si no hay nadie en el trigger autom�ticamente la cerramos.
            m_state = State.OPEN;
            if (m_numElementsInTrigger == 0)
                Close();
        }
        else if (m_state == State.CLOSING)
        {
            //Si estamos cerrando la puerta la puerta pasa al estado cerrado. Si hay alg�n elemento dentro del trigger inemediatamente la volvemos a abrir.
            m_state = State.CLOSED;
            if (m_numElementsInTrigger > 0)
                Open();
        }

            
    }

    private void Update()
    {
        triggerErrorTime -= Time.deltaTime;

        if (m_remainingTime > 0)
        {
            m_remainingTime -= Time.deltaTime;
            if (m_remainingTime <= 0)
                RestoreDoor();
        }
        else
        {
            // ## TO-DO 4: Mecanismo de seguridad para evitar que se quede la puerta abierta.
            //si habiendo pasado el tiempo con la puerta abierta, alguien ha habandonado el trigger, la volvemos a cerrar
            if (m_state == State.OPEN && triggerErrorTime <= 0)
            {
                m_numElementsInTrigger = 0;
                Close();
            }
        }
    }
}