using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SeedOfLife : MonoBehaviour {

    public GameObject mainObject;
    public GameObject center;
    public Slider opacity;
    public Slider rotatex;
    public Slider rotatez;
    public Text tileCountText;
    public Text toggleText;
    public Toggle centerToggle;

    private Color color;
    private Renderer[] renderers;
    private float tileCount = 5;
    

    private void Start()
    {
        renderers = mainObject.GetComponentsInChildren<Renderer>();
        HandleTiles(true);
        SetCenterToggle();
    }

    private void SetCenterToggle()
    {
        centerToggle.onValueChanged.AddListener(delegate {
            CenterToggle(centerToggle);
        });
        toggleText.text = "Active";
    }

    private void Update()
    {
        foreach (Renderer objectRenderer in renderers)
        {
            color = objectRenderer.material.GetColor("_TintColor");
            color.a = opacity.value;
            objectRenderer.material.SetColor("_TintColor", color);
        }
        mainObject.transform.rotation = Quaternion.Euler(new Vector3(rotatex.value, 0, rotatez.value));
    }

    public void HandleTiles(bool dir)
    {
        if (dir && tileCount < 11)
        {
            tileCount++;
        } else if(!dir && tileCount > 0)
        {
            tileCount--;
        }
        foreach (Renderer tile in renderers)
        {
            tile.material.SetTextureScale("_MainTex", new Vector2(tileCount, tileCount));
        }
        tileCountText.text = "Count: " + tileCount;
    }

    public void CenterToggle(Toggle change)
    {
        center.SetActive(true);
        string message = "Active";
        if (!centerToggle.isOn)
        {
            center.SetActive(false);
            message = "Deactive";
        }
        toggleText.text = message; 
    }

}
