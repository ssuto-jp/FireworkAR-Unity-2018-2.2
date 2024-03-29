﻿using System;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Kudan.AR.Samples;

public class SaveViewController : MonoBehaviour
{
    [SerializeField] private RawImage photoImage;

    private void Start()
    {
        photoImage.texture = CaptureViewController.PhotoTexture;
    }

    public void SaveTapped()
    {
        StartCoroutine("SaveTappedCoroutine");
    }

    private IEnumerator SaveTappedCoroutine()
    {
        string timeStamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
        string fileName = timeStamp + ".png";
        NativeGallery.SaveImageToGallery(CaptureViewController.PhotoTexture, "FireworkAR", fileName);

        yield return new WaitForEndOfFrame();

        BackToScene();
    }

    public void ShareTapped()
    {
        StartCoroutine(ShareTappedCoroutine());
    }

    private IEnumerator ShareTappedCoroutine()
    {
        string timeStamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
        string fileName = timeStamp + ".png";
        string imagePath = Application.persistentDataPath + "/" + fileName;
        ScreenCapture.CaptureScreenshot(fileName);

        while (true)
        {
            if (File.Exists(imagePath)) break;
            yield return null;
        }

        var text = "#FireworkAR";
        var url = "";
        SocialConnector.SocialConnector.Share(text, url, imagePath);
    }

    public void BackTapped()
    {
        BackToScene();
    }

    private void BackToScene()
    {
        SceneManager.LoadScene("CaptureViewScene");
    }
}
