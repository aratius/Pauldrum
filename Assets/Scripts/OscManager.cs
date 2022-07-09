using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Events;
using OscJack;

public class OscManager : MonoBehaviour
{

  public UnityEvent<string, Vector2> onGetPosition;

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
    this.onGetPosition = new UnityEvent<string, Vector2>();
  }

  void Start()
  {
    this._server = new OscServer(this._outPort);
    this._server.MessageDispatcher.AddCallback("", this._onReceiveOsc);

    this._client = new OscClient(this._host, this._inPort);
  }

  void Update()
  {

  }

  /// <summary>
  /// OSC受信したら
  /// </summary>
  /// <param name="address"></param>
  /// <param name="data"></param>
  private void _onReceiveOsc(string address, OscDataHandle data)
  {
    Debug.Log("hello");
    Regex touch = new Regex("touch");
    if (touch.IsMatch(address))
    {
      Vector2 position = new Vector2(data.GetElementAsFloat(0), data.GetElementAsFloat(1));
      this.onGetPosition.Invoke(address, position);
    }
  }

  /// <summary>
  /// 送信
  /// </summary>
  /// <param name="address"></param>
  /// <param name="val"></param>
  public void Send(string address, int val)
  {

  }

}
