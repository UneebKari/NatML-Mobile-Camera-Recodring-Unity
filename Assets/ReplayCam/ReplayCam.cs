/* 
*   NatCorder
*   Copyright (c) 2020 Yusuf Olokoba
*/
using UnityEngine;
using NatML.VideoKit;

public class ReplayCam : MonoBehaviour
{

    public VideoKitRecorder vidRecorder;
    private void Start()
    {
        Debug.Log(vidRecorder.videoBitRate);
        vidRecorder.videoBitRate = 10000000/2;
        Debug.Log(vidRecorder.videoBitRate);

    }
    private void OnDestroy () {
            
    }

    public void StartRecording()
    {
        vidRecorder.StartRecording();
        Debug.Log("Strat");

    }

    public async void StopRecording()
    {
        vidRecorder.StopRecording();
        Debug.Log("Stop");
    }
}