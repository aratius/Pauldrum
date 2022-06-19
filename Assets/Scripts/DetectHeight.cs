using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

/// <summary>
/// 高さをチェック
/// </summary>
public class DetectHeight : MonoBehaviour
{

    [SerializeField]
    private RenderTexture waveMap;

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
	if (Input.GetMouseButton(0))
			{
				Debug.Log(Input.mousePosition);
            }
    }

    private async void _Calc()
    {
        while(true)
        {
            await UniTask.Delay(100);
            var res = await this._GetPixel(1, 1);
            // Debug.Log(res);
        }
    }
}
