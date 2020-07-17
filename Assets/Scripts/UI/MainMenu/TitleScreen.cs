using UnityEngine;

namespace UI.MainMenu {
    public class TitleScreen : MonoBehaviour {
        [SerializeField] private GameObject tutorialMenu;
        [SerializeField] private GameObject mainMenu;
        [SerializeField] private GameObject headerText;

        public void OnTutorialClicked() {
            tutorialMenu.SetActive(true);
            mainMenu.SetActive(false);
            headerText.SetActive(false);
        }
        
        public void OnTutorialExitClicked() {
            tutorialMenu.SetActive(false);
            mainMenu.SetActive(true);
            headerText.SetActive(true);
        }
    }
}