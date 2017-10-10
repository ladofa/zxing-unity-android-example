using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


/**
 * @brief 스캔 결과를 받아올 수 있는 이벤트
 */

public class ScanResultUpdatedHandler : UnityEvent<string>
{

}

/**
 * @brief 카메라로부터 이미지를 받아와서 QR 코드를 인식하고, 그 결과를 이벤트로 내보낸다.
 */
public class CameraController : MonoBehaviour
{
    public WebCamTexture mCamera = null;
    public GameObject plane;
	public ScanResultUpdatedHandler ScanResultUpdated = new ScanResultUpdatedHandler();

    /**
	 * @brief 시작 시, 카메라 로드
	 */
    void Start()
    {
        //start camera
        Debug.Log("Script has been started...");

        mCamera = new WebCamTexture();        
        //I think it starts independent thread to operate the camera.
        mCamera.Play();
    }

	/**
	 * @brief 이미지를 받아와서 QRCode를 해석한다.
	 */
	void DecodeQR(Color32[] c, int width, int height)
    {
        ZXing.QrCode.QRCodeReader reader = new ZXing.QrCode.QRCodeReader();

        try
        {
            //make source
            var source = new ZXing.Color32LuminanceSource(c, width, height);
            //in case of RGB raw

            var binarizer = new ZXing.Common.HybridBinarizer(source);
            var binBitmap = new ZXing.BinaryBitmap(binarizer);

            //raise exception if the reader couldn't recognize it
            string text = reader.decode(binBitmap).Text;

			if (ScanResultUpdated != null)
			{
				ScanResultUpdated.Invoke(text);
			}

			Debug.Log(text);
        }
        catch
        {
			if (ScanResultUpdated != null)
			{
				ScanResultUpdated.Invoke("");
			}
			Debug.Log("decoding failed.");
        }

    }

	void Update()
    {
        //reload current image
        DecodeQR(mCamera.GetPixels32(), mCamera.width, mCamera.height);
    }
}
