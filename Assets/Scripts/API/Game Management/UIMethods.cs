﻿using UnityEngine;

/*
 * A wrapper for the UI to call methods from other classes.
 * Mostly because UI method calls have limitations like not being able to call static methods and only taking 0 or 1 parameter.
 *
 * TODO Eventually these methods will need to be separated into their own classes.
 */
public class UIMethods : MonoBehaviour
{
    public void CreateAndRegisterUnitInMiddle(GameObject prefab)
    {
        UnitCreation.CreateAndSetupUnit(prefab, 2, 2);
    }

    public void StartTileHoverEventParticleEffects()
    {
        TileHoveredParticleEffect.Start();
    }

    public void StopTileHoverEventParticleEffects()
    {
        TileHoveredParticleEffect.Stop();
    }

    public void StartUnitActiveParticleEffect()
    {
        UnitActiveParticleEffect.Start();
    }

    public void StopUnitActiveParticleEffect()
    {
        UnitActiveParticleEffect.Stop();
    }


    public void EndCurrentTurn()
    {
        GameComponents.TurnHandler.EndTurn();
    }
}
