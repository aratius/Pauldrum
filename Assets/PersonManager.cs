using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonManager : MonoBehaviour
{

  [SerializeField]
  private OscManager _oscManager;

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
  public void SetPosition(string id, Vector2 position)
  {
    Debug.Log(id);
  }
}
