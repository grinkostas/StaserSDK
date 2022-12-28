using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class VectorExtentions
{
    public static bool SameDirection(this Vector3 forwardSource, Vector3 forwardTarget)
    {
        float dot =Vector3.Dot(forwardSource, forwardTarget);
        if(dot <= 0f)
            return false;
        return true;
    }

    public static bool SameDirection(this Transform sourceTransform, Transform targetTransform)
    {
        return SameDirection(sourceTransform.forward, targetTransform.forward);
    }

    public static float Random(this Vector2 vector)
    {
        return UnityEngine.Random.Range(vector.Min(), vector.Max());
    }

    public static float Min(this Vector2 vector2)
    {
        if (vector2.x > vector2.y)
            return vector2.y;
        return vector2.x;
    }
    
    public static float Max(this Vector2 vector2)
    {
        if (vector2.x < vector2.y)
            return vector2.y;
        return vector2.x;
    }

    public static Vector2 Abs(this Vector2 vector2)
    {
        return new Vector2(Mathf.Abs(vector2.x), Mathf.Abs(vector2.y));
    }

    public static float Sum(this Vector2 vector2)
    {
        return vector2.x + vector2.y;
    }

    public static Vector2 XZ(this Vector3 vector3)
    {
        return new Vector2(vector3.x, vector3.z);
    }
}
