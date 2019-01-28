using System;
using UnityEngine;
using UnityEngine.UI;

/*
 * Utility methods that can be used anywhere in the code.
 */
public static class Utilities
{
    // Checks an instance of this class doesn't already exist and returns the object it was called on
    public static T GetAndEnforceSingleInstance<T>(this T me, T instance)
    {
        if (instance != null && !instance.Equals(me))
        {
            throw new Exception("Only one instance this class can exist.");
        }

        return me;
    }

    // Initializes every element of an array to the provided value
    public static T[] InitializeArray<T>(this T[] arr, T value)
    {
        for (int i = 0; i < arr.Length; ++i)
        {
            arr[i] = value;
        }

        return arr;
    }

    // Initializes every element of a 2D array to the provided value
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

    /*
     * Unity methods
     */

    // Creates a text component attached to the provided canvas
    public static GameObject CreateText(Transform canvas, float x, float y, string text, int fontSize, Color color)
    {
        GameObject go = new GameObject($"__text ({text})");
        go.transform.SetParent(canvas);

        RectTransform rectTransform = go.AddComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector2(x, y);

        Text textComponent = go.AddComponent<Text>();
        textComponent.text = text;
        textComponent.fontSize = fontSize;
        textComponent.color = color;
        textComponent.font = Font.CreateDynamicFontFromOSFont("Arial", 10); // TODO just use default font

        return go;
    }

    // Set the text component of a GameObject or it's children
    public static void SetText(this GameObject gameObject, string text)
    {
        Text textComponent = gameObject.GetComponent<Text>();

        if (!textComponent) textComponent = gameObject.GetComponentInChildren<Text>();

        if (textComponent) textComponent.text = text;
    }
}
