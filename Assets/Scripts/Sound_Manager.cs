using UnityEngine;
using UnityEngine.UI;

public class Sound_Manager : MonoBehaviour
{
    //Variables

    [SerializeField]
    Slider volume;
    AudioSource sound;
    void Start()
    {
        sound = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (volume.value != sound.volume)
        {
            sound.volume = volume.value;
        }
    }
}
