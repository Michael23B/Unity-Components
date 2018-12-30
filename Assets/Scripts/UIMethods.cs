using UnityEngine;

/*
 * A wrapper for the UI to call methods from other classes.
 * Mostly because UI method calls have limitations like not being able to call static methods and only taking 0 or 1 parameter.
 */
public class UIMethods : MonoBehaviour
{
    public void CreateAndRegisterUnitInMiddle(GameObject prefab)
    {
        Unit.CreateAndRegisterUnit(prefab, 2, 2);
    }

    public void StartTileHoverEventParticleEffects()
    {
        TileHoveredParticleEffect.Start();
    }

    public void StopTileHoverEventParticleEffects()
    {
        TileHoveredParticleEffect.Stop();
    }
}
