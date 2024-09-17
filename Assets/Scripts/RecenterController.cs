using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using ZXing;

namespace Personal.Wayfinder
{
    public class RecenterController : MonoBehaviour
    {
        [SerializeField]
        private ARSession session;
        [SerializeField]
        private XROrigin sessionOrigin;
        [SerializeField]
        private ARCameraManager cameraManager;
        [SerializeField]
        private string startingPointName = "StartingPoint";
        [SerializeField]
        private List<Targets> navigationTargetObjects = new List<Targets>();

        private Texture2D cameraImageTexture;
        private IBarcodeReader reader = new BarcodeReader();
        private bool canAdjust = true;

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                SetQRCodeRecenterTarget(startingPointName);
            }
        }

        private void OnEnable()
        {
            cameraManager.frameReceived += OnCameraFrameReceived;
        }

        private void OnDisable()
        {
            cameraManager.frameReceived -= OnCameraFrameReceived;
        }

        private void OnCameraFrameReceived(ARCameraFrameEventArgs obj)
        {
            if(!cameraManager.TryAcquireLatestCpuImage(out XRCpuImage image))
            {
                return;
            }

            var conversionParams = new XRCpuImage.ConversionParams
            {
                inputRect = new RectInt(0, 0, image.width, image.height),
                outputDimensions = new Vector2Int(image.width / 2, image.height / 2),
                outputFormat = TextureFormat.RGBA32,
                transformation = XRCpuImage.Transformation.MirrorY
            };

            int size = image.GetConvertedDataSize(conversionParams);
            var buffer = new NativeArray<byte>(size, Allocator.Temp);
            image.Convert(conversionParams, buffer);
            image.Dispose();

            cameraImageTexture = new Texture2D(
                conversionParams.outputDimensions.x,
                conversionParams.outputDimensions.y,
                conversionParams.outputFormat,
                false);

            cameraImageTexture.LoadRawTextureData(buffer);
            cameraImageTexture.Apply();

            buffer.Dispose();

            var result = reader.Decode(cameraImageTexture.GetPixels32(), cameraImageTexture.width, cameraImageTexture.height);

            if(result != null)
            {
                SetQRCodeRecenterTarget(result.Text);
            }
        }

        private void SetQRCodeRecenterTarget(string v)
        {
            Targets currentTarget = navigationTargetObjects.Find(x => x.name.Equals(v));
            if(currentTarget != null && canAdjust)
            {
                session.Reset();

                sessionOrigin.transform.position = currentTarget.targetPosition.transform.position;
                sessionOrigin.transform.rotation = currentTarget.targetPosition.transform.rotation;
                StartCoroutine(DelayToScanAgain());
            }
        }

        private IEnumerator DelayToScanAgain()
        {
            canAdjust = false;
            yield return new WaitForSeconds(2);
            canAdjust = true;
        }
    }
}
