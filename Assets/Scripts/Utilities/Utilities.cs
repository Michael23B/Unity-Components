using System;

public static class Utilities
{
    //Checks an instance of this class doesn't already exist and returns the object it was called on
    public static T GetAndEnforceSingleInstance<T>(this T me, T instance)
    {
        if (instance != null && !instance.Equals(me))
        {
            throw new Exception("Only one instance this class can exist.");
        }

        return me;
    }
}
