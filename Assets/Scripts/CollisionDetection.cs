using UnityEngine;
using Cysharp.Threading.Tasks;
namespace Es.WaveformProvider.Sample
{

  /// <summary>
  /// 人と波の衝突判定
  /// </summary>
  public class CollisionDetection : MonoBehaviour
  {

    [SerializeField]
    private DetectHeight _detectHeight;
    [SerializeField]
    private float threshold;

    
    [SerializeField]
    private MyDebug debug;

    /// <summary>
    /// 衝突した
    /// </summary>
    /// <returns></returns>
    public async UniTask<bool> IsCollided(Vector2 screenPos)
    {
      var ray = Camera.main.ScreenPointToRay(screenPos);
      RaycastHit hitInfo;
      if (Physics.Raycast(ray, out hitInfo))
      {
        var waveObject = hitInfo.transform.GetComponent<WaveConductor>();
        if (waveObject != null)
        {
          Vector2 hitPointNormalized = hitInfo.textureCoord;
          Color height = await this._detectHeight.GetPixelFromNormalizedPos(hitPointNormalized.x, hitPointNormalized.y);
          if (height.r > this.threshold) return true;
        }
      }
      return false;
    }

  }
}