using TheraBytes.BetterUi;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSounds : MonoBehaviour
    {
        [SerializeField] private AudioClip _clickSound;
        [SerializeField] private AudioClip _hoverSound;

        // Start is called before the first frame update
        private void Start()
        {
            var button = GetComponent<Button>();
            if (button) button.onClick.AddListener(PlayClickSound);
            else
            {
                var betterButton = GetComponent<BetterButton>();
                if (betterButton) betterButton.onClick.AddListener(PlayClickSound);
            }

            var toggle = GetComponent<Toggle>();
            if (toggle) toggle.onValueChanged.AddListener(PlayClickSound);
            else
            {
                var betterToggle = GetComponent<BetterToggle>();
                if (betterToggle) betterToggle.onValueChanged.AddListener(PlayClickSound);
            }

            var slider = GetComponent<Slider>();
            if (slider) slider.onValueChanged.AddListener(PlayClickSound);
        }

        
        private void PlayClickSound(bool state)
        {
            var toggle = GetComponent<Toggle>(); // inefficient
            if (!toggle) return;
            if (!state && toggle.group != null && toggle.group.AnyTogglesOn()) return;
            
            AudioMgr.Instance.PlaySound(_clickSound);
        }
        
        private void PlayClickSound(float _)
        {
            AudioMgr.Instance.PlaySound(_clickSound, _);
        }

        private void PlayClickSound()
        {
            AudioMgr.Instance.PlaySound(_clickSound);
        }

        public void PlayHoverSound()
        {
            if (_hoverSound != null)
            {
                AudioMgr.Instance.PlaySound(_hoverSound);
            }
            else
            {
                AudioMgr.Instance.PlaySound(AudioMgr.SoundTypes.ButtonHover);
            }
        }
    }
