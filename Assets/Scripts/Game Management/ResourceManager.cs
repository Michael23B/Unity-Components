using System;
using System.Collections.Generic;
using UnityEngine;

/*
 * Loads resources into arrays based on their folder/type for easy access.
 */
public static class ResourceManager
{
    private static readonly Dictionary<Constants.ParticleEffectType, GameObject> particleEffectDictionary;
    private static readonly Dictionary<Constants.UnitPrefabType, GameObject> unitPrefabDictionary;

    static ResourceManager()
    {
        // Load particle effects dictionary
        GameObject[] particleEffects = Resources.LoadAll<GameObject>("ParticleEffects");
        particleEffectDictionary = new Dictionary<Constants.ParticleEffectType, GameObject>(particleEffects.Length);

        foreach (GameObject particleEffect in particleEffects)
        {
            particleEffectDictionary.Add(particleEffect.GetComponent<ParticleEffectTypeIdentifier>().ParticleEffectType, particleEffect);
        }

        // Load unit dictionary
        GameObject[] units = Resources.LoadAll<GameObject>("Units");
        unitPrefabDictionary = new Dictionary<Constants.UnitPrefabType, GameObject>(units.Length);

        foreach (GameObject unit in units)
        {
            unitPrefabDictionary.Add(unit.GetComponent<Unit>().UnitPrefabType, unit);
        }
    }

    public static GameObject GetParticleEffect(Constants.ParticleEffectType particleEffect)
    {
        if (particleEffectDictionary.ContainsKey(particleEffect))
        {
            return particleEffectDictionary[particleEffect];
        }
        else
        {
            throw new Exception($"Tried to access a resource that doesnt exist: {particleEffect}");
        }
    }

    public static GameObject GetUnitPrefab(Constants.UnitPrefabType unit)
    {
        if (unitPrefabDictionary.ContainsKey(unit))
        {
            return unitPrefabDictionary[unit];
        }
        else
        {
            throw new Exception($"Tried to access a resource that doesnt exist: {unit}");
        }
    }
}