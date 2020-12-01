using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State_TeleportPlayerAction { DEACTIVATED, ACTIVATED, FADINGIN, FINISHED }

public class TeleportPlayerAction : InteractableAction
{
    [SerializeField] private State_TeleportPlayerAction state = State_TeleportPlayerAction.DEACTIVATED;
    [SerializeField] private Transform teleportPosition;
    [SerializeField] private float activationDelay;
    [SerializeField] private float fadeTime;
    [SerializeField] private GameObject elevator;

    private float timer;
    private GameObject fadeOverlay;
    private Transform playerTransform;
    private SpriteRenderer fadeSprite;

    // Start is called before the first frame update
    void Start()
    {
        fadeOverlay = GameObject.FindGameObjectWithTag("FadeOverlay");
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        fadeSprite = fadeOverlay.GetComponent<SpriteRenderer>();
        Color tmp = fadeSprite.color;
        tmp.a = 0;
        fadeSprite.color = tmp;
    }

    public override void Activate()
    {
        if (state == State_TeleportPlayerAction.FINISHED || state == State_TeleportPlayerAction.FADINGIN) return;
        elevator.SetActive(true);
        state = State_TeleportPlayerAction.ACTIVATED;
    }

    public override void Deactivate()
    {
        state = State_TeleportPlayerAction.DEACTIVATED;
    }

    public override void InputData(SongData data)
    {
        return;
    }

    public override void Reset()
    {
        return;
    }


    // Update is called once per frame
    void Update()
    {
        if (state == State_TeleportPlayerAction.ACTIVATED)
        {
            timer += Time.deltaTime;
            fadeOverlay.GetComponent<Transform>().position = playerTransform.position;
            if (timer > activationDelay)
            {
                if (timer > activationDelay + fadeTime)
                {
                    playerTransform.position = teleportPosition.position;
                    GameObject.FindGameObjectWithTag("MainCamera").transform.position = teleportPosition.position;
                    state = State_TeleportPlayerAction.FADINGIN;
                    timer = -1f;

                    Color tmp = fadeSprite.color;
                    tmp.a = 1;
                    fadeSprite.color = tmp;
                }
                else
                { // fade to solid color
                    Color tmp = fadeSprite.color;
                    tmp.a = (timer - activationDelay) / fadeTime;
                    fadeSprite.color = tmp;
                }
            }
        }

        else if (state == State_TeleportPlayerAction.FADINGIN)
        {
            timer += Time.deltaTime;
            if (timer > fadeTime)
            {
                state = State_TeleportPlayerAction.FINISHED;
                Color tmp = fadeSprite.color;
                tmp.a = 0.0f;
                fadeSprite.color = tmp;
            }
            else
            { // fade to solid color
                Color tmp = fadeSprite.color;
                tmp.a = 1.0f - (timer / fadeTime);
                fadeSprite.color = tmp;
            }
        }
    }
}
