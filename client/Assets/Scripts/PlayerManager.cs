using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;
using Normal.Realtime;

public class PlayerManager : MonoBehaviour {
    [SerializeField] private GameObject _camera = default;
    [SerializeField] private Text _sideText = default;

    private Realtime _realtime;

    private void Awake() {
        // Get the Realtime component on this game object
        _realtime = GetComponent<Realtime>();

        // Notify us when Realtime successfully connects to the room
        _realtime.didConnectToRoom += DidConnectToRoom;
    }

    private void DidConnectToRoom(Realtime realtime) {
        // Instantiate the Player for this client once we've successfully connected to the room

        GameObject playerGameObject = Realtime.Instantiate(              prefabName: "Player",  // Prefab name
                                                                      ownedByClient: true,      // Make sure the RealtimeView on this prefab is owned by this client
                                                           preventOwnershipTakeover: true,      // Prevent other clients from calling RequestOwnership() on the root RealtimeView.
                                                                        useInstance: realtime); // Use the instance of Realtime that fired the didConnectToRoom event.

        // Get a reference to the player
        Player player = playerGameObject.GetComponent<Player>();
        GameObject[] playerGameObjects = GameObject.FindGameObjectsWithTag("Player");
        player.side = playerGameObjects.Length == 1 ? "lefty" : "righty";
        _sideText.text = "you are " + player.side + ".";
        
        // Get the constraint used to position the camera behind the player
        ParentConstraint cameraConstraint = _camera.GetComponent<ParentConstraint>();
        
        // Add the camera target so the camera follows it
        ConstraintSource constraintSource = new ConstraintSource { sourceTransform = player.cameraTarget, weight = 1.0f };
        int constraintIndex = cameraConstraint.AddSource(constraintSource);

        // Set the camera offset so it acts like a third-person camera.
        cameraConstraint.SetTranslationOffset(constraintIndex, new Vector3( 0.0f,  1.0f, -4.0f));
        cameraConstraint.SetRotationOffset   (constraintIndex, new Vector3(15.0f,  0.0f,  0.0f));
    }
}