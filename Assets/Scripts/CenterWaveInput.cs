using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Es.WaveformProvider.Sample
{
	

	/// <summary>
	/// Give input of waveform to random position.
	/// </summary>
	public class CenterWaveInput : MonoBehaviour
	{
		[SerializeField]
		private float waitTime = 0.5f;

		[SerializeField]
		private Texture2D waveform;

		[SerializeField, Range(0f, 1f)]
		private float inputScale = 0.05f;

		[SerializeField, Range(0f, 1f)]
		private float inputStrength = 0.1f;

		[SerializeField]
		private List<WaveConductor> targets;

		private int crr = 0;
		private readonly int max = 8;

    [SerializeField]
    private OscManager _oscManager;

		private void Start()
		{
			StartCoroutine(RandomInput());
		}

		private IEnumerator RandomInput()
		{
			while (true)
			{
				yield return new WaitForSeconds(waitTime);
				foreach (var t in targets)
				{
					var randomUV = new Vector2(0.5f, 0.5f);
					t.Input(waveform, randomUV, inputScale, inputStrength);
					crr ++;
					if(crr > max) crr = 0;
          this._oscManager.Send("/human", 64 + crr);
				}
			}
		}
	}
}