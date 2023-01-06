using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class anim_Controllor : MonoBehaviour
{
    public static Home Instance { set; get; }
    [SerializeField] private Animator menuAnimator;

    private void Awake()
    {
        Instance = this;
    }

    // Methods that Manage the Display of Screens
    // Ekranlar Gosterimini Yoneten Metodlar
    public void startAnim()
    {
        menuAnimator.SetTrigger("startAnimation");
        Debug.Log("Cart1Button");
    }
    public void cart1Button()
    {
        menuAnimator.SetTrigger("CART1S");
        Debug.Log("Cart1Button");
    }
}
