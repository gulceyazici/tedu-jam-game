using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    public Image[] hearts;
    public Sprite fullHeart;
    public Animator[] heartAnimators; 

    public void UpdateHealth(int currentHealth)
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < currentHealth)
            {
                hearts[i].sprite = fullHeart;
                hearts[i].enabled = true;
            }
            else
            {
                if (hearts[i].enabled) // If the heart is currently visible, play the animation
                {
                    StartCoroutine(DisableHeartAfterAnimation(heartAnimators[i], i));
                }
            }
        }
    }

    private IEnumerator DisableHeartAfterAnimation(Animator animator, int index)
    {
        animator.SetTrigger("Disappear");

        // Wait for the animation to finish
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);


        hearts[index].enabled = false; 
    }

}


