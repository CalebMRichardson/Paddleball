using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace PaddleBall {
    class ScreenManager {

        private Stack<Screen>   screens; 

        public ScreenManager() {
            screens = new Stack<Screen>();
        }

        public void Push(Screen _screen) {
            screens.Push(_screen); 
        }

        public void Pop() {
            screens.Pop().Dispose();
        }

        public void Set(Screen _screen) {
            screens.Pop().Dispose();
            screens.Push(_screen);
        }

        public void Update(GameTime _gameTime) {
            screens.Peek().Update(_gameTime);
        }

        public void Draw(SpriteBatch _spriteBatch) {
            screens.Peek().Draw(_spriteBatch);
        }
    }
}
