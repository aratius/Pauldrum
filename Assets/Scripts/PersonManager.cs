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
    [SerializeField]
    private CollisionDetection _collisionDetection;
    [SerializeField]
    private PersonWaveInput _personWaveInput;

    void Start()
    {
      this._oscManager.onGetPosition.AddListener(this.SetPosition);
    }

    /// <summary>
    /// Osc中身
    /// TODO: IDで識別して同時に何回もならないようにする処理
    /// </summary>
    public async void SetPosition(string id, Vector2 position)
    {
      await UniTask.WaitForFixedUpdate();

      Vector2 size = new Vector2(Screen.width, Screen.height);
      // Vector2 screenPos = position * size;
      // NOTE: TouchOSCの値に合わせてむりやり値調整しています
      Vector2 uvPos = new Vector2(position.y, position.x);
      Vector2 screenPos = uvPos * size;

      if(await this._collisionDetection.IsCollided(screenPos))
      {
        this._personWaveInput.Input(uvPos);
      }
    }
  }
}