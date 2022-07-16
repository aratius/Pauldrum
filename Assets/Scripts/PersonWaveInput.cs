using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Es.WaveformProvider.Sample
{
	/// <summary>
	/// 人と波のインタラクションによるInput
	/// </summary>
	public class PersonWaveInput : MonoBehaviour
	{
    [SerializeField]
		private WaveConductor waveConductor;
		[SerializeField]
		private Texture2D waveform;

		[SerializeField, Range(0f, 1f)]
		private float inputScale = 0.05f;

		[SerializeField, Range(0f, 1f)]
		private float inputStrength = 0.1f;

		[SerializeField]
		private List<WaveConductor> targets;

		public void Input(Vector2 uvPos)
		{
	  Debug.Log($"input {uvPos}");
      this.waveConductor.Input(waveform, uvPos, inputScale, inputStrength);
		}
	}
}