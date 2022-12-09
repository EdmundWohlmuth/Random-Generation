using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MeshTools : MonoBehaviour
{
    public MapGen mapGen;

    public Slider amplitude;
    public Slider frequency;
    public Slider waterLevel;
    public Slider XCoord;
    public Slider ZCoord;

    // Start is called before the first frame update
    void Start()
    {
        amplitude.maxValue = 20;
        frequency.maxValue = 0.5f;
        waterLevel.maxValue = 20;

        XCoord.maxValue = 200f;
        XCoord.minValue = -200f;
        ZCoord.maxValue = 200f;
        ZCoord.minValue = -200f;

        amplitude.value = mapGen.amplitude;
        frequency.value = mapGen.frequency;
        waterLevel.value = mapGen.waterLevel;
        XCoord.value = mapGen.offsetX;
        ZCoord.value = mapGen.offsetZ;
    }

    // Update is called once per frame
    void Update()
    {
        mapGen.amplitude = amplitude.value;
        mapGen.frequency = frequency.value;
        mapGen.waterLevel = waterLevel.value;
        mapGen.offsetX = XCoord.value;
        mapGen.offsetZ = ZCoord.value;
    }

    public void Exit()
    {
        Application.Quit(0);
    }
}
