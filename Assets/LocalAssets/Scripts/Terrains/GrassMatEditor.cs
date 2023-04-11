using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassMatEditor : MonoBehaviour
{
    public Texture2D texture;
    public Color color;

    public MaterialPropertyBlock SetColor(MaterialPropertyBlock prop)
    {
        prop.SetColor("_Color", color);
        return prop;
    }

    public MaterialPropertyBlock SetTex(MaterialPropertyBlock prop)
    {
        if (texture == null)
            return prop;

        prop.SetTexture("_Albedo", texture);
        return prop;
    }
}
