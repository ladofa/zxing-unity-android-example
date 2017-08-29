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
        //카메라를 시작합니다.
        Debug.Log("Script has been started...");
        plane = GameObject.FindWithTag("Player");

        mCamera = new WebCamTexture();
        //카메라 텍스쳐를 3D객체에 할당
        plane.GetComponent<Renderer>().material.mainTexture = mCamera;
        mCamera.Play();
    }

    void DecodeQR(Color32[] c, int width, int height)
    {
        ZXing.QrCode.QRCodeReader reader = new ZXing.QrCode.QRCodeReader();
        try
        {
            //소스 영상을 넣습니다.
            var source = new ZXing.Color32LuminanceSource(c, width, height);
            //RGB raw 영상일 경우
            //var source = new ZXing.RGBLuminanceSource(rgb, width, height);
            var binarizer = new ZXing.Common.HybridBinarizer(source);
            var binBitmap = new ZXing.BinaryBitmap(binarizer);

            //실패할 경우 여기서 익셉션 발생
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
        //매번 업데이트때마다 카메라로부터 영상을 불러옴
        DecodeQR(mCamera.GetPixels32(), mCamera.width, mCamera.height);
    }
}
