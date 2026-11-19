using UnityEngine;

using UnityEngine.SceneManagement;
public class NewMonoBehaviourScript : MonoBehaviour
{
  // Reference to the ParticleSystem Systen
  [SerializeField] private ParticleSystem finishParticles;

  void OnTriggerEnter2D(Collider2D other)
   {
    
       if (other.CompareTag("Player"))
      {
           Debug.Log("WIN!!!!");
           //play particles
           finishParticles.Play();

           //reload Scene
           Invoke(nameof(reloadScene), 1.5f);
        }

  }

  void reloadScene()
  {
      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
  }
}
