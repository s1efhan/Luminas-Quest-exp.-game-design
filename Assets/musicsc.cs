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
    public string color = "yellow";
    private string oldcolor = "yellow";


    private void Awake()
    {
        AudioSourceMap.Add("yellow", yellow);
        AudioSourceMap.Add("blue", blue);
        AudioSourceMap.Add("red", red);
        AudioSourceMap.Add("green", green);
    }
    // Update is called once per frame
    void Update()
    {
        if(oldcolor != color)
        {
            AudioSourceMap[oldcolor].Stop();
            oldcolor= color;
            AudioSourceMap[color].Play();
        }
    }
}
