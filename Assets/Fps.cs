using UnityEngine;

public class Fps : MonoBehaviour
{

    void　Awake()
    {
        Application.targetFrameRate = 120;
    }

}