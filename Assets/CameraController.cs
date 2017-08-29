using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public WebCamTexture mCamera = null;
    public GameObject plane;

    // Use this for initialization
    void Start()
    {
        //start camera
        Debug.Log("Script has been started...");
        plane = GameObject.FindWithTag("Player");

        mCamera = new WebCamTexture();
        //attach my WebCamCamera to specific texture
        plane.GetComponent<Renderer>().material.mainTexture = mCamera;
        //I think it starts independent thread to operate the camera.
        mCamera.Play();
    }

    void DecodeQR(Color32[] c, int width, int height)
    {
        ZXing.QrCode.QRCodeReader reader = new ZXing.QrCode.QRCodeReader();
        try
        {
            //make source
            var source = new ZXing.Color32LuminanceSource(c, width, height);
            //in case of RGB raw
            //var source = new ZXing.RGBLuminanceSource(rgb, width, height);
            //also you can use another ZXing.xxxLuminanceSource

            var binarizer = new ZXing.Common.HybridBinarizer(source);
            var binBitmap = new ZXing.BinaryBitmap(binarizer);

            //raise exception if the reader couldn't recognize it
            string text = reader.decode(binBitmap).Text;
            
            GameObject.Find("Text").GetComponent<UnityEngine.UI.Text>().text = text;
            Debug.Log(text);
        }
        catch
        {
            GameObject.Find("Text").GetComponent<UnityEngine.UI.Text>().text = "decoding failed";
            Debug.Log("decoding failed.");
        }

    }

    // Update is called once per frame
    void Update()
    {
        //reload current image
        DecodeQR(mCamera.GetPixels32(), mCamera.width, mCamera.height);
    }
}
