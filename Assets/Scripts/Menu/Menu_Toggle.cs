using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Menu
{
    public sealed class Menu_Toggle : MonoBehaviour
    {
        [SerializeField]
        private List<Toggle_Text> _toggle_Text;

        private readonly string _key_Input = "Input_Save";

        private Toggle _toggle;
        private Text _text;

        private Input_Type _input_Type;


        [System.Serializable]
        private struct Toggle_Text
        {
            public string Text;
            public Input_Type Input_type;
        }

        private void Awake()
        {
            _toggle = GetComponent<Toggle>();
            _text = GetComponentInChildren<Text>();
        }

        private void OnEnable()
        {
            Load();
        }

        public void Change_Input()
        {
            // set type
            if(_toggle.isOn)
            {
                _input_Type = Input_Type.Keyboard;
            }
            else
            {
                _input_Type = Input_Type.Keyboard_Mouse;
            }

            // set text
            for (int x = 0; x < _toggle_Text.Count; x++)
            {
                if (_toggle_Text[x].Input_type == _input_Type)
                {
                    _text.text = _toggle_Text[x].Text;
                }
            }

            // set save
            PlayerPrefs.SetInt(_key_Input, (int)_input_Type);
        }

        private void Load()
        {
            // get save
            if (PlayerPrefs.HasKey(_key_Input))
            {
                _input_Type = (Input_Type)PlayerPrefs.GetInt(_key_Input);

                // set toggle
                switch (_input_Type)
                {
                    case Input_Type.Keyboard:

                        _toggle.isOn = true;

                        break;


                    case Input_Type.Keyboard_Mouse:

                        _toggle.isOn = false;

                        break;
                }
            }
            // default load
            else
            {
                _input_Type = Input_Type.Keyboard;
                _toggle.isOn = true;
            }

            // set text
            for (int x = 0; x < _toggle_Text.Count; x++)
            {
                if (_toggle_Text[x].Input_type == _input_Type)
                {
                    _text.text = _toggle_Text[x].Text;
                }
            }

            // set save
            PlayerPrefs.SetInt(_key_Input, (int)_input_Type);
        }
    }

    public enum Input_Type
    {
        Keyboard,
        Keyboard_Mouse
    }
}
