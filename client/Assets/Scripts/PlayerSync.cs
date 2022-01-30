using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;

public class PlayerSync : RealtimeComponent<PlayerSyncModel>
{

    private Player _player;

    private void Awake() {
        _player = GetComponent<Player>();
    }

    protected override void OnRealtimeModelReplaced(PlayerSyncModel previousModel, PlayerSyncModel currentModel) {
        if (previousModel != null) {
            previousModel.sideDidChange -= SideDidChange;
            previousModel.groundedDidChange -= GroundedDidChange;
            previousModel.deadDidChange -= DeadDidChange;
        }
        
        if (currentModel != null) {
            if (currentModel.isFreshModel)
                currentModel.side = _player.side;
                currentModel.grounded = _player.grounded;
                currentModel.dead = _player.dead;
        
            UpdateSide();
            UpdateGrounded();
            UpdateDead();

            currentModel.sideDidChange += SideDidChange;
            currentModel.groundedDidChange += GroundedDidChange;
            currentModel.deadDidChange += DeadDidChange;
        }
    }

    private void SideDidChange(PlayerSyncModel model, string value) {
        UpdateSide();
    }

    private void GroundedDidChange(PlayerSyncModel model, bool value) {
        UpdateGrounded();
    }

    private void DeadDidChange(PlayerSyncModel model, bool value) {
        UpdateDead();
    }

    private void UpdateSide() {
        _player.side = model.side;
    }

    private void UpdateGrounded() {
        _player.grounded = model.grounded;
    }

    private void UpdateDead() {
        _player.dead = model.dead;
    }

    public void SetSide(string side) {
        model.side = side;
    }

    public void SetGrounded(bool grounded) {
        model.grounded = grounded;
    }

    public void SetDead(bool dead) {
        model.dead = dead;
    }
}
