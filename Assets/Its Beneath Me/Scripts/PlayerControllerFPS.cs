using UnityEngine;

    /// <summary> The Movement State Willow is in.</summary>
    public enum WillowMovementState
    {
        Idle,Walking,Running
    }

[RequireComponent(typeof(CharacterController))] public class PlayerControllerFPS : MonoBehaviour
{
    [SerializeField] private float walkingSpeed = 7.5f;
    [SerializeField] private float runningSpeed = 11.5f;
    [SerializeField] private CharacterController characterController;
    [SerializeField] private Vector3 moveDirection = Vector3.zero;
    [HideInInspector] public bool canMove = true;

    [Header("Time to throw a thing")] [SerializeField] private GameObject[] gameObjectsToThrow;
    [SerializeField] private Vector3 throwPointVector3;
    [SerializeField] private Quaternion rightThrowRotation;
    [SerializeField] private Quaternion leftThrowRotation;
    [SerializeField] private float myForce;
    [SerializeField] private float throwTimerMax = 2.0f;
    [SerializeField] private float throwTimer;

    [Header("Vector Stuff")] [SerializeField] private Direction facingDirection;
    [SerializeField] private Transform pivotPoint;


    [Header("Animator")]
    /// <summary> The Animator, Grabs on Awake - !!! (Make Sure it has the Willow Character Controller) !!! </summary>
    public Animator myAnim;
    /// <summary> The Movement State the player is in. This will change the movement speed of the player.</summary>
    public WillowMovementState myWillowMovementState = WillowMovementState.Idle;


    private float zPosition = 0f;
    private Vector3 movementOffSet;
    [SerializeField] private float gravity = 20f;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        myAnim = GetComponentInChildren<Animator>();
    }

    // These are all the scripts that change the player states.
#region Player States

    /// <summary> This is the non moving idle state.</summary>
    private void IdleState()
    {
        myAnim.SetBool("Idle", true);
        myAnim.SetBool("Walking", false);
        myAnim.SetBool("Running", false);
        myWillowMovementState = WillowMovementState.Idle;
    }

    /// <summary> This is the moving walking state.</summary>
    private void WalkingState()
    {
        myAnim.SetBool("Idle", false);
        myAnim.SetBool("Walking", true);
        myAnim.SetBool("Running", false);
        myWillowMovementState = WillowMovementState.Walking;
    }

    /// <summary> This is the moving running state.</summary>
    private void RunningState()
    {
        myAnim.SetBool("Idle", false);
        myAnim.SetBool("Walking", false);
        myAnim.SetBool("Running", true);
        myWillowMovementState = WillowMovementState.Running;
    }

#endregion

    void Update()
    {

    #region Z Offset Alignment
        // This should fix the player from moving on the z direction.
        if(transform.position.z != zPosition)
            movementOffSet.z = (zPosition - transform.position.z) * 0.05f;

        characterController.Move(movementOffSet);
    #endregion

    #region Throw Item
        if(throwTimer < throwTimerMax)
            throwTimer += Time.deltaTime;
        else
        {
            // Throw an item in the direction you are facing.
            if(Input.GetButtonDown("Jump"))
            {
                if(facingDirection == Direction.Right)
                {
                    throwTimer = 0;
                    CmdThrow(throwPointVector3, rightThrowRotation, Direction.Right);
                }

                if(facingDirection == Direction.Left)
                {
                    throwTimer = 0;
                    CmdThrow(new Vector3(-throwPointVector3.x, throwPointVector3.y, throwPointVector3.z), leftThrowRotation, Direction.Left);
                }
            }
        }
    #endregion

    #region Move Player
        // We are grounded, so recalculate move direction based on axes.
        Vector3 right = transform.TransformDirection(Vector3.right);
        // Press Left Shift to run.
        bool isRunning = Input.GetKey(KeyCode.LeftShift);

        if(isRunning)
        {
            RunningState();
        }

        float curSpeedY = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Horizontal") : 0;

        moveDirection = (right * curSpeedY);

        // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
        // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
        // as an acceleration (ms^-2)f
        moveDirection.y -= gravity * Time.deltaTime;

        // Move the controller
        characterController.Move(moveDirection * Time.deltaTime);

        if(moveDirection.x > 0)
        {
            if(facingDirection == Direction.Left)
            {
                facingDirection = Direction.Right;
                pivotPoint.localRotation = Quaternion.Euler(0, 90, 0);
            }

            if(!isRunning)
                WalkingState();
        }

        if(moveDirection.x < 0)
        {
            if(facingDirection == Direction.Right)
            {
                pivotPoint.localRotation = Quaternion.Euler(0, -90, 0);
                facingDirection = Direction.Left;
            }

            if(!isRunning)
                WalkingState();
        }

        if(moveDirection.x == 0)
            IdleState();

    #endregion

    #region Quit on ESC
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
        }
    #endregion

    }

    /// <summary>
    /// Throws an object.
    /// </summary>
    /// <param name="_position"> Place to start throwing the Object. </param>
    /// <param name="_rotation"> Rotation of the throw. </param>
    private void CmdThrow(Vector3 _position, Quaternion _rotation, Direction _direction)
    {
        GameObject newThrowObject = Instantiate(gameObjectsToThrow[0], transform.localPosition + _position, _rotation);
        Rigidbody throwRigidbody = newThrowObject.GetComponent<Rigidbody>();

        throwRigidbody.velocity = characterController.velocity * 2f;

        if(_direction == Direction.Right)
        {
            throwRigidbody.AddForce(myForce, myForce * 0.2f, Random.Range(0, 10f), ForceMode.Impulse);
        }

        if(_direction == Direction.Left)
        {
            throwRigidbody.AddForce(-myForce, myForce * 0.2f, -Random.Range(0f, 10f), ForceMode.Impulse);
        }

        throwRigidbody.AddTorque(Random.Range(myForce, -myForce), Random.Range(myForce, -myForce), Random.Range(myForce, -myForce), ForceMode.Impulse);
        throwTimer = 0f;
    }

    private enum Direction
    {
        Right,
        Left
    }
}