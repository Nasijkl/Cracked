using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatusElementWidget : MonoBehaviour
{
   [SerializeField] private Image icon;

   [SerializeField] private TextMeshProUGUI text;

   public int trigger_id;

   private CanvasGroup canvasGroup;

   private void Awake()
   {
      canvasGroup = GetComponent<CanvasGroup>();
   }

   public void Initialize(CharacterStatus status, int value)
   {
      trigger_id = status.trigger.trigger_id;
      icon.sprite = status.trigger.sprite;
      text.text = value.ToString();
   }

   public void Show()
   {
      canvasGroup.DOFade(1.0f, 1.0f);
   }

   public void UpdateModifier(int value)
   {
      text.text = value.ToString();
   }

   public void FadeAndDestroy()
   {
      var sequence = DOTween.Sequence();
      sequence.Append(icon.DOFade(0, 0.3f));
      sequence.AppendCallback(() => Destroy(gameObject));

      text.DOFade(0, 0.3f);
   }
   
}
