using UnityEngine;

using UnityEngine.SceneManagement;
public class NewMonoBehaviourScript : MonoBehaviour
{
  // Reference to the ParticleSystem Systen
  [SerializeField] private ParticleSystem finishParticles;
  [SerializeField] private AudioClip finishSound;



  AudioSource audioSource;
  void Start()
  {
      audioSource = GetComponent<AudioSource>();
  }
  void OnTriggerEnter2D(Collider2D other)
   {
    
       if (other.CompareTag("Player"))
      {
           Debug.Log("WIN!!!!");
           //play particles
           finishParticles.Play();

           //play sound
           audioSource.PlayOneShot(finishSound);

           //reload Scene
           Invoke(nameof(reloadScene), 1.5f);
        }

  }

  void reloadScene()
  {
      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
  }
}
