using System;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.UI;

namespace RaindropGame
{
    public class RaindropKeyIndicator : MonoBehaviour
    {
        [Header("Setting")] [SerializeField] private Light2D LightEffect;
        [SerializeField] private TextMeshProUGUI text;
        
        private Image image;
        private Animator animator;
        private static readonly int Press = Animator.StringToHash("Press");

        private void Awake()
        {
            image = GetComponent<Image>();
            animator = GetComponent<Animator>();
        }

        public void SetUpIndicator(KeyCode keyCode, Color color)
        {
            //Set up key text
            text.text = keyCode.ToString();
            
            //Set up color
            image.color = new Color(color.r,color.g,color.b,0.8f);
            LightEffect.color = color;
        }

        public void KeyPressed()
        {
            animator.Play("KeyIndiactor_Pressed");
            animator.SetBool("Pressed",true);
        }

        public void KeyReleased()
        {
            animator.SetBool("Pressed",false);
        }
    }
}