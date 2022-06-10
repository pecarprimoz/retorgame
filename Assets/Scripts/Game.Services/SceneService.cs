using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Services {
    public class SceneService {
        public void ReloadScene () {
            SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
        }
    }
    
}