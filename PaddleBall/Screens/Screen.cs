using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PaddleBall {
    abstract class Screen {

        protected ScreenManager screenManager; 

        public Screen(ScreenManager _screenManager) {
            screenManager = _screenManager; 
        }


        public abstract void Update(GameTime _gameTime);
        public abstract void Draw(SpriteBatch _spriteBatch);
        public abstract void Dispose(); 
    }
}
