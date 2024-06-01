using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIVolumeSlider : MonoBehaviour
{
    Slider slider;

    private void Start() {
        slider = GetComponent<Slider>();
        AudioManager.instance.SetMaxVolume(slider.maxValue);
    }
   private void Update()
   {
      AudioManager.instance.SetVolume(slider.value);
   }
}
