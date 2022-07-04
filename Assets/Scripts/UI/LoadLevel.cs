using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TopdownRPG.UI
{
    public class LoadLevel : MonoBehaviour
    {
        public float transitionTime = 1f;
        public Animator transition;

        public void LoadNextLevel(float delay = 1f)
        {
            StartCoroutine(LoadNextScene(SceneManager.GetActiveScene().buildIndex + 1, delay));
        }

        IEnumerator LoadNextScene(int levelIndex, float delay)
        {
            //transition.SetTrigger("Start");
            yield return new WaitForSeconds(delay);
            SceneManager.LoadScene(levelIndex);


        }
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }

}