using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class DetectHeight : MonoBehaviour
{

    [SerializeField]
    private RenderTexture waveMap;

    private Color[] pixels
    {
        get {
            var crrRt = RenderTexture.active;
            var texture = new Texture2D(waveMap.width, waveMap.height);
            texture.ReadPixels(new Rect(0, 0, waveMap.width, waveMap.height), 0, 0);
            texture.Apply();
            var colors = texture.GetPixels();
            RenderTexture.active = crrRt;
            return colors;
        }
    }

    private async UniTask<Color> _GetPixel(int x, int y)
    {
        var crrRt = RenderTexture.active;
        RenderTexture.active = waveMap;
        var texture = new Texture2D(waveMap.width, waveMap.height);

        await UniTask.WaitForEndOfFrame();

        texture.ReadPixels(new Rect(0, 0, waveMap.width, waveMap.height), 0, 0);
        texture.Apply();
        var color = texture.GetPixel(x, y);
        RenderTexture.active = crrRt;
        return color;
    }

    // Start is called before the first frame update
    void Start()
    {
        this._Calc();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private async void _Calc()
    {
        while(true)
        {
            await UniTask.Delay(100);
            var res = await this._GetPixel(1, 1);
            Debug.Log(res);
        }
    }
}
