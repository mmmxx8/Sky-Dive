using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Collider2D))]
public class PlayerMovement : MonoBehaviour
{
    //int screenWidth = Screen.width;
    public static PlayerMovement Instance { get; private set;}
    float screenLeftEdge;
    float screenRightEdge;
    [SerializeField] float forceOnClick;
    Collider2D playerCollider;
    public event EventHandler OnNormalCloudHit;
    private void Awake()
    {
        screenLeftEdge = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;
        screenRightEdge = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;
        playerCollider = GetComponent<Collider2D>();
        Instance = this;
    }
    void Update()
    {

        //FOR TOUCH
        //    if (Touchscreen.current != null &&
        //Touchscreen.current.primaryTouch.press.isPressed)
        //    {
        //        Vector2 touchPosition = Touchscreen.current.primaryTouch.position.ReadValue();\

        //        if (touchPosition.x < screenWidth / 2) { applyforce(forceOnClick);}
        //        else { applyForce(-forceOnClick);}

        //    }

        if (Keyboard.current.dKey.isPressed)
        {
            ApplyForce(forceOnClick);
        }
        else if (Keyboard.current.aKey.isPressed)
        {
            ApplyForce(-forceOnClick);
        }

    }

    private void ApplyForce(float forceToApply)
    {
        if (playerCollider.bounds.max.x < screenRightEdge && forceToApply > 0)
        {
            transform.position += new Vector3(forceToApply*Time.deltaTime, 0, 0);
        }
        if (playerCollider.bounds.min.x > screenLeftEdge && forceToApply < 0)
        {
            transform.position += new Vector3(forceToApply*Time.deltaTime, 0, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("NormalCloud"))
        {
            OnNormalCloudHit?.Invoke(this, EventArgs.Empty);
        }
    }
}
