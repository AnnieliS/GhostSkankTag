using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] float speed = 10.0f;
    [SerializeField] float rotationSpeed = 100.0f;
    [SerializeField] float currentSpeed = 0;
    Animator anim;
    bool isRunning = false;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveCharacter();
        CheckSpeed();

    }

    private void MoveCharacter()
    {
        float translation = Input.GetAxis("Vertical") * speed;
        float rotation = Input.GetAxis("Horizontal") * rotationSpeed;

        translation *= Time.deltaTime;
        currentSpeed *= Time.deltaTime;

        transform.Translate(0, 0, translation);
        currentSpeed = translation;

        transform.Rotate(0, rotation, 0);
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
