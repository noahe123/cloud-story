using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using Cinemachine;
using DG.Tweening;
using System;

public class StasisCharacter : MonoBehaviour
{

    //movement variables

    [Header("Collision")]
    public LayerMask layerMask;

    private MovementInput input;
    public Animator anim;

    [Space]

    [Header("Aim and Zoom")]
    public bool stasisAim;
    public CinemachineFreeLook thirdPersonCamera;
    public float zoomDuration = .3f;
    private float originalFOV;
    public float zoomFOV;
    private Vector3 originalCameraOffset;
    public Vector3 zoomCameraOffset;

    [Space]

    [Header("Target")]
    public Transform target;


    //color variables

    [Space]

    [Header("Colors")]
    public Color highligthedColor;
    public Color normalColor;
    public Color finalColor;


    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;

        input = GetComponent<MovementInput>();
        anim = GetComponent<Animator>();
        originalFOV = thirdPersonCamera.m_Lens.FieldOfView;
        originalCameraOffset = thirdPersonCamera.GetRig(1).GetCinemachineComponent<CinemachineComposer>().m_TrackedObjectOffset;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            StasisAim(true);
        }

        if (Input.GetMouseButtonUp(1))
        {
            StasisAim(false);
        }

        if (stasisAim)
        {
            //create a new raycast to determine distance from player
            RaycastHit hit;
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            //check if physics raycast is valid or not
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
            {

                if (target != hit.transform)
                {

                    //check if emissioncolor is valid for the check

                    if (target != null && target.GetComponent<StasisObject>() != null)
                        target.GetComponent<Renderer>().material.SetColor("_EmissionColor", normalColor);

                    target = hit.transform;
                    if (target.GetComponent<StasisObject>() != null)
                    {

                        //determine if the stasis object is activated

                        if (!target.GetComponent<StasisObject>().activated)
                            target.GetComponent<Renderer>().material.SetColor("_EmissionColor", highligthedColor);

                        InterfaceAnimation.instance.Target(true);
                    }
                    else
                    {
                        InterfaceAnimation.instance.Target(false);
                    }
                }
            }
            else
            {
                if (target != null)
                {
                    //check the target stasis object for the proper emission color
                    if (target.GetComponent<StasisObject>() != null)
                        if (!target.GetComponent<StasisObject>().activated)
                            target.GetComponent<Renderer>().material.SetColor("_EmissionColor", normalColor);

                    target = null;
                    InterfaceAnimation.instance.Target(false);
                }
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (!stasisAim)
            {
                //trigger a slash animation if true
                anim.SetTrigger("slash");
                StartCoroutine(WaitFrame());
            }
            else
            {
                if (target != null && target.GetComponent<StasisObject>() != null)
                {
                    //determine the stasis state
                    bool stasis = target.GetComponent<StasisObject>().activated;
                    target.GetComponent<StasisObject>().SetStasis(!stasis);
                    StasisAim(false);
                }
            }
        }


        //
        RestartHotkey();
    }


    IEnumerator WaitFrame()
    {
        //try to find the end of the frame if it exists
        yield return new WaitForEndOfFrame();
        if (!anim.GetBool("attacking"))
            anim.SetBool("attacking", true);
    }

    void StasisAim(bool state)
    {
        stasisAim = state;
        float fov = state ? zoomFOV : originalFOV;
        Vector3 offset = state ? zoomCameraOffset : originalCameraOffset;
        float stasisEffect = state ? .4f : 0;

        //use the cinemachine virtual camera to track the color effects
        CinemachineComposer composer = thirdPersonCamera.GetRig(1).GetCinemachineComponent<CinemachineComposer>();
        DOVirtual.Float(thirdPersonCamera.m_Lens.FieldOfView, fov, zoomDuration, SetFieldOfView);
        DOVirtual.Float(composer.m_TrackedObjectOffset.x, offset.x, zoomDuration, SetCameraOffsetX);
        DOVirtual.Float(composer.m_TrackedObjectOffset.y, offset.y, zoomDuration, SetCameraOffsetY);

        //look for the stasis object in the scene
        StasisObject[] stasisObjects = FindObjectsOfType<StasisObject>();
        foreach (StasisObject obj in stasisObjects)
        {
            if (!obj.activated)
            {
                obj.GetComponent<Renderer>().material.SetColor("_EmissionColor", normalColor);
                obj.GetComponent<Renderer>().material.SetFloat("_StasisAmount", stasisEffect);
            }
        }

        InterfaceAnimation.instance.Show(state);

    }


    void SetFieldOfView(float x)
    {
        thirdPersonCamera.m_Lens.FieldOfView = x;
    }
    void SetCameraOffsetX(float x)
    {
        for (int i = 0; i < 3; i++)
        {
            thirdPersonCamera.GetRig(i).GetCinemachineComponent<CinemachineComposer>().m_TrackedObjectOffset.x = x;
        }
    }
    void SetCameraOffsetY(float y)
    {
        for (int i = 0; i < 3; i++)
        {
            thirdPersonCamera.GetRig(i).GetCinemachineComponent<CinemachineComposer>().m_TrackedObjectOffset.y = y;
        }
    }

    void RestartHotkey()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
        }
    }
}s