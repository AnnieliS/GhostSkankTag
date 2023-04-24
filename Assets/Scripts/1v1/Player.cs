using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] GameObject playerCamera;
    [SerializeField] float speed = 10.0f;
    [SerializeField] float rotationMoveSpeed = 100.0f;
    [SerializeField] float currentSpeed = 0;
    [SerializeField] float rotateCameraSpeed;
    Animator anim;
    Transform cameraTransform;
    bool isRunning = false;

    float xCamera;
    float yCamera;
    float xRotate = 0f;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        cameraTransform = playerCamera.GetComponent<Transform>();
        //hide mouse cursor
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        CameraControl();
        MoveCharacter();
        CheckSpeed();

    }

    private void CameraControl()
    {
        // get mouse movement
        xCamera = Input.GetAxis("Mouse X") * rotateCameraSpeed * Time.deltaTime;
        yCamera = Input.GetAxis("Mouse Y") * rotateCameraSpeed * Time.deltaTime;

        // assign and lock x rotation
        xRotate -= yCamera;
        xRotate = Mathf.Clamp(xRotate, 0f, 30f);

        // actually move some shit
        cameraTransform.localRotation = Quaternion.Euler(xRotate, 0f, 0f);
        // cameraTransform.Rotate(Vector3.right* transform.rotation.y);
        transform.Rotate(Vector3.up * xCamera);
    }

    private void MoveCharacter()
    {
        float translation = Input.GetAxis("Vertical") * speed;
        // float rotation = Input.GetAxis("Horizontal") * rotationMoveSpeed;

        translation *= Time.deltaTime;
        currentSpeed *= Time.deltaTime;

        transform.Translate(0, 0, translation);
        currentSpeed = translation;

        // transform.Rotate(0, rotation, 0);
    }

    private void CheckSpeed()
    {
        if (currentSpeed == 0f && isRunning)
        {
            anim.SetTrigger("isIdle");
            isRunning = false;
        }

        else if (currentSpeed != 0f && !isRunning)
        {
            anim.SetTrigger("isRunning");
            isRunning = true;
        }
    }
}
