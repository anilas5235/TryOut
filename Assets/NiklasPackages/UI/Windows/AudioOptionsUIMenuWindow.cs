using Project.Scripts.UI.InteractableUI;
using UnityEngine;
using UnityEngine.Audio;
using Utilities;

namespace Project.Scripts.UI.Windows
{
    public class AudioOptionsUIMenuWindow : UIMenuWindowHandler
    {
        [Header("Slider")] [SerializeField] private AudioSlider main;
        [SerializeField] private AudioSlider music, effects, voice;

        [Header("NamesOfExposedParameters")] [SerializeField]
        private string paramMaster = "Master";

        [SerializeField] private string paramMusic = "Music", paramEffects = "Effects", paramVoice = "Voice";

        [Header("AudioMixer")] [SerializeField]
        private AudioMixer mainAudioMixer;

        private string path = "audioOptions";

        private void OnEnable()
        {
            LoadFromSaveText();
        }

        private void Start()
        {
            UpdateSoundOptions();
        }

        private void OnDisable()
        {
            SaveOptionsToText();
        }

        public void UpdateSoundOptions()
        {
            mainAudioMixer.SetFloat(paramMaster, ConvertSliderValueTodB(main.Value));
            mainAudioMixer.SetFloat(paramMusic, ConvertSliderValueTodB(music.Value));
            mainAudioMixer.SetFloat(paramEffects, ConvertSliderValueTodB(effects.Value));
            mainAudioMixer.SetFloat(paramVoice, ConvertSliderValueTodB(voice.Value));
        }

        public void SaveOptionsToText()
        {
            var save = SaveSystem.Load<AudioOptionsSave>(path);
            float[] optionsValues = save.audioOptions;
            mainAudioMixer.GetFloat(paramMaster, out optionsValues[0]);
            mainAudioMixer.GetFloat(paramMusic, out optionsValues[1]);
            mainAudioMixer.GetFloat(paramEffects, out optionsValues[2]);
            mainAudioMixer.GetFloat(paramVoice, out optionsValues[3]);
            
            SaveSystem.Save(path,save);
        }

        public void LoadFromSaveText()
        {
            var save = SaveSystem.Load<AudioOptionsSave>(path);
            float[] optionsValues = save.audioOptions;
            mainAudioMixer.SetFloat(paramMaster, optionsValues[0]);
            mainAudioMixer.SetFloat(paramMusic, optionsValues[1]);
            mainAudioMixer.SetFloat(paramEffects, optionsValues[2]);
            mainAudioMixer.SetFloat(paramVoice, optionsValues[3]);

            main.Value = ConvertDBToSliderValue(optionsValues[0]);
            music.Value = ConvertDBToSliderValue(optionsValues[1]);
            effects.Value = ConvertDBToSliderValue(optionsValues[2]);
            voice.Value = ConvertDBToSliderValue(optionsValues[3]);
        }

        private float ConvertSliderValueTodB(float sliderValue)
        {
            return Mathf.Log10(sliderValue) * 20f;
        }

        private float ConvertDBToSliderValue(float dBValue)
        {
            return Mathf.Pow(10, (dBValue) / 20f);
        }

    }
    
    [System.Serializable]
    public class AudioOptionsSave
    {
        public float[] audioOptions = new float[10];
    }
}
