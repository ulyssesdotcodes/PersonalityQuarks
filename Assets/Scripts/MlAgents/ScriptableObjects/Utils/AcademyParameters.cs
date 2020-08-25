using Unity.MLAgents;
using UnityEngine;

static class AcademyParameters
{

    public static float FetchOrParse(Academy academy, string key)
    {
        float prop = academy.EnvironmentParameters.GetWithDefault(key, -1);
        if (prop == -1)
        {
            float.TryParse(key, out prop);
        }

        return prop;
    }

    public static float Update(Academy academy, string key, float current)
    {
        if (academy != null && academy.EnvironmentParameters != null)
        {
            return academy.EnvironmentParameters.GetWithDefault(key, current);
        }
        return current;
    }
}
