
using System.Security.Cryptography;
using System.Runtime.ExceptionServices;
using System.Globalization;
using System;
using Unity.MLAgents;

[Serializable]
public class EnvironmentParameter
{
    public float value;
    public string name;
    public EnvironmentParameter(float value)
    {
        this.value = value;
    }

    public float GetValue()
    {
        if (name == null || name == "")
        {
            return value;
        }

        return Academy.Instance.EnvironmentParameters.GetWithDefault(name, value);
    }

    public static implicit operator EnvironmentParameter(float value)
    {
        return new EnvironmentParameter(value);
    }

    public static implicit operator float(EnvironmentParameter ep)
    {
        return ep.GetValue();
    }
}