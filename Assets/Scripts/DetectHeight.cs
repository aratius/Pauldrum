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

  
    [SerializeField]
    private MyDebug debug;

  public async UniTask<Color> GetPixel(int x, int y)
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

  public async UniTask<Color> GetPixelFromNormalizedPos(float x, float y)
  {
    var crrRt = RenderTexture.active;
    RenderTexture.active = waveMap;
    var texture = new Texture2D(waveMap.width, waveMap.height);

    // debug.Hoge($"{(int)(x * waveMap.width)} {(int)(y * waveMap.height)}");

    await UniTask.WaitForEndOfFrame();

    texture.ReadPixels(new Rect(0, 0, waveMap.width, waveMap.height), 0, 0);
    texture.Apply();
    var color = texture.GetPixel((int)(x * waveMap.width), (int)(y * waveMap.height));
    RenderTexture.active = crrRt;
    return color;
  }

}
