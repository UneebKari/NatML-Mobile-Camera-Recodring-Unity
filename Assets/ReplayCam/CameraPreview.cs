/* 
*   NatCorder
*   Copyright (c) 2020 Yusuf Olokoba
*/

namespace NatSuite.Examples.Components {

    using UnityEngine;
    using UnityEngine.Android;
    using UnityEngine.UI;
    using System.Collections;

	[RequireComponent(typeof(RawImage), typeof(AspectRatioFitter))]
    public class CameraPreview : MonoBehaviour {

        WebCamTexture cameraTexture;// { get; private set; }
		private RawImage rawImage;
		private AspectRatioFitter aspectFitter;
        int currentCamIndex = 0;

        IEnumerator Start () {
			rawImage = GetComponent<RawImage>();
			aspectFitter = GetComponent<AspectRatioFitter>();
            // Request camera permission
            if (Application.platform == RuntimePlatform.Android) {
                if (!Permission.HasUserAuthorizedPermission(Permission.Camera)) {
                    Permission.RequestUserPermission(Permission.Camera);
                    yield return new WaitUntil(() => Permission.HasUserAuthorizedPermission(Permission.Camera));
                }
            } else {
                yield return Application.RequestUserAuthorization(UserAuthorization.WebCam);
                if (!Application.HasUserAuthorization(UserAuthorization.WebCam))
                    yield break;
            }
            // Start the WebCamTexture
            cameraTexture = new WebCamTexture(null, 1280, 720, 30);
            cameraTexture.Play();
            yield return new WaitUntil(() => cameraTexture.width != 16 && cameraTexture.height != 16); // Workaround for weird bug on macOS
            // Setup preview shader with correct orientation
            rawImage.texture = cameraTexture;
            rawImage.material.SetFloat("_Rotation", cameraTexture.videoRotationAngle * Mathf.PI / 180f);
            rawImage.material.SetFloat("_Scale", cameraTexture.videoVerticallyMirrored ? -1 : 1);
            // Scale the preview panel
            if (cameraTexture.videoRotationAngle == 90 || cameraTexture.videoRotationAngle == 270)
                aspectFitter.aspectRatio = (float)cameraTexture.height / cameraTexture.width;
            else
                aspectFitter.aspectRatio = (float)cameraTexture.width / cameraTexture.height;
        }

        public void SwapCam_Clicked()
        {
            if (WebCamTexture.devices.Length > 0) { 

                Debug.Log("There are more than one cameras");
                Debug.Log(WebCamTexture.devices[currentCamIndex].name + "Camera Name Before");
                currentCamIndex += 1;
                currentCamIndex %= WebCamTexture.devices.Length;
                Debug.Log(WebCamTexture.devices[currentCamIndex].name + "Camera Name After");
                // if tex is not null:
                // stop the web cam
                // start the web cam

                if (cameraTexture != null)
                {
                    StopWebCam();
                    StartStopCam_Clicked();
                }
            }
        }
        private void StopWebCam()
        {
            rawImage.texture = null;
            cameraTexture.Stop();
            cameraTexture = null;
        }
        public void StartStopCam_Clicked()
        {
            if (cameraTexture != null) // Stop the camera
            {
                StopWebCam();
                //startStopText.text = "Start Camera";
            }
            else // Start the camera
            {
                WebCamDevice device = WebCamTexture.devices[currentCamIndex];
                cameraTexture = new WebCamTexture(device.name);
                rawImage.texture = cameraTexture;
                cameraTexture.Play();

                //uneeb
                StartCoroutine(ApplyAspectRatio());


                //startStopText.text = "Stop Camera";
            }
        }
        IEnumerator ApplyAspectRatio() {
            // Start the WebCamTexture
            //cameraTexture = new WebCamTexture(null, 1280, 720, 30);
            cameraTexture.Play();
            yield return new WaitUntil(() => cameraTexture.width != 16 && cameraTexture.height != 16); // Workaround for weird bug on macOS
            // Setup preview shader with correct orientation
            rawImage.texture = cameraTexture;
            rawImage.material.SetFloat("_Rotation", cameraTexture.videoRotationAngle * Mathf.PI / 180f);
            rawImage.material.SetFloat("_Scale", cameraTexture.videoVerticallyMirrored ? -1 : 1);
            // Scale the preview panel
            if (cameraTexture.videoRotationAngle == 90 || cameraTexture.videoRotationAngle == 270)
                aspectFitter.aspectRatio = (float)cameraTexture.height / cameraTexture.width;
            else
                aspectFitter.aspectRatio = (float)cameraTexture.width / cameraTexture.height;
        }
    }
}