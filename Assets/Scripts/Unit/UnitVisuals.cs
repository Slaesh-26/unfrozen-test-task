using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitVisuals : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Animator animator;
    [SerializeField] private float selectionAnimationLength = 0.3f;

    private Color selectedColor;
    private Color deselectedColor;
    private Color defaultColor;
    
    public void Init(Color selectedColor, Color deselectedColor, Color defaultColor)
    {
        this.selectedColor = selectedColor;
        this.deselectedColor = deselectedColor;
        this.defaultColor = defaultColor;
        
        SetDefaultMarkerColor();
    }

    public void SetSelectedMarkerColor()
    {
        spriteRenderer.color = selectedColor;
    }
    
    public void SetDeselectedMarkerColor()
    {
        spriteRenderer.color = deselectedColor;
    }

    public void SetDefaultMarkerColor()
    {
        spriteRenderer.color = defaultColor;
    }

    public void PlaySelectedAnimation()
    {
        StartCoroutine(PlaySelectedAnimationInternal());
    }

    public void AttackAnimation()
    {
        animator.SetTrigger("Attack");
    }

    public void GetDamageAnimation()
    {
        animator.SetTrigger("GetDamage");
    }

    private IEnumerator PlaySelectedAnimationInternal()
    {
        spriteRenderer.enabled = false;
        yield return new WaitForSeconds(selectionAnimationLength);
        spriteRenderer.enabled = true;
    }
}
