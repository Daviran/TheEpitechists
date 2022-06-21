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

        public void LoadNextLevel()
        {
            StartCoroutine(LoadNextScene(SceneManager.GetActiveScene().buildIndex + 1));
        }

        IEnumerator LoadNextScene(int levelIndex)
        {
            //transition.SetTrigger("Start");
            yield return new WaitForSeconds(transitionTime);
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
