using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;

public class CosmicRay : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    [SerializeField] private Transform _cosmicRay = default;
    private Rigidbody2D _player;

    // Multiplayer
    private RealtimeView _realtimeView;

    void Awake()
    {
        // Set physics timestep to 60hz
        Time.fixedDeltaTime = 1.0f/60.0f;

        // Store a reference to the RealtimeView for easy access
        _realtimeView = GetComponent<RealtimeView>();
        _rigidbody = GetComponent<Rigidbody2D>();

        foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
        {
            if (player.GetComponent<RealtimeView>().isOwnedLocallyInHierarchy) { 
                _player = GameObject.Find("Player(Clone)").GetComponent<Rigidbody2D>();
                break;
            }
        }

        
    }

    // Start is called before the first frame update
    void Start()
    {
        if (_realtimeView.isOwnedLocallyInHierarchy)
        {
            GetComponent<RealtimeTransform>().RequestOwnership();
            _cosmicRay.GetComponent<RealtimeTransform>().RequestOwnership();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_realtimeView.isOwnedLocallyInHierarchy)
        {
            Vector2 v = _player.position - _rigidbody.position;
            v.Normalize();
            _rigidbody.velocity = 2 * v;
            _rigidbody.rotation = Mathf.Atan(_rigidbody.velocity.y/_rigidbody.velocity.x) * 180/Mathf.PI;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (_realtimeView.isOwnedLocallyInHierarchy)
        {
            Realtime.Destroy(gameObject);
        }
    }
}
