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

    private Vector2[] _positions = new Vector2[10];
    private int[] _ids = new int[10];

    void Start()
    {
      this._oscManager.onGetId.AddListener(this._SetId);
      this._oscManager.onGetX.AddListener(this._SetX);
      this._oscManager.onGetY.AddListener(this._SetY);
    }

    async void Update()
    {
      Vector2 size = new Vector2(Screen.width, Screen.height);

      for (int i = 0; i < this._ids.Length; i++)
      {
        if (this._ids[i] == 0) continue;

        Vector2 position = this._positions[i];
        // NOTE: TouchOSCの値に合わせてむりやり値調整しています
        // NOTE: 0-1だっけ？
        Vector2 uvPos = new Vector2(position.y, position.x);
        Vector2 screenPos = uvPos * size;
        Debug.Log($"human{i}");

        if (await this._collisionDetection.IsCollided(screenPos))
        {
          this._personWaveInput.Input(uvPos);

          // ここで音用のOSCおくる
          int x = (int)Mathf.Ceil(uvPos.x * 8);
          int y = (int)Mathf.Ceil(uvPos.y * 8);
          int num = x + y * 8;
          this._oscManager.Send("/human", num);
        }
      }
    }

    private async void _SetId(int id, int val)
    {
      int index = id;
      Debug.Log(index);
      this._ids[index] = val;
      if(val != 0) Debug.Log(" not 0 !");
    }

    private async void _SetX(int id, float x)
    {
      int index = id;
      this._positions[index].y = x;
    }

    private async void _SetY(int id, float y)
    {
      int index = id;
      this._positions[index].x = y;
    }
  }
}