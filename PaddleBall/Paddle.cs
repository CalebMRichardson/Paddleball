using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace PaddleBall {

    class Paddle : Sprite {

        private float moveSpeed;
        private bool ballAttatched = true;

        private Ball ball;
        private float lastXDirection; 

        public Paddle() {
            Load ( "assets/paddle_64x16_px" );
            moveSpeed = 150.0f;
            lastXDirection = 0.0f;
            boundingRect = new Rectangle(0, 0, (int)width, (int)height);
        }

        public void Update(GameTime _gameTime) {

            if (Keyboard.GetState().IsKeyDown(Keys.D) || Keyboard.GetState().IsKeyDown(Keys.Right)) {
                
                if (position.X + width <= PaddleBall.WIDTH) {
                    Move(1.0f, 0.0f, moveSpeed, _gameTime);
                    lastXDirection = 1.0f; 
                }

            } else if (Keyboard.GetState().IsKeyDown(Keys.A) || Keyboard.GetState().IsKeyDown(Keys.Left)) {
                
                if (position.X >= 0) {
                    Move(-1.0f, 0.0f, moveSpeed, _gameTime);
                    lastXDirection = -1.0f;
                }
            }

            if (ballAttatched) {
                SetChildPosition();
                if (Keyboard.GetState().IsKeyDown(Keys.Space)) {
                    ballAttatched = false;
                    ball.Shoot(lastXDirection);
                }
            }

            boundingRect.Location = position.ToPoint();
            CheckBallCollision();

        }

        private void CheckBallCollision() {
            if (boundingRect.Intersects(ball.GetBoudingRect())) {
                ball.Bounce();
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
