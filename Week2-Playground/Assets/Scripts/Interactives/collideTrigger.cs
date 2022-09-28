using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class collideTrigger : MonoBehaviour
{
    public enum Effect {nothing, destroyObject, toggleAnimation, startTimeline, triggerParticles, playAudio, teleportPlayer, killPlayer, loadScene};
    [SerializeField] private Effect effect = Effect.nothing;
    [SerializeField] private int sceneIndexToLoad;
    public GameObject targetObject;
    // Start is called before the first frame update
    void Start()
    {
        if (targetObject == null){
            Debug.Log("Warning: trigger has no target specified");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Activate(GameObject player){
        if (targetObject != null){
            //Destroy Effect
            if (effect == Effect.destroyObject){
            Destroy(targetObject);
            }
            //Animation Effect
            else if(effect == Effect.toggleAnimation){
                Animator anim = targetObject.transform.gameObject.GetComponent<Animator>();
                if (anim != null){
                    if (anim.enabled){
                        anim.enabled = false;
                    }
                    else{
                        anim.enabled = true;
                    }
                }
            }
            //Timeline Effect
            else if (effect == Effect.startTimeline){
                PlayableDirector pd = targetObject.transform.gameObject.GetComponent<PlayableDirector>();
                if (pd != null){
                    if(pd.state != PlayState.Playing){
                        pd.Play();
                    }
                }
            }
            //Audio Effect
            else if (effect == Effect.playAudio){
                AudioSource audi = targetObject.transform.gameObject.GetComponent<AudioSource>();
                if(audi != null){
                    if(!audi.isPlaying){
                    audi.Play();
                    }
                }
            }
            //Particle Effects
            else if (effect == Effect.triggerParticles){
                ParticleSystem ps = targetObject.transform.gameObject.GetComponent<ParticleSystem>();
                if (ps != null){
                    ps.Play();

                }
            }
            //Teleport Player
            else if (effect == Effect.teleportPlayer){
                CharacterController cc = player.GetComponent<CharacterController>();
                if (cc != null){
                    cc.enabled = false;
                    player.transform.position = targetObject.transform.position;
                    cc.enabled = true;
                }
            }



        }
        //No need for a target Object
        //Kill Player
        if (effect == Effect.killPlayer){
            Scene thisScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(thisScene.name);
        }
            //Load Scene
        if (effect == Effect.loadScene){
            Debug.Log(SceneManager.sceneCount);
                Debug.Log("WTF?");
            if(sceneIndexToLoad >= 0 && sceneIndexToLoad <= SceneManager.sceneCount){
                SceneManager.LoadScene(sceneIndexToLoad, LoadSceneMode.Single);
            }
        }

    }
}

