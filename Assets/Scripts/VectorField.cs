using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum FieldType
{
    Constant,
	Linear,
    Spiral
};

/// <summary>
/// Serializable class to create different types of vector fields. Attached to vector field controller object in scene. 
/// Use xSlope, ySlope to tune size and direction of vectors. Tune exponents for weirdness.
/// </summary>
[System.Serializable]
public class VectorField
{
	public FieldType fieldType;
    public float xSlope = 1f;
    public float ySlope = 1f;
    public float xExponent = 1f;
    public float yExponent = 1f;

    public VectorField(VectorField vectorField)
    {
        fieldType = vectorField.fieldType;
    }

    /// <summary>
    /// Compute force of the wind current at game object position
    /// </summary>
    /// <param name="vectorField"></param>
    /// <param name="transform"></param>
    /// <returns>Force vector</returns>
	public static Vector2 WindCurrentForce(VectorField vectorField, Vector2 position)
    {
        Vector2 windForce = new Vector2 { x = 0f, y = 0f };
		switch(vectorField.fieldType)
        {
            case FieldType.Constant:
                windForce = CalculateConstant(vectorField.xSlope, vectorField.ySlope);
                break;
            case FieldType.Linear:
                windForce = CalculateLinear(position, vectorField.xSlope, vectorField.ySlope, vectorField.xExponent, vectorField.yExponent);
                break;
            case FieldType.Spiral:
                windForce = CalculateSpiral(position, vectorField.xSlope, vectorField.ySlope, vectorField.xExponent, vectorField.yExponent);
                break;
        }
        return windForce;
    }


# region Vector field functions to compute force based on field type

    // Constant
    private static Vector2 CalculateConstant(float xSlope, float ySlope)
    {
        return new Vector2
        {
            x = xSlope,
            y = ySlope
        };
    }

    // Linear
    private static Vector2 CalculateLinear(Vector2 pos, float xSlope, float ySlope, float xExponential, float yExponential)
    {
        return new Vector2
        {
            x = xSlope * Mathf.Pow(pos.x, xExponential),
            y = ySlope * Mathf.Pow(pos.y, yExponential)
        };
    }

    // Spiral
    private static Vector2 CalculateSpiral(Vector2 pos, float xSlope, float ySlope, float xExponential, float yExponential)
    {
        return new Vector2
        {
            x = xSlope * Mathf.Pow(pos.y, xExponential),
            y = ySlope * Mathf.Pow(pos.x, yExponential)
        };
    }

# endregion


}
