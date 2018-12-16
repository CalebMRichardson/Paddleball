using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace PaddleBall {

    class Ball : Sprite {

        private float moveSpeed;
        private Vector2 movementVec;
        private Paddle paddle;
        private SoundEffect collide; 

        public Ball(Paddle _paddle) {
            Load("assets/ball_16x16_px");
            moveSpeed = 300.0f;
            movementVec = new Vector2();
            boundingRect = new Rectangle(0, 0, ( int )width, ( int )height);
            paddle = _paddle;
            collide = ContentUtil.contentManger.Load<SoundEffect>("assets/collision");
            SoundEffect.MasterVolume = 0.1f;
        }

        public void Shoot(float _lastXDirection) {

            if (_lastXDirection == 0.0f) {
                Random rand = new Random();
                int randNum = (int)rand.Next(1, 3);
                if (randNum == 1) {
                    _lastXDirection = 1; 
                } else {
                    _lastXDirection = -1;
                }
                Debug.WriteLine(_lastXDirection);
            }

            movementVec.X = _lastXDirection;
            movementVec.Y = -1.0f;
        }

        public void Bounce(Vector2 _newDirection) {
            if (movementVec.Y != 0.0f) {
                movementVec = _newDirection; 
                Collide();
            }
        }

        public Vector2 GetMovementVec() {
            return movementVec;
        }

        private void Collide() {
            collide.CreateInstance().Play();
        }

        public void Update(GameTime _gameTime) {

            if (position.X + width >= PaddleBall.WIDTH) {
                movementVec.X = -1.0f;
                Collide();
            } else if (position.X <= 0) {
                movementVec.X = 1.0f;
                Collide();
            }

            if (position.Y >= PaddleBall.HEIGHT) {
                movementVec = Vector2.Zero;
                paddle.SetChild(this);

            } 
            if (position.Y <= 0) {
                movementVec.Y = 1.0f;
                Collide();
            }
            
            Move(movementVec, moveSpeed, _gameTime);
            boundingRect.Location = position.ToPoint();
        }
    }
}
