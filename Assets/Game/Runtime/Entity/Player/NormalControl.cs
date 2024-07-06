using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Game
{
    public class NormalControl : BaseControl
    {
        protected override void Dash()
        {
            if (!data.canDash) return;
            Vector2 faceDir = Input.Move.ReadValue<Vector2>();
            if (faceDir == Vector2.zero)
            {
                faceDir = Vector2.right; // TODO 朝向
            }

            if (Input.Dash.triggered && data.HasEnergy && data.canDash)
            {
                _rb2D.AddForce(faceDir * data.dashSpeed, ForceMode2D.Impulse);
                data.ChangeEnergy(-data.dashEnergyCost);

                // 禁止移动一会
                DashLock(data.dashLock);
                DashCoolDown(data.dashCoolDown);
            }
        }
    }
}