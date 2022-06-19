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
  private int _port;

  private OscServer _server;

  void Awake()
  {
    this.onGetPosition = new UnityEvent<string, Vector2>();
  }

  void Start()
  {
    this._server = new OscServer(this._port);
    this._server.MessageDispatcher.AddCallback("", this._onReceiveOsc);
  }

  void Update()
  {

  }

  private void _onReceiveOsc(string address, OscDataHandle data)
  {
    Regex touch = new Regex("touch");
    if (touch.IsMatch(address))
    {
      Vector2 position = new Vector2(data.GetElementAsFloat(0), data.GetElementAsFloat(1));
      this.onGetPosition.Invoke(address, position);
    }
  }

}
