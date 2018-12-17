using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace PaddleBall {
    class Block : Sprite {

        private Ball          ball;
        private Paddle        paddle;
        private SoundEffect   kaching;
        public bool           hit;
        public bool           canHit; 
        public bool           outOfGame;
        private float         collisionDelay;
        private float         remainingDelay; 

        public Block() {
            Load("assets/block_64x32_px");

            SetPosition(PaddleBall.WIDTH / 2, PaddleBall.HEIGHT / 2);
            boundingRect = new Rectangle(0, 0, (int)width, (int)height);
            boundingRect.Location = position.ToPoint();
            hit = false;
            outOfGame = false;
            canHit = true;

            collisionDelay = 250f; // miliseconds
            remainingDelay = collisionDelay;

            kaching = ContentUtil.contentManger.Load<SoundEffect>("assets/kaching");
        }

        public void SetBallObject(Ball _ball) {
            ball = _ball; 
        }

        public void SetPaddleObject(Paddle _paddle) {
            paddle = _paddle;
        }

        public void Update(GameTime _gameTime) {
            SetBoudingRectPosition(position);

            if(canHit) {
                CheckBallCollision();
            } else {
                var timer = (float) _gameTime.ElapsedGameTime.TotalMilliseconds;
                remainingDelay -= timer; 
            }

            if (remainingDelay <= 0) {
                canHit = true;
                remainingDelay = collisionDelay;
            }

            if (hit && !outOfGame) {
                Fall(_gameTime);
            }

            if (position.Y > PaddleBall.HEIGHT + height) {
                outOfGame = true;
            }
        }

        private void CheckPaddleCollision() {
            if (boundingRect.Intersects(paddle.GetBoudingRect())) {
                kaching.CreateInstance().Play();
                outOfGame = true;
            }
        }

        private Vector2 down = new Vector2(0, 1);

        private void Fall(GameTime _gameTime) {
            Move(down, 200f, _gameTime);
            CheckPaddleCollision();
        }

        private void CheckBallCollision() {
          
            if (boundingRect.Intersects(ball.GetBoudingRect())) {
                Vector2 newDirection = ball.GetMovementVec(); ;
                newDirection.Y *= -1; 
                ball.Bounce(newDirection);
                hit = true;
                canHit = false;
            }
        }

    }
}
