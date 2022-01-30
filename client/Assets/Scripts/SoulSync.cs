using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;

public class SoulSync : RealtimeComponent<SoulSyncModel>
{
    private Soul _soul;

    private void Awake()
    {
        _soul = GetComponent<Soul>();
    }

    protected override void OnRealtimeModelReplaced(SoulSyncModel previousModel, SoulSyncModel currentModel)
    {
        if (previousModel != null)
        {
            previousModel.neitherLiveDidChange -= NeitherLiveDidChange;
            previousModel.rightyLivesDidChange -= RightyLivesDidChange;
            previousModel.leftyLivesDidChange -= LeftyLivesDidChange;
            previousModel.bothLiveDidChange -= BothLiveDidChange;
        }

        if (currentModel != null)
        {
            if (currentModel.isFreshModel)
            {
                currentModel.neitherLive = _soul.neitherLive;
                currentModel.rightyLives = _soul.rightyLives;
                currentModel.leftyLives = _soul.leftyLives;
                currentModel.bothLive = _soul.bothLive;
            }

            UpdateNeitherLive();
            UpdateRightyLives();
            UpdateLeftyLives();
            UpdateBothLive();

            currentModel.neitherLiveDidChange += NeitherLiveDidChange;
            currentModel.rightyLivesDidChange += RightyLivesDidChange;
            currentModel.leftyLivesDidChange += LeftyLivesDidChange;
            currentModel.bothLiveDidChange += BothLiveDidChange;
        }
    }

    private void NeitherLiveDidChange(SoulSyncModel model, Vector2 value)
    {
        UpdateNeitherLive();
    }

    private void RightyLivesDidChange(SoulSyncModel model, Vector2 value)
    {
        UpdateRightyLives();
    }

    private void LeftyLivesDidChange(SoulSyncModel model, Vector2 value)
    {
        UpdateLeftyLives();
    }

    private void BothLiveDidChange(SoulSyncModel model, Vector2 value)
    {
        UpdateBothLive();
    }

    private void UpdateNeitherLive()
    {
        _soul.neitherLive = model.neitherLive;
    }

    private void UpdateRightyLives()
    {
        _soul.rightyLives = model.rightyLives;
    }

    private void UpdateLeftyLives()
    {
        _soul.leftyLives = model.leftyLives;
    }

    private void UpdateBothLive()
    {
        _soul.bothLive = model.bothLive;
    }

    public void SetNeitherLive(Vector2 neitherLive)
    {
        model.neitherLive = neitherLive;
    }

    public void SetRightyLives(Vector2 rightyLives)
    {
        model.rightyLives = rightyLives;
    }

    public void SetLeftyLives(Vector2 leftyLives)
    {
        model.leftyLives = leftyLives;
    }

    public void SetBothLive(Vector2 bothLive)
    {
        model.bothLive = bothLive;
    }

}
