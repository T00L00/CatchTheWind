using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FieldParameters
{
    [Tooltip("Coefficient controlling general direction of wind.")]
    public float coeff = 1f;
    [Tooltip("Shift vector field along x-axis. May allow access to different parts of wind pattern.")]
    public float xShift = 0f;
    [Tooltip("Exponent controlling magnitude of wind gust along x-axis.")]
    public float xExponent = 1f;
    [Tooltip("Shift vector field along y-axis. May allow access to different parts of wind pattern.")]
    public float yShift = 0f;
    [Tooltip("Exponent controlling magnitude of wind gust along y-axis.")]
    public float yExponent = 0f;
    [Tooltip("Time exponent controls how fast wind direction changes.")]
    public float tExponent = 0f;
    [Tooltip("Shift vector field along y-axis. May allow access to different parts of wind pattern.")]
    public float constant = 0f;
}
