using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace PaddleBall {

    class Ball : Sprite {

        private float          moveSpeed;
        private Vector2        movementVec;
        private Paddle         paddle;
        private SoundEffect    collide;
        private int            trailSize;
        private List<Vector2>  trailPositions;
        private Vector2        lastPosition;
        private int            trailDistance;
        private bool           ballMoving;
        private bool           drawTrail; 

        public Ball(Paddle _paddle) {
            Load("assets/ball_16x16_px");
            moveSpeed = 300.0f;
            movementVec = new Vector2();
            boundingRect = new Rectangle(0, 0, ( int )width, ( int )height);
            paddle = _paddle;
            collide = ContentUtil.contentManger.Load<SoundEffect>("assets/collision");
            SoundEffect.MasterVolume = 0.1f;
            trailSize = 5;
            trailPositions = new List<Vector2>();
            lastPosition = position;
            trailDistance = 8;
            ballMoving = false;
            drawTrail = false;
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

            ballMoving = true;

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

        public override void Draw(SpriteBatch _spriteBatch) {

            _spriteBatch.Draw(texture, position, Color.White);

            if(drawTrail) {
                float alphaVal = .60f;
                if(ballMoving) {
                    Color customBallColor = new Color(new Vector4(0, 0, 0, alphaVal));
                    foreach(Vector2 trailPosition in trailPositions) {
                        _spriteBatch.Draw(texture, trailPosition, customBallColor);
                        alphaVal -= .10f;
                    }
                }
            }
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
                ballMoving = false;
                trailPositions.Clear();

            } 
            if (position.Y <= 0) {
                movementVec.Y = 1.0f;
                Collide();
            }
            
            Move(movementVec, moveSpeed, _gameTime);
            boundingRect.Location = position.ToPoint();
            

            if (ballMoving && drawTrail)
                UpdateTrailPosition();
        }

        private void UpdateTrailPosition() {
            if(Vector2.Distance(position, lastPosition) > trailDistance) {
                trailPositions.Insert(0, new Vector2((int)lastPosition.X, (int)lastPosition.Y));
                lastPosition = position;

                if (trailPositions.Count > trailSize) {
                    trailPositions.RemoveAt(trailSize);
                }
            }
        }
    }
}
