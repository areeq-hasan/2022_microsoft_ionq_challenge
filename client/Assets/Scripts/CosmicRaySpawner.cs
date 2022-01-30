using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;

public class CosmicRaySpawner : MonoBehaviour
{

    public float spawnTime = 5f;

    private GameObject _player;
    private GameObject _cosmicRay;

    private Realtime _realtime;

    private void Awake() {
        // Get the Realtime component on this game object
        _realtime = GetComponent<Realtime>();

        // Notify us when Realtime successfully connects to the room
        _realtime.didConnectToRoom += DidConnectToRoom;
    } 

    private void DidConnectToRoom(Realtime realtime) {
        InvokeRepeating ("SpawnCosmicRay", spawnTime, spawnTime);
    }

    void SpawnCosmicRay()
    {
        // Instantiate the Player for this client once we've successfully connected to the room
        GameObject _cosmicRay = Realtime.Instantiate(              prefabName: "Cosmic Ray",   // Prefab name
                                                                         ownedByClient: true,      // Make sure the RealtimeView on this prefab is owned by this client
                                                              preventOwnershipTakeover: true,      // Prevent other clients from calling RequestOwnership() on the root RealtimeView.
                                                                           useInstance: _realtime);  // Use the instance of Realtime that fired the didConnectToRoom event.
    }
}