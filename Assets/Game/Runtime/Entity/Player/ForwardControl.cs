using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Game
{
    public class ForwardControl : BaseControl
    {
        protected override void Move()
        {
            if (!data.canMove) return;
            _rb2D.velocity = new Vector2(data.fixedMoveSpeed, _rb2D.velocity.y);
        }
    }
}