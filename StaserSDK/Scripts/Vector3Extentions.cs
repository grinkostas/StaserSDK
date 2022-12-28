using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class Vector3Extentions
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
}
