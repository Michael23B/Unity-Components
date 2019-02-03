using System;
using System.Collections.Generic;

public static class UnitStatCollection
{
    static readonly Dictionary<string, UnitStats> unitStatsDictionary = new Dictionary<string, UnitStats>();

    static UnitStatCollection()
    {
        // TODO Read from a file to define these values

        unitStatsDictionary.Add("Player", new UnitStats {
            MaxHealth = 5,
            Health = 5,
            Team = 1
        });
        unitStatsDictionary.Add("Enemy", new UnitStats {
            MaxHealth = 5,
            Health = 5,
            Team = 2
        });
    }

    public static UnitStats GetUnitStats(string name)
    {
        if (unitStatsDictionary.ContainsKey(name))
        {
            return unitStatsDictionary[name];
        }
        else
        {
            throw new Exception($"Tried to access a unit stat that doesnt exist: {name}");
        }
    }
}
