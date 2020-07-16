using UnityEngine;

namespace Audio {
    public class AudioManager : MonoBehaviour {
        public static AudioManager Instance;
        [SerializeField] private AudioSource gameMusic;
        [SerializeField] private AudioSource menuMusic;
        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
                DontDestroyOnLoad(this);
            }
        }

        public void PlayGameMusic() {
            menuMusic.Stop();
            gameMusic.Play();
        }

        public void PlayMenuMusic() {
            gameMusic.Stop();
            menuMusic.Play();
        }
    }
}