using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NatML.VideoKit;

public class OnSessionCompleted : MonoBehaviour
{
    public VideoKitRecorder _VideoKitRecorder;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void SaveVideo() {
        //Debug.Log(_VideoKitRecorder.);
        //save video to gallery
        //NativeGallery.Permission permission = NativeGallery.SaveVideoToGallery(path, "CameraTest", "testVideo.mp4", (success, path) => Debug.Log("Media save result: " + success + " " + path));

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
