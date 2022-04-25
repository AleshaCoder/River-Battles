using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Utils
{
    [RequireComponent(typeof(Text))]
    public class FpsCounter : MonoBehaviour
    {
        [SerializeField] private float updateDelay = 0.5f;
        private float _delay;
        private int _count;

        private Text _text;

        private async void Start()
        {
            _text = GetComponent<Text>();
            while (this)
            {
                var value = _delay / _count;
                _delay = 0;
                _count = 0;

                _text.text = $"{(int) (1f / value)} FPS";
                await Task.Delay(TimeSpan.FromSeconds(updateDelay));
            }
        }

        private void Update()
        {
            _delay += Time.unscaledDeltaTime;
            _count++;
        }
    }
}
