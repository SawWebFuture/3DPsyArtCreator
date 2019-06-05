using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FilterManager : MonoBehaviour {
    
    public GameObject mainObject;
    private HomePage home;
    private Texture image;

    // Use this for initialization
    void Start()
    {
        SetImage();
    }

    public void UpdateImage()
    {
        SetImage();
    }

    private void SetImage()
    {
        image = HomePage.Instance.GetImageByTexture();
        //set Objects with image
        Renderer[] objs = mainObject.GetComponentsInChildren<Renderer>();
        foreach (Renderer objectRenderer in objs)
        {
            objectRenderer.material.mainTexture = image;
        }
    }

}
