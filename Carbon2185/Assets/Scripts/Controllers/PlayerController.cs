using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{
    GameManager _GameManager;

    public LayerMask MovementMask;

    public Interactable focus;

    Camera CameraMain;
    PlayerMotor motor;

    public float maxSpeed = 10f;

    public enum MovementMode
    {
        Disabled,
        Realtime,
        TurnBased
    }

    public MovementMode movementMode;

    void Awake()
    {
        _GameManager = GameManager.Instance;
        _GameManager.OnGameStateChange += HandleOnGameStateChange;
    }

    public void HandleOnGameStateChange()
    {
        //What type of movement we are allowing the player to do
        switch (_GameManager.gameState)
        {
            case(GameState.Menu):
                movementMode = MovementMode.Disabled;
                Debug.Log("Movement Mode: Disabled");
            break;
            case(GameState.World):
                movementMode = MovementMode.Realtime;
                Debug.Log("Movement Mode: Realtime");
            break;
            case(GameState.Combat):
                movementMode = MovementMode.TurnBased;
                Debug.Log("Movement Mode: TurnBased");
                StopMovement();
            break;
            default:
                Debug.LogError("No movement type selected");
            break;                        
        }

        Debug.Log("MovementMode: " + movementMode);
    }

    // Start is called before the first frame update
    void Start()
    {
        CameraMain = Camera.main;
        motor = GetComponent<PlayerMotor>();
    }

    // Update is called once per frame
    void Update()
    {

        //if we're moving freely about the world. move and interact normally
        if (MovementMode.Realtime == movementMode)
        {
            if (Input.GetMouseButtonDown(0))
            {
                // Check if the mouse was clicked over a UI element
                if (EventSystem.current.IsPointerOverGameObject())
                {
                    Debug.Log("Clicked on the UI");
                    return;
                }

                Ray ray = CameraMain.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, 100, MovementMask))
                {
                    motor.MoveToPoint(hit.point);

                    RemoveFocus();
                }
            }

            if (Input.GetMouseButtonDown(1))
            {
                // Check if the mouse was clicked over a UI element
                if (EventSystem.current.IsPointerOverGameObject())
                {
                    Debug.Log("Clicked on the UI");
                    return;
                }

                Ray ray = CameraMain.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, 100))
                {
                    Interactable interactable = hit.collider.GetComponent<Interactable>();

                    if (interactable != null)
                    {
                        SetFocus(interactable);
                    }
                }
            }
        }
        //if we're in combat restrict movement to max move speed
        else if(MovementMode.TurnBased == movementMode)
        {
            if (Input.GetMouseButtonDown(0))
            {
                // Check if the mouse was clicked over a UI element
                if (EventSystem.current.IsPointerOverGameObject())
                {
                    Debug.Log("Clicked on the UI");
                    return;
                }

                Debug.Log("Can't move due to turn based");
            }
            
        }

    }

    void SetFocus (Interactable interactable)
    {
        if (interactable != focus)
        {
            if (focus != null)
            {
                focus.OnDefocused();
            }
            
            focus = interactable;
            motor.FollowTarget(interactable);
        }
        
        interactable.OnFocused(transform);        
    }

    public void StopMovement()
    {
        Debug.Log("Stopping Movement");
        motor.Stop();
    }

    void RemoveFocus()
    {
        if (focus != null)
        {
            focus.OnDefocused();
        }

        focus = null;
        motor.StopFollowingTarget();
    }
}
