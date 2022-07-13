using UnityEngine;
using UnityEngine.SceneManagement;

namespace Menu
{
    public sealed class Base_Menu : MonoBehaviour
    {
        private Menu_Toggle _menu_Toggle;

        private void Awake()
        {
            _menu_Toggle = GetComponentInChildren<Menu_Toggle>();
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }
        
        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void Button_New_Game()
        {
            SceneManager.LoadScene(1);
        }

        public void Change_Control()
        {
            _menu_Toggle.Change_Input();
        }

        public void Button_Exit()
        {
            Application.Quit();
        }
    }
}
