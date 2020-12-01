using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State_ChangeCameraSize { STABLE, RESIZETOPLAYER, RESIZETOROOM }

public class ChangeCameraSize : MonoBehaviour
{
    private State_ChangeCameraSize state = State_ChangeCameraSize.STABLE;
    [SerializeField] private Collider2D area;
    [SerializeField] private float cameraSize;
    [SerializeField] private float resizeTime = 0.0f;
    private float resizeFraction = 0.0f;
    private float originalCameraSize;
    private float originalCameraSmoothTime;
    private GameObject camera;
    private GameObject player;
    private CameraMove cameraMove;
    private ActivatorSensor actiSen;


    void Start()
    {
        originalCameraSize = Camera.main.orthographicSize;
        camera = GameObject.FindGameObjectWithTag("MainCamera");
        cameraMove = camera.GetComponent<CameraMove>();
        actiSen = camera.GetComponent<ActivatorSensor>();
        originalCameraSmoothTime = cameraMove.SmootTime;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        switch (state)
        {
            case State_ChangeCameraSize.STABLE:
                resizeFraction = 0.0f;
                actiSen.ScreenSizeChanged = false;
                break;
            case State_ChangeCameraSize.RESIZETOROOM:
                cameraMove.Target = gameObject;
                actiSen.ScreenSizeChanged = true;
                //var acti = camera.GetComponent<ActivatorSensor>();
                //acti.ScreenSizeChanged = true;
                if (resizeTime > 0.0f)
                {
                    cameraMove.SmootTime = resizeTime;
                    if (resizeFraction < 1)
                    {
                        resizeFraction += Time.fixedDeltaTime / resizeTime;
                        Camera.main.orthographicSize = Mathf.SmoothStep(originalCameraSize, cameraSize, resizeFraction);
                    }
                    else
                    {
                        state = State_ChangeCameraSize.STABLE;
                    }
                }
                else
                {
                    Camera.main.orthographicSize = cameraSize;
                    state = State_ChangeCameraSize.STABLE;
                }
                break;
            case State_ChangeCameraSize.RESIZETOPLAYER:
                cameraMove.Target = player;
                actiSen.ScreenSizeChanged = true;
                cameraMove.StartShaking = false;
                //acti = camera.GetComponent<ActivatorSensor>();
                //acti.ScreenSizeChanged = true;
                if (resizeTime > 0.0f)
                {
                    cameraMove.SmootTime = originalCameraSmoothTime;
                    if (resizeFraction < 1)
                    {
                        resizeFraction += Time.fixedDeltaTime / resizeTime;
                        Camera.main.orthographicSize = Mathf.SmoothStep(Camera.main.orthographicSize, originalCameraSize, resizeFraction);
                    }
                    else
                    {
                        state = State_ChangeCameraSize.STABLE;
                    }
                }
                else
                {
                    Camera.main.orthographicSize = originalCameraSize;
                    state = State_ChangeCameraSize.STABLE;
                }
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (state == State_ChangeCameraSize.RESIZETOPLAYER) resizeFraction = 1.0f - resizeFraction;
            state = State_ChangeCameraSize.RESIZETOROOM;
        } 
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (state == State_ChangeCameraSize.RESIZETOROOM) resizeFraction = 1.0f - resizeFraction;
            state = State_ChangeCameraSize.RESIZETOPLAYER;
        }
    }
}
