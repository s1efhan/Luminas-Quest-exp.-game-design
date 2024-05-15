using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musicsc : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource yellow;
    public AudioSource red;
    public AudioSource blue;
    public AudioSource green;
    private Dictionary<string, AudioSource> AudioSourceMap = new Dictionary<string, AudioSource>();
    private string color = "yellow";


    private void Awake()
    {
        AudioSourceMap.Add("yellow", yellow);
        AudioSourceMap.Add("blue", blue);
        AudioSourceMap.Add("red", red);
        AudioSourceMap.Add("green", green);
        color = GetComponentInParent<PlayerController>().currentColor;
    }
    // Update is called once per frame
    void Update()
    {
        if(GetComponentInParent<PlayerController>().currentColor != color) {
            AudioSourceMap[color].Stop();
            color = GetComponentInParent<PlayerController>().currentColor;
            AudioSourceMap[color].Play();
        }
    }
}
