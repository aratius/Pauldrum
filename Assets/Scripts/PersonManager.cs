using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

namespace Es.WaveformProvider.Sample
{
  public class PersonManager : MonoBehaviour
  {

    [SerializeField]
    private OscManager _oscManager;
    [SerializeField]
    private Texture2D waveform;
    [SerializeField, Range(0f, 1f)]
    private float waveScale = 0.05f;
    [SerializeField, Range(0f, 1f)]
    private float strength = 0.1f;
    [SerializeField]
    private DetectHeight _detectHeight;

    void Start()
    {
      this._oscManager.onGetPosition.AddListener(this.SetPosition);
    }

    void Update()
    {

    }

    /// <summary>
    /// Osc中身
    /// </summary>
    public async void SetPosition(string id, Vector2 position)
    {
      await UniTask.WaitForFixedUpdate();

      Vector2 size = new Vector2(Screen.width, Screen.height);
      // Vector2 screenPos = position * size;
      // NOTE: TouchOSCの値に合わせてむりやり値調整しています
      Vector2 screenPos = new Vector2(position.y, position.x) * size;

      var ray = Camera.main.ScreenPointToRay(screenPos);
      RaycastHit hitInfo;
      if (Physics.Raycast(ray, out hitInfo))
      {
        var waveObject = hitInfo.transform.GetComponent<WaveConductor>();
        if (waveObject != null)
          {
            // waveObject.Input(waveform, hitInfo, waveScale, strength);
            Vector2 hitPointNormalized = hitInfo.textureCoord2;
            Color height = await this._detectHeight.GetPixelFromNormalizedPos(hitPointNormalized.x, hitPointNormalized.y);
            Debug.Log(height);
            if(height.r > 0.5) waveObject.Input(waveform, hitInfo, waveScale, strength);
          }
      }
    }
  }
}