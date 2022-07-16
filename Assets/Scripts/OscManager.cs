using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Events;
using OscJack;

public class OscManager : MonoBehaviour
{

  public UnityEvent<int, int> onGetId;
  public UnityEvent<int, float> onGetX;
  public UnityEvent<int, float> onGetY;

  [SerializeField]
  private int _outPort;

  [SerializeField]
  private string _host;

  [SerializeField]
  private int _inPort;

  private OscServer _server;
  private OscClient _client;

  void Awake()
  {
    this.onGetId = new UnityEvent<int, int>();
    this.onGetX = new UnityEvent<int, float>();
    this.onGetY = new UnityEvent<int, float>();
  }

  void Start()
  {
    this._server = new OscServer(this._inPort);
    this._server.MessageDispatcher.AddCallback("", this._onReceiveOsc);

    this._client = new OscClient(this._host, this._outPort);
  }

  void Update()
  {
    if(Input.GetKeyDown(KeyCode.O))
    {
      this.Send("/human", (int)Random.Range(1, 5));
    }
  }

  /// <summary>
  /// OSC受信したら
  /// </summary>
  /// <param name="address"></param>
  /// <param name="data"></param>
  private void _onReceiveOsc(string address, OscDataHandle data)
  {
    // Regex touch = new Regex("touch");
    // if (touch.IsMatch(address))
    // {
    //   Vector2 position = new Vector2(data.GetElementAsFloat(0), data.GetElementAsFloat(1));
    //   this.onGetPosition.Invoke(address, position);
    // }

    Regex sensor = new Regex("/sensor.*");
    if (!sensor.IsMatch(address)) return;

    int id = Int32.Parse(address.Substring(8, 1));
    Debug.Log($"id: {id}");

    Regex idReg = new Regex("/sensor_.*_id");
    if (idReg.IsMatch(address))
    {
      this.onGetId.Invoke(id, data.GetElementAsInt(0));
    }

    Regex xReg = new Regex("/sensor_.*_x");
    if (xReg.IsMatch(address))
    {
      this.onGetX.Invoke(id, data.GetElementAsFloat(0));
    }

    Regex yReg = new Regex("/sensor_.*_y");
    if (yReg.IsMatch(address))
    {
      this.onGetY.Invoke(id, data.GetElementAsFloat(0));
    }
  }

  /// <summary>
  /// 送信
  /// </summary>
  /// <param name="address"></param>
  /// <param name="val"></param>
  public void Send(string address, int val)
  {
    Debug.Log($"send{val}");
    this._client.Send(address, val);
  }

}
