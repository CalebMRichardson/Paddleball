using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PaddleBall {
    class GameScreen : Screen {

        private Paddle         paddle;
        private Ball           ball;
        private LevelBuilder   builder;
        private List<Block>    blocks; 

        public GameScreen(ScreenManager _screenManager) 
            : base(_screenManager) {

            paddle = new Paddle();
            ball = new Ball(paddle);
            paddle.SetChild(ball);
            builder = new LevelBuilder();
            // TODO allow save progression
            // Load level from file
            builder.SetLevel(2);
            builder.Build();
            blocks = builder.GetLevelBlocks();
            foreach(Block block in blocks) {
                block.SetBallObject(ball);
                block.SetPaddleObject(paddle);
            }
        }



        public override void Update(GameTime _gameTime) {
            paddle.Update(_gameTime);
            ball.Update(_gameTime);

            for (int i = 0; i < blocks.Count; i++) {
                Block currentBlock = blocks[ i ];
                currentBlock.Update(_gameTime);
                if(currentBlock.outOfGame) {
                    blocks.Remove(currentBlock);
                }
            }
        }

        public override void Draw(SpriteBatch _spriteBatch) {
            paddle.Draw(_spriteBatch);
            ball.Draw(_spriteBatch);
            foreach(Block block in blocks) {
                block.Draw(_spriteBatch);
            }
        }

        public override void Dispose() {
            // TODO handle Disposing >:]
        }
    }
}
