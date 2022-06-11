using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Events;
using OscJack;

public class OscManager : MonoBehaviour
{

  public UnityEvent<string, Vector2> onGetPosition;

  private OscServer _server;

  void Start()
  {
    this._server = new OscServer(8000);
    this._server.MessageDispatcher.AddCallback("", this._onReceiveOsc);

    this.onGetPosition = new UnityEvent<string, Vector2>();
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
      this.onGetPosition.Invoke("hoge", position);
      Debug.Log(position);
    }
  }

}
