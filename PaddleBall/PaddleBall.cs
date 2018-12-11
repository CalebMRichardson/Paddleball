using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PaddleBall {
    public class PaddleBall : Game {

        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private Paddle paddle;
        private Ball ball; 

        public  const int WIDTH   = 800;
        public const int HEIGHT  = 600;

        public PaddleBall() {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = WIDTH;
            graphics.PreferredBackBufferHeight = HEIGHT;
            graphics.ApplyChanges();
            this.IsMouseVisible = true;
            Content.RootDirectory = "Content";
            ContentUtil.contentManger = Content;
        }

        protected override void Initialize() {

            base.Initialize();
        }


        protected override void LoadContent() {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            paddle = new Paddle();
            ball = new Ball(paddle);

            float paddleStartingXPos = (WIDTH / 2) - (paddle.width / 2);
            float paddleStartingYPos = HEIGHT - paddle.height;

            paddle.SetPosition(paddleStartingXPos, paddleStartingYPos);
            paddle.SetChild(ball);

        }

        protected override void UnloadContent() {
            Content.Unload();
        }

        protected override void Update(GameTime _gameTime) {
            if(GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            paddle.Update(_gameTime);
            ball.Update(_gameTime);

            base.Update(_gameTime);
        }

        protected override void Draw(GameTime _gameTime) {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            paddle.Draw(spriteBatch, _gameTime);
            ball.Draw(spriteBatch, _gameTime);
            spriteBatch.End();


            base.Draw(_gameTime);
        }
    }
}
