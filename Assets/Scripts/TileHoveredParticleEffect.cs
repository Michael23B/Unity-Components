using System;
using UnityEngine;
using Object = UnityEngine.Object;

/*
 * When the TileHovered event is fired, moves a particle effect to that tile.
 * Can be activated and deactivated using Start() and Stop().
 */
public static class TileHoveredParticleEffect
{
    private static readonly Action<object, EventArgs> eventListener = (sender, e) => TileHoverEvent(((TileEventArgs)e).Tile);
    private static readonly GameObject particleEffectPrefab = ResourceManager.GetResource(1);
    private static GameObject particleEffect;
    private static ParticleSystem particleSystem;
    private static int currentTileId = -1;

    public static void Start()
    {
        eventListener.Subscribe(Constants.EventName.TILEHOVERED);
    }

    public static void Stop()
    {
        if (particleEffect) Object.Destroy(particleEffect);
        currentTileId = -1;

        eventListener.Unsubscribe(Constants.EventName.TILEHOVERED);
    }

    private static void TileHoverEvent(Tile tile)
    {
        if (tile.Id == currentTileId) return;

        currentTileId = tile.Id;

        if (!particleEffect)
        {
            particleEffect = Object.Instantiate(particleEffectPrefab, tile.GetPositionWithOffset(), Quaternion.identity);
            particleSystem = particleEffect.GetComponentInChildren<ParticleSystem>();
        }

        particleSystem.Stop();
        particleSystem.Clear();
        particleEffect.transform.position = tile.GetPositionWithOffset();
        particleSystem.Play();
    }

    // TODO add a 'no tile hovered' or 'ground (the big green flat thingy) hovered' event so I can stop the effect when no tiles are hovered
}
