using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassRandomMove : MonoBehaviour
{
    public bool randomMove;
    [SerializeField] float _bendStrength = 5;
    [SerializeField] Vector2 _dirRandom = new Vector2(0, 1);
    [SerializeField] MeshRenderer[] _grassRender;
    [SerializeField] [Range(0f, 2)] float _timeScale;
    MaterialPropertyBlock _prop;

    private void Awake()
    {
        InitProps();
        InitGrassGroup();
    }

    private void Start()
    {
        SetGroupDirection();
    }

    float RandomFloat()
    {
        return Random.Range(-2.0f, 2.0f);
    }

    float RandomTimeScale()
    {
        return Random.Range(0f, 2);
    }

    void InitProps()
    {
        if (_prop == null)
            _prop = new MaterialPropertyBlock();
    }

    void InitGrassGroup()
    {
        _grassRender = new MeshRenderer[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).GetChild(0).tag.Equals("Grass"))
                _grassRender[i] = transform.GetChild(i).GetChild(0).GetComponent<MeshRenderer>();
        }
    }

    void SetGroupDirection()
    {
        foreach (MeshRenderer renderer in _grassRender)
        {
            var gme =  renderer.GetComponent<GrassMatEditor>();

            if (gme != null)
            {
                gme.SetTex(_prop);
                gme.SetColor(_prop);
            }

            SetDirection(renderer, _prop);
        }
    }

    void SetDirection(MeshRenderer _render, MaterialPropertyBlock prop)
    {
        if (_render == null)
            return;

        if (randomMove)
        {
            _dirRandom = new Vector2(RandomFloat(), RandomFloat());
            _timeScale = RandomTimeScale();
        }

        // _render.GetPropertyBlock(prop);
        prop.SetVector("Direction", _dirRandom);
        prop.SetFloat("TimeScale", _timeScale);

        _render.SetPropertyBlock(prop);
    }
}
