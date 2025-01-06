using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageFlash : MonoBehaviour
{
  [SerializeField] private Color flashColor = Color.white;
  [SerializeField] private float flashTime = 0.25f;

  private SpriteRenderer[] _spriteRenderers;
  public Material[] _materials;

  private Coroutine _damageFlashCoroutine;

  private void Start()
  {
    _spriteRenderers = GetComponents<SpriteRenderer>();
    _materials = new Material[_spriteRenderers.Length];

    for (int i = 0; i < _spriteRenderers.Length; i++)
    {
        _materials[i] = _spriteRenderers[i].material;
    }
  }

  public void CallDamageFlash()
  {
    _damageFlashCoroutine = StartCoroutine(DamageFlasher());
  }

  private IEnumerator DamageFlasher()
  {
    SetFlashColor();

    float currentFlashAmount = 0f;
    float elapsedTime = 0f;
    while(elapsedTime < flashTime)
    {
       elapsedTime += Time.deltaTime;

       currentFlashAmount = Mathf.Lerp(1f, 0f, (elapsedTime/ flashTime));
       SetFlashAmount(currentFlashAmount);

        yield return null;
    }
    
  } 

  private void SetFlashColor()
  {
    for(int i = 0; i < _materials.Length; i++)
    {
     _materials[i].SetColor("_FlashColor", flashColor);
    }
  }

  private void SetFlashAmount(float amount)
  {

    for (int i = 0; i < _materials.Length; i++)
    {
        _materials[i].SetFloat("_FlashAmount", amount);
    }
  }
}
