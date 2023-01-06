using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundToggleButton : MonoBehaviour
{
    public enum ButtonType
    {
        BackgroundMusic,
        SoundFX
    };
    public ButtonType type;
    public Sprite onSprite;
    public Sprite OffSprite;

    public GameObject button;
    public Vector3 offButtonPoisiton;
    private Vector3 _onButtonPoisiton;
    private Image _image;

    // Start is called before the first frame update
    void Start()
    {
        _image = GetComponent<Image>();
        _image.sprite = onSprite;
        _onButtonPoisiton = button.GetComponent<RectTransform>().anchoredPosition;
        ToggleButton();
        
    }

    public void ToggleButton()
    {
        var mute = false;
        if (type == ButtonType.BackgroundMusic)
            mute = SoundManager.instance.IsBackgrounMusicMute();
        else
            mute = SoundManager.instance.IsSoundFXMute();

        if (mute)
        {
            _image.sprite = OffSprite;
            button.GetComponent<RectTransform>().anchoredPosition = offButtonPoisiton;
        }
        else
        {
            _image.sprite = onSprite;
            button.GetComponent<RectTransform>().anchoredPosition = _onButtonPoisiton;
        }
    }
   
}
