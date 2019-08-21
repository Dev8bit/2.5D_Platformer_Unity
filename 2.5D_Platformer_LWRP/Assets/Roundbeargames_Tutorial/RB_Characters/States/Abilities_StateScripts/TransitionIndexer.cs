﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public enum TransitionConditionType
    {
        UP,
        DOWN,
        LEFT,
        RIGHT,
        ATTACK,
        JUMP,
        GRABBING_LEDGE,

        LEFT_OR_RIGHT,

        GROUNDED,

        MOVE_FORWARD,
    }

    [CreateAssetMenu(fileName = "New State", menuName = "Roundbeargames/AbilityData/TransitionIndexer")]
    public class TransitionIndexer : StateData
    {
        public int Index;
        public List<TransitionConditionType> transitionConditions = new List<TransitionConditionType>();

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (MakeTransition(characterState.characterControl))
            {
                animator.SetInteger(TransitionParameter.TransitionIndex.ToString(), Index);
            }
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (animator.GetInteger(TransitionParameter.TransitionIndex.ToString()) == 0)
            {
                if (MakeTransition(characterState.characterControl))
                {
                    animator.SetInteger(TransitionParameter.TransitionIndex.ToString(), Index);
                }
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            animator.SetInteger(TransitionParameter.TransitionIndex.ToString(), 0);
        }

        private bool MakeTransition(CharacterControl control)
        {
            foreach(TransitionConditionType c in transitionConditions)
            {
                switch (c)
                {
                    case TransitionConditionType.UP:
                        {
                            if (!control.MoveUp)
                            {
                                return false;
                            }
                        }
                        break;
                    case TransitionConditionType.DOWN:
                        {
                            if (!control.MoveDown)
                            {
                                return false;
                            }
                        }
                        break;
                    case TransitionConditionType.LEFT:
                        {
                            if (!control.MoveLeft)
                            {
                                return false;
                            }
                        }
                        break;
                    case TransitionConditionType.RIGHT:
                        {
                            if (!control.MoveRight)
                            {
                                return false;
                            }
                        }
                        break;
                    case TransitionConditionType.ATTACK:
                        {
                            if (!control.animationProgress.AttackTriggered)
                            {
                                return false;
                            }
                        }
                        break;
                    case TransitionConditionType.JUMP:
                        {
                            if (!control.Jump)
                            {
                                return false;
                            }
                        }
                        break;
                    case TransitionConditionType.GRABBING_LEDGE:
                        {
                            if (!control.ledgeChecker.IsGrabbingLedge)
                            {
                                return false;
                            }
                        }
                        break;
                    case TransitionConditionType.LEFT_OR_RIGHT:
                        {
                            if (!control.MoveLeft && !control.MoveRight)
                            {
                                return false;
                            }
                        }
                        break;
                    case TransitionConditionType.GROUNDED:
                        {
                            if (control.SkinnedMeshAnimator.GetBool(TransitionParameter.Grounded.ToString()) == false)
                            {
                                return false;
                            }
                        }
                        break;
                    case TransitionConditionType.MOVE_FORWARD:
                        {
                            if (control.IsFacingForward())
                            {
                                if (!control.MoveRight)
                                {
                                    return false;
                                }
                            }
                            else
                            {
                                if (!control.MoveLeft)
                                {
                                    return false;
                                }
                            }
                        }
                        break;
                }
            }

            return true;
        }
    }
}