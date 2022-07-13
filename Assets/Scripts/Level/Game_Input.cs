using Menu;
using UnityEngine;

public sealed class Game_Input : MonoBehaviour
{
    public static Game_Input Instace;

    [SerializeField]
    private Base_Menu _base_Menu;

    public bool Pause { get; private set; }

    private void Awake()
    {
        Instace = this;
    }

    private void Update()
    {
        Set_Pause();
    }

    public void Button_Set_Pause()
    {
        if (!Pause)
        {
            Pause = true;
            Time.timeScale = 0.0f;
            _base_Menu.Show();
        }
        else
        {
            Pause = false;
            Time.timeScale = 1.0f;
            _base_Menu.Hide();
        }
    }

    private void Set_Pause()
    {
        if (!Pause)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Pause = true;
                Time.timeScale = 0.0f;
                _base_Menu.Show();
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Pause = false;
                Time.timeScale = 1.0f;
                _base_Menu.Hide();
            }
        }
    }
}
