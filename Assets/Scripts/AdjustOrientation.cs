using ZXing;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using TMPro;

namespace Personal.Wayfinder
{
    public class AdjustOrientation : MonoBehaviour
    {
        [SerializeField]
        private ARCameraBackground aRCameraBackground;
        [SerializeField]
        private RenderTexture targetRenderTexture;

        private Texture2D cameraImageTexture;
        private IBarcodeReader reader = new BarcodeReader();

        private void Update()
        {
            Graphics.Blit(null, targetRenderTexture, aRCameraBackground.material);
            cameraImageTexture = new Texture2D(targetRenderTexture.width, targetRenderTexture.height, TextureFormat.RGBA32, false);
            Graphics.CopyTexture(targetRenderTexture, cameraImageTexture);

            var result = reader.Decode(cameraImageTexture.GetPixels32(), cameraImageTexture.width, cameraImageTexture.height);

            if(result != null)
            {
                Debug.Log(result.Text);
            }
        }
    }
}
