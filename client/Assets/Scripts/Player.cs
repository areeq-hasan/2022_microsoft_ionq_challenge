using UnityEngine;
using System.Collections;
using Normal.Realtime;

public class Player : MonoBehaviour
{

    // Camera
    public  Transform  cameraTarget;

    [SerializeField] float _speed = 4.0f;
    [SerializeField] float _jumpForce = 7.5f;

    private Animator _animator;
    private Rigidbody2D _rigidbody;
    [SerializeField] private Transform _character = default;

    private PlayerSensor _groundSensor;

    // Multiplayer
    private RealtimeView _realtimeView;

    // Synced Fields
    public bool grounded = false;
    public bool dead = false;
    public string side = "lefty";

    private PlayerSync _playerSync;

    void Awake()
    {
        // Set physics timestep to 60hz
        Time.fixedDeltaTime = 1.0f/60.0f;

        // Store a reference to the RealtimeView for easy access
        _realtimeView = GetComponent<RealtimeView>();
        
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _groundSensor = transform.Find("GroundSensor").GetComponent<PlayerSensor>();

        _playerSync = GetComponent<PlayerSync>();
    }

    // Use this for initialization
    void Start()
    {
        if (_realtimeView.isOwnedLocallyInHierarchy)
        {
            GetComponent<RealtimeTransform>().RequestOwnership();
            _character.GetComponent<RealtimeTransform>().RequestOwnership();
        }
    }

    // Update is called once per frame
    void Update()
    {

        //Check if character just landed on the ground

        _playerSync.SetGrounded(_groundSensor.State() || grounded);
        _animator.SetBool("Grounded", _groundSensor.State() || grounded);

        if (dead)
        {
            _playerSync.SetDead(true);
            _animator.SetTrigger("Death");
        }
        else
        {
            _playerSync.SetDead(false);
        }

        // Call LocalUpdate() only if this instance is owned by the local client
        if (_realtimeView.isOwnedLocallyInHierarchy && !dead)
        {
            // -- Handle input and movement --
            float inputX = Input.GetAxis("Horizontal");

            // Swap direction of sprite depending on walk direction
            if (inputX > 0)
                transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
            else if (inputX < 0)
                transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

            // Move
            _rigidbody.velocity = new Vector2(inputX * _speed, _rigidbody.velocity.y);

            //Set AirSpeed in animator
            _animator.SetFloat("AirSpeed", _rigidbody.velocity.y);

            // -- Handle Animations --
            //Death
            if (Input.GetKeyDown("e"))
            {
                if (!dead)
                    _animator.SetTrigger("Death");
                // else
                //     _animator.SetTrigger("Recover");

                _playerSync.SetDead(true);
            }

            //Hurt
            else if (Input.GetKeyDown("q"))
                _animator.SetTrigger("Hurt");

            //Attack
            else if (Input.GetMouseButtonDown(0))
            {
                _animator.SetTrigger("Attack");
            }

            // //Change between idle and combat idle
            // else if (Input.GetKeyDown("f"))
            //     _isCombatIdle = !_isCombatIdle;

            //Jump
            else if (Input.GetKeyDown("space") && grounded)
            {
                _animator.SetTrigger("Jump");
                _playerSync.SetGrounded(false);
                _animator.SetBool("Grounded", grounded);
                _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _jumpForce);
                _groundSensor.Disable(0.2f);
            }

            //Run
            else if (Mathf.Abs(inputX) > Mathf.Epsilon)
                _animator.SetInteger("AnimState", 2);

            // //Combat Idle
            // else if (_isCombatIdle)
            //     _animator.SetInteger("AnimState", 1);

            //Idle
            else
                _animator.SetInteger("AnimState", 0);
        }
    }
}