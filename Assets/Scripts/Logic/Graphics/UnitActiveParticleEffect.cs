﻿using System;
using UnityEngine;
using Object = UnityEngine.Object;

// TODO could create a base class for this and tilehovered particle effect.
// The start and stop method likely wouldnt change and the eventListener could be abstract.
public class UnitActiveParticleEffect
{
    private static readonly Action<object, EventArgs> eventListener = (sender, e) => UnitTurnStartedEvent(((TurnEventArgs)e).CurrentUnit);
    private static readonly GameObject particleEffectPrefab = ResourceManager.GetParticleEffect(Constants.ParticleEffectType.ActiveUnit);
    private static GameObject particleEffect;
    private static ParticleSystem particleSystem;
    private static bool active = false;

    public static void Start()
    {
        if (!active)
        {
            eventListener.Subscribe(Constants.EventName.UNITTURNSTARTED);
            active = true;
        }
    }

    public static void Stop()
    {
        if (active)
        {
            if (particleEffect) Object.Destroy(particleEffect);

            eventListener.Unsubscribe(Constants.EventName.UNITTURNSTARTED);

            active = false;
        }
    }

    private static void UnitTurnStartedEvent(Unit unit)
    {
        Tile currentTile = GameComponents.GridController.GetTile(unit);

        // If we don't have a particle effect (the gameObject), create it and get the particle system
        if (!particleEffect)
        {
            particleEffect = Object.Instantiate(particleEffectPrefab, currentTile.GetPositionWithOffset(), Quaternion.identity);
            particleSystem = particleEffect.GetComponentInChildren<ParticleSystem>();
        }

        particleSystem.Stop();
        particleSystem.Clear();
        particleEffect.transform.position = currentTile.GetPositionWithOffset();
        particleSystem.Play();
    }
}