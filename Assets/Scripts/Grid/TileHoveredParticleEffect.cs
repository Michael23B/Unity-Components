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
    private static bool active = false;

    public static void Start()
    {
        if (!active)
        {
            eventListener.Subscribe(Constants.EventName.TILEHOVERED);
            active = true;
        }
    }

    public static void Stop()
    {
        if (active)
        {
            if (particleEffect) Object.Destroy(particleEffect);
            currentTileId = -1;

            eventListener.Unsubscribe(Constants.EventName.TILEHOVERED);

            active = false;
        }
    }

    private static void TileHoverEvent(Tile tile)
    {
        if (tile.Id == currentTileId) return;

        currentTileId = tile.Id;

        // If we don't have a particle effect (the gameObject), create it and get the particle system
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
