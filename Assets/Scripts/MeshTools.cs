using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MeshTools : MonoBehaviour
{
    public MapGen mapGen;

    public Slider amplitude;
    public Slider frequency;

    // Start is called before the first frame update
    void Start()
    {
        amplitude.maxValue = 10;
        frequency.maxValue = 1;

        amplitude.value = mapGen.amplitude;
        frequency.value = mapGen.frequency;
    }

    // Update is called once per frame
    void Update()
    {
        mapGen.amplitude = amplitude.value;
        mapGen.frequency = frequency.value;
    }

    public void Exit()
    {
        Application.Quit(0);
    }
}
