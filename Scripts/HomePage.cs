using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomePage : MonoBehaviour {
    public static HomePage Instance { set; get; }
    public GameObject defaultImageContainer;
    public GameObject canvas;
    public Texture defaultTexture;
    public GameObject savedToGalleryMessage;

    private bool isSaving = false;
    private static Texture image;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        image = defaultTexture;
    }

    public void SaveImage()
    {
        if (!isSaving)
        {
            isSaving = true;
            canvas.SetActive(false);
            StartCoroutine(TakeScreenshotAndSave());
        }
    }

    public Texture GetImageByTexture()
    {
        return image;
    }

    public void RemoveDefaultImage()
    {
        if (defaultImageContainer)
        {
            Debug.Log("removeDefaultImage");
            defaultImageContainer.SetActive(false);
        }
    }

    public void UploadImage()
    {
        if (NativeGallery.IsMediaPickerBusy())
            return;
        PickImage(512);
    }

    public void UploadVideo()
    {
        if (NativeGallery.IsMediaPickerBusy())
            return;
        PickVideo();
    }

    private IEnumerator TakeScreenshotAndSave()
    {
        yield return new WaitForEndOfFrame();

        Texture2D ss = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        ss.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        ss.Apply();

        // Save the screenshot to Gallery/Photos
        Debug.Log("Permission result: " + NativeGallery.SaveImageToGallery(ss, "3DPsyArtCreatorCreations", "My_3D_Psy_img_{0}.png"));
        
        // To avoid memory leaks
        Destroy(ss);
        canvas.SetActive(true);
        GameObject thisSavedMessage = Instantiate(savedToGalleryMessage, canvas.transform);
        StartCoroutine(RemoveSave(thisSavedMessage));
        
    }

    IEnumerator RemoveSave(GameObject message)
    {
        yield return new WaitForSeconds(2);
        Destroy(message);
        isSaving = false;
    }
    

    private void PickImage(int maxSize)
    {
        
        NativeGallery.Permission permission = NativeGallery.GetImageFromGallery((path) =>
        {
            if (path != null)
            {
                // Create Texture from selected image
                Texture texture = NativeGallery.LoadImageAtPath(path, maxSize);
                if (texture == null)
                {
                    Debug.Log("Couldn't load texture from " + path);
                    return;
                }
                //set image on surface
                defaultImageContainer.GetComponent<Renderer>().material.mainTexture = texture;
                image = texture;
                UpdateImageInFilter();
            }
        }, "Select a PNG image", "image/png", maxSize);

        Debug.Log("Permission result: " + permission);
    }

    private void UpdateImageInFilter()
    {
        if(FindObjectOfType<FilterManager>() != null)
        {
            FindObjectOfType<FilterManager>().UpdateImage();
        }
    }

    private void PickVideo()
    {
        NativeGallery.Permission permission = NativeGallery.GetVideoFromGallery((path) =>
        {
            Debug.Log("Video path: " + path);
            if (path != null)
            {
                // Play the selected video
                Handheld.PlayFullScreenMovie("file://" + path);
            }
        }, "Select a video");

        Debug.Log("Permission result: " + permission);
    }
}
