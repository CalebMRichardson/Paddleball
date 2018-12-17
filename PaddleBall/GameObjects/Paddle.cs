using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

namespace PaddleBall {

    class Paddle : Sprite {

        private float   moveSpeed;
        private bool    ballAttatched = true;
        private Ball    ball;
        private float   lastXDirection;

        public Paddle() {
            Load ( "assets/paddle_64x16_px" );
            moveSpeed = 300.0f;
            lastXDirection = 0.0f;
            boundingRect = new Rectangle(0, 0, (int)width, (int)height);
            float paddleStartingXPos = (PaddleBall.WIDTH / 2) - (width / 2);
            float paddleStartingYPos = PaddleBall.HEIGHT - height;
            SetPosition(paddleStartingXPos, paddleStartingYPos);
        }

        public void Update(GameTime _gameTime) {

            boundingRect.Location = position.ToPoint();
            CheckBallCollision();

            if (Keyboard.GetState().IsKeyDown(Keys.D) || Keyboard.GetState().IsKeyDown(Keys.Right)) {
                
                if (position.X + width <= PaddleBall.WIDTH -2) {
                    Move(1.0f, 0.0f, moveSpeed, _gameTime);
                    lastXDirection = 1.0f; 
                }

            } else if (Keyboard.GetState().IsKeyDown(Keys.A) || Keyboard.GetState().IsKeyDown(Keys.Left)) {
                
                if (position.X >= 2) {
                    Move(-1.0f, 0.0f, moveSpeed, _gameTime);
                    lastXDirection = -1.0f;
                }
            }

            if (ballAttatched) {
                SetChildPosition();
                if (Keyboard.GetState().IsKeyDown(Keys.Space) || Keyboard.GetState().IsKeyDown(Keys.Up)) {
                    ballAttatched = false;
                    ball.Shoot(lastXDirection);
                }
            }
        }

        private void CheckBallCollision() {
            if (boundingRect.Intersects(ball.GetBoudingRect())) {
                Vector2 newDirection = new Vector2(); 
                if (boundingRect.Left > ball.GetBoudingRect().Left) {
                    newDirection.X = -1;
                } else if (boundingRect.Right < ball.GetBoudingRect().Right) {
                    newDirection.X = 1; 
                } else {
                    newDirection.X = ball.GetMovementVec().X; 
                }
                newDirection.Y = -1; 
                ball.Bounce(newDirection);
            }
        }

        private void SetChildPosition() {

            float x = position.X + (width / 2) - (ball.width / 2);
            float y = position.Y - ball.height;
            ball.SetPosition(x, y);
        }

        public void SetChild(Ball _child) {
            ball = _child;
            ballAttatched = true;
        }
    }
}
