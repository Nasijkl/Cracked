using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectResolutionManager : BaseManager
{
    public CardSelectionHasArrow cardSelectionHasArrow;
    private CharacterObject _currentEnemy;
    private CardDisplayManager _cardDisplayManager;


    public void Start()
    {
        _cardDisplayManager = FindFirstObjectByType<CardDisplayManager>();
    }

    public void ResolveCardEffects(CrackedCardData card, CharacterObject playerSelectedTarget)
    {
        
        
        /*
        foreach (var effect in card.Template.Effects)
        {
            var targetableEffect = effect as TargetableEffect;

            if (targetableEffect != null)
            {
                var targets = GetTargets(targetableEffect, playerSelectedTarget, true);
                foreach (var target in targets)
                {
                    targetableEffect.Resolve(Player.Character, target.Character);

                    foreach (var groupManager in targetableEffect.SourceActions)
                    {
                        foreach (var action in groupManager.Group.Actions)
                        {
                            action.Execute(Player.gameObject);
                        }
                    }
                    
                    foreach (var groupManager in targetableEffect.TargetActions)
                    {
                        foreach (var action in groupManager.Group.Actions)
                        {
                            var enemy = cardSelectionHasArrow.GetSelectedEnemy();
                            action.Execute(enemy.gameObject);
                        }
                    }
                }
            }
        }
        */
    }

    public void ResolveCardEffects(CrackedCardData card, List<GameObject> selectedObjects)
    {
        for(int i = 2; i < 4; i++)
        {
            var effect_piece = card.card_pieces[i] as EffectPieceData;
            if(effect_piece == null)
            {
                continue;
            }
            for(int j=0; j< effect_piece.events.Count; j++)
            {
                CardEventTuple tuple = effect_piece.events[j];
                GameObject primary_object;
                GameObject secondary_object;
                int target_index = 0;
                //TODO: handle all the target type
                if(tuple.card_event.primary_target == EventTargetType.Null)
                {
                    primary_object = null;
                }
                else if(tuple.card_event.primary_target == EventTargetType.PlayerSelf)
                {
                    primary_object = Player.gameObject;
                }
                else if(tuple.card_event.primary_target == EventTargetType.SelectedEnemy)
                {
                    primary_object = selectedObjects[target_index];
                    target_index++;
                }
                else if(tuple.card_event.primary_target == EventTargetType.SelectedHandCard)
                {
                    primary_object = selectedObjects[target_index];
                    target_index++;
                }
                else
                {
                    primary_object = null;
                }

                if(tuple.card_event.secondary_target == EventTargetType.Null)
                {
                    secondary_object = null;
                }
                else if(tuple.card_event.secondary_target == EventTargetType.Deck)
                {
                    secondary_object = runtimeDeckManager.gameObject;
                }
                else if(tuple.card_event.secondary_target == EventTargetType.Hand)
                {
                    secondary_object = _cardDisplayManager.gameObject;
                }
                else if(tuple.card_event.secondary_target == EventTargetType.PlayerSelf)
                {
                    secondary_object = Player.gameObject;
                }
                else
                {
                    secondary_object = null;
                }

                tuple.card_event.Resolve(secondary_object, primary_object, tuple.value);
            }
        }
    }

    public void ResolveCardEffects(CrackedCardData card)
    {
        
        /*
        foreach (var effect in card.Template.Effects)
        {
            var targetableEffect = effect as TargetableEffect;

            if (targetableEffect != null)
            {
                var targets = GetTargets(targetableEffect, null, true);

                foreach (var target in targets)
                {
                    targetableEffect.Resolve(Player.Character, target.Character);
                }
            }
        }
        */
    }
    
    public void SetCurrentEnemy(CharacterObject enemy)
    {
        _currentEnemy = enemy;
    }

    public void ResolveEnemyEffects(CharacterObject enemy, List<Effect> effects)
    {
        foreach (var effect in effects)
        {
            var targetableEffect = effect as TargetableEffect;
            if (targetableEffect != null)
            {
                var targets = GetTargets(targetableEffect, null, false);

                foreach (var target in targets)
                {
                    targetableEffect.Resolve(enemy.Character, target.Character);
                }
            }
        }
    }

    public void ResolveEnemyEffects(CharacterObject enemy, EffectTuple.EffectTuple effect)
    {
        var targetableEffect = effect.effect as TargetableEffect;
        if (targetableEffect != null)
        {
            var targets = GetTargets(targetableEffect, null, false);

            foreach (var target in targets)
            {
                targetableEffect.aResolve(enemy.Character, target.Character, effect.value);
            }
        }
    }
    

    private List<CharacterObject> GetTargets(
        TargetableEffect effect,
        CharacterObject playerSelectedTarget,
        bool playerSource)
    {
        var targets = new List<CharacterObject>(4);
        
        // 如果动作发起方是主角Player
        if (playerSource)
        {
            switch (effect.Target)
            {
                case EffectTargetType.Self:
                    targets.Add(Player);
                    break;
                case EffectTargetType.TargetEnemy:
                    targets.Add(playerSelectedTarget);
                    break;
            }
        }
        // 如果动作发起方是敌人
        else
        {
            switch (effect.Target)
            {
                case EffectTargetType.Self:
                    targets.Add(_currentEnemy);
                    break;
                case EffectTargetType.TargetEnemy:
                    targets.Add(Player);
                    break;
            }
        }

        return targets;
    }

   
    
}
