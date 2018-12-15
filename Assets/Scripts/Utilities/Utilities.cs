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

    //Initializes every element of an array to the provided value
    public static T[] InitializeArray<T>(this T[] arr, T value)
    {
        for (int i = 0; i < arr.Length; ++i)
        {
            arr[i] = value;
        }

        return arr;
    }

    //Initializes every element of a 2D array to the provided value
    public static T[,] Initialize2DArray<T>(this T[,] arr, T value)
    {
        for (int i = 0; i < arr.GetLength(0); ++i)
        {
            for (int j = 0; j < arr.GetLength(1); ++j)
            {
                arr[i, j] = value;
            }
        }

        return arr;
    }
}
