using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum FieldType
{
    Linear,
    SinTLinear,
    SinXSinY,
    SinXCosY,
    CosXSinY,
    CosXCosY
};

/// <summary>
/// Serializable class to create different types of vector fields by changing equation parameters. Attached to vector field controller object in scene. 
/// </summary>
[System.Serializable]
public class VectorField
{
    public FieldType fieldType;
    public FieldParameters xParams;
    public FieldParameters yParams;

    public VectorField(VectorField vectorField)
    {
        fieldType = vectorField.fieldType;
        xParams = vectorField.xParams;
        yParams = vectorField.yParams;
    }

    /// <summary>
    /// Compute force of the wind current at game object position
    /// </summary>
    /// <param name="vectorField"></param>
    /// <param name="transform"></param>
    /// <returns>Force vector</returns>
	public static Vector2 VectorAtPosition(VectorField vf, Vector2 pos)
    {
        Vector2 vector = new Vector2 { x = 0f, y = 0f };
        switch (vf.fieldType)
        {
            case FieldType.Linear:
                vector = CalculateLinear(vf, pos);
                break;
            case FieldType.SinTLinear:
                vector = CalculateSinTLinear(vf, pos);
                break;
            case FieldType.SinXSinY:
                vector = CalculateSinXSinY(vf, pos);
                break;
            case FieldType.SinXCosY:
                vector = CalculateSinXCosY(vf, pos);
                break;
            case FieldType.CosXSinY:
                vector = CalculateCosXSinY(vf, pos);
                break;
            case FieldType.CosXCosY:
                vector = CalculateCosXCosY(vf, pos);
                break;
        }
        return vector;
    }

    #region Vector Field Functions
    public static Vector2 CalculateLinear(VectorField vf, Vector2 pos)
    {
        FieldParameters xp = vf.xParams;
        FieldParameters yp = vf.yParams;
        return new Vector2
        {
            x = xp.coeff * Mathf.Pow(pos.x - xp.xShift, xp.xExponent) * Mathf.Pow(pos.y - xp.yShift, xp.yExponent) * Mathf.Pow(Time.time, xp.tExponent) + xp.constant,
            y = yp.coeff * Mathf.Pow(pos.x - yp.xShift, yp.xExponent) * Mathf.Pow(pos.y - yp.yShift, yp.yExponent) * Mathf.Pow(Time.time, yp.tExponent) + yp.constant
        };
    }

    // Sways back and forth
    public static Vector2 CalculateSinTLinear(VectorField vf, Vector2 pos)
    {
        FieldParameters xp = vf.xParams;
        FieldParameters yp = vf.yParams;
        return new Vector2
        {
            x = xp.coeff * Mathf.Sin( Mathf.Pow(Time.time, xp.tExponent) ) * Mathf.Pow(pos.x - xp.xShift, xp.xExponent) * Mathf.Pow(pos.y - xp.yShift, xp.yExponent) + xp.constant,
            y = yp.coeff * Mathf.Sin( Mathf.Pow(Time.time, yp.tExponent) ) * Mathf.Pow(pos.x - yp.xShift, yp.xExponent) * Mathf.Pow(pos.y - yp.yShift, yp.yExponent) + yp.constant
        };
    }

    public static Vector2 CalculateSinXSinY(VectorField vf, Vector2 pos)
    {
        FieldParameters xp = vf.xParams;
        FieldParameters yp = vf.yParams;
        return new Vector2
        {
            x = Mathf.Sin(xp.coeff * Mathf.Pow(pos.x - xp.xShift, xp.xExponent) * Mathf.Pow(pos.y - xp.yShift, xp.yExponent) * Mathf.Pow(Time.time, xp.tExponent)) + xp.constant,
            y = Mathf.Sin(yp.coeff * Mathf.Pow(pos.x - yp.xShift, yp.xExponent) * Mathf.Pow(pos.y - yp.yShift, yp.yExponent) * Mathf.Pow(Time.time, yp.tExponent)) + yp.constant
        };
    }

    public static Vector2 CalculateSinXCosY(VectorField vf, Vector2 pos)
    {
        FieldParameters xp = vf.xParams;
        FieldParameters yp = vf.yParams;
        return new Vector2
        {
            x = Mathf.Sin(xp.coeff * Mathf.Pow(pos.x - xp.xShift, xp.xExponent) * Mathf.Pow(pos.y - xp.yShift, xp.yExponent) * Mathf.Pow(Time.time, xp.tExponent)) + xp.constant,
            y = Mathf.Cos(yp.coeff * Mathf.Pow(pos.x - yp.xShift, yp.xExponent) * Mathf.Pow(pos.y - yp.yShift, yp.yExponent) * Mathf.Pow(Time.time, yp.tExponent)) + yp.constant
        };
    }

    public static Vector2 CalculateCosXSinY(VectorField vf, Vector2 pos)
    {
        FieldParameters xp = vf.xParams;
        FieldParameters yp = vf.yParams;
        return new Vector2
        {
            x = Mathf.Cos(xp.coeff * Mathf.Pow(pos.x - xp.xShift, xp.xExponent) * Mathf.Pow(pos.y - xp.yShift, xp.yExponent) * Mathf.Pow(Time.time, xp.tExponent)) + xp.constant,
            y = Mathf.Sin(yp.coeff * Mathf.Pow(pos.x - yp.xShift, yp.xExponent) * Mathf.Pow(pos.y - yp.yShift, yp.yExponent) * Mathf.Pow(Time.time, yp.tExponent)) + yp.constant
        };
    }

    public static Vector2 CalculateCosXCosY(VectorField vf, Vector2 pos)
    {
        FieldParameters xp = vf.xParams;
        FieldParameters yp = vf.yParams;
        return new Vector2
        {
            x = Mathf.Cos(xp.coeff * Mathf.Pow(pos.x - xp.xShift, xp.xExponent) * Mathf.Pow(pos.y - xp.yShift, xp.yExponent) * Mathf.Pow(Time.time, xp.tExponent)) + xp.constant,
            y = Mathf.Cos(yp.coeff * Mathf.Pow(pos.x - yp.xShift, yp.xExponent) * Mathf.Pow(pos.y - yp.yShift, yp.yExponent) * Mathf.Pow(Time.time, yp.tExponent)) + yp.constant
        };
    }

    #endregion
}
