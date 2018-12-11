using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PaddleBall {
    class Sprite {

        protected Texture2D texture;
        public Vector2 position;
        public float width;
        public float height;
        protected Rectangle boundingRect;

        public void Load(string _file) {

            texture = ContentUtil.contentManger.Load<Texture2D>(_file);
            width = texture.Width;
            height = texture.Height;
        }

        public void Draw(SpriteBatch spriteBatch, GameTime _gameTime) {
            spriteBatch.Draw(texture, position, Color.White);
        }

        public void SetPosition(Vector2 _pos) {
            position = _pos;
        }

        public void SetPosition(float _x, float _y) {
            position = new Vector2(_x, _y);
        }

        public void Move(Vector2 _direction, float _speed, GameTime _gameTime) {

            float dt = (float)_gameTime.ElapsedGameTime.TotalSeconds;

            position += (_direction * _speed) * dt; 

        }

        public void Move(float _x, float _y, float _speed, GameTime _gameTime) {

            float dt = (float)_gameTime.ElapsedGameTime.TotalSeconds;
            position.X += ( _x * _speed) * dt;
            position.Y += ( _y * _speed ) * dt; 
        }

        public Rectangle GetBoudingRect() {
            return boundingRect;
        }

    }
}
