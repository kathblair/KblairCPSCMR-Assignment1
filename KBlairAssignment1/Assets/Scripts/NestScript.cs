using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NestScript : MonoBehaviour
{
    public Text accelText;
    public Text micText;
    public float accelxVal;
    public float accelyVal;
    public float accelzVal;
    public float micLev;
    // Start is called before the first frame update
    void Start()
    {
        print("started nestscript");
        accelxVal = 0;
        accelyVal = 0;
        accelzVal = 0;
        SetAccelText();
        SetMicText();

    }

    // Update is called once per frame
    void Update()
    {
        accelxVal = Input.acceleration.x;
        accelyVal = Input.acceleration.y;
        accelzVal = Input.acceleration.z;
        SetAccelText();
        SetMicText();
        print(Microphone.devices);
        //accelText.text = "Accelerometer: x[" + Input.acceleration.x.ToString();
    }

    void SetAccelText()
    {
        accelText.text = "Accelerometer: x[" + accelxVal.ToString() + "], y[" + accelyVal.ToString() + "], z[" + accelzVal.ToString() + "]";
    }

    void SetMicText()
    {
        float db = 20 * Mathf.Log10(Mathf.Abs(MicInput.MicLoudness));
        micText.text = "Mic level: " + MicInput.MicLoudness;
    }
}

//This code is thanks to user atomtwist at https://forum.unity.com/threads/check-current-microphone-input-volume.133501/
public class MicInput : MonoBehaviour
{

    public static float MicLoudness;

    private string _device;

    //mic initialization
    void InitMic()
    {
        if (_device == null) _device = Microphone.devices[0];
        _clipRecord = Microphone.Start(_device, true, 999, 44100);
    }

    void StopMicrophone()
    {
        Microphone.End(_device);
    }

    //may need to get rid of this
    AudioClip _clipRecord;
    int _sampleWindow = 128;

    //get data from microphone into audioclip
    float LevelMax()
    {
        float levelMax = 0;
        float[] waveData = new float[_sampleWindow];
        int micPosition = Microphone.GetPosition(null) - (_sampleWindow + 1); // null means the first microphone
        if (micPosition < 0) return 0;
        _clipRecord.GetData(waveData, micPosition);
        // Getting a peak on the last 128 samples
        for (int i = 0; i < _sampleWindow; i++)
        {
            float wavePeak = waveData[i] * waveData[i];
            if (levelMax < wavePeak)
            {
                levelMax = wavePeak;
            }
        }
        return levelMax;
    }



    void Update()
    {
        // levelMax equals to the highest normalized value power 2, a small number because < 1
        // pass the value to a static var so we can access it from anywhere
        MicLoudness = LevelMax();
    }

    bool _isInitialized;
    // start mic when scene starts
    void OnEnable()
    {
        InitMic();
        _isInitialized = true;
    }

    //stop mic when loading a new level or quit application
    void OnDisable()
    {
        StopMicrophone();
    }

    void OnDestroy()
    {
        StopMicrophone();
    }


    // make sure the mic gets started & stopped when application gets focused
    void OnApplicationFocus(bool focus)
    {
        if (focus)
        {
            //Debug.Log("Focus");

            if (!_isInitialized)
            {
                //Debug.Log("Init Mic");
                InitMic();
                _isInitialized = true;
            }
        }
        if (!focus)
        {
            //Debug.Log("Pause");
            StopMicrophone();
            //Debug.Log("Stop Mic");
            _isInitialized = false;

        }
    }
}
