#pragma warning disable 0649

using UnityEngine;
using UnityEngine.SceneManagement;

using System.Collections;
using System.Linq;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private Animator fadeAnimator;

    private float fadeOutDuration; // so it auto scale on fade out duration

    private void Start()
    {
        // gangster line
        fadeOutDuration = fadeAnimator.runtimeAnimatorController.animationClips.First(c => c.name.Equals("FadeOut")).averageDuration;
    }

    // Call this function from button's OnClick event to load a level giving a build index
    public void Load(int buildIndex)
    {
        // Load level when the fade is finished
        // Use a coroutine so it doesn't freezes unity
        IEnumerator LoadInternal()
        {
            fadeAnimator.SetTrigger("FadeOut");
            
            yield return new WaitForSeconds(fadeOutDuration);

            SceneManager.LoadScene(buildIndex);
        }

        StartCoroutine(LoadInternal());
    }
}
