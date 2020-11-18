using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State_ChangeCameraSize { STABLE, RESIZETOPLAYER, RESIZETOROOM }

public class ChangeCameraSize : MonoBehaviour
{
    public State_ChangeCameraSize state = State_ChangeCameraSize.STABLE;
    [SerializeField] private Collider2D area;
    [SerializeField] private float cameraSize;
    [SerializeField] private float resizeTime = 0.0f;
    public float resizeFraction = 0.0f;
    private float originalCameraSize;
    private GameObject camera;
    private GameObject player;


    void Start()
    {
        originalCameraSize = Camera.main.orthographicSize;
        camera = GameObject.FindGameObjectWithTag("MainCamera");
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State_ChangeCameraSize.STABLE:
                resizeFraction = 0.0f;
                camera.GetComponent<ActivatorSensor>().ScreenSizeChanged = false;
                break;
            case State_ChangeCameraSize.RESIZETOROOM:
                camera.GetComponent<CameraMove>().Target = gameObject;
                camera.GetComponent<ActivatorSensor>().ScreenSizeChanged = true;
                
                //var acti = camera.GetComponent<ActivatorSensor>();
                //acti.ScreenSizeChanged = true;
                if (resizeTime > 0.0f)
                {
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
                camera.GetComponent<CameraMove>().Target = player;
                camera.GetComponent<ActivatorSensor>().ScreenSizeChanged = true;
                //acti = camera.GetComponent<ActivatorSensor>();
                //acti.ScreenSizeChanged = true;
                if (resizeTime > 0.0f)
                {
                    if (resizeFraction < 1)
                    {
                        resizeFraction += Time.fixedDeltaTime / resizeTime;
                        Camera.main.orthographicSize = Mathf.SmoothStep(cameraSize, originalCameraSize, resizeFraction);
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
