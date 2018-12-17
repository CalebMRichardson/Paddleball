using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PaddleBall {
    class Sprite {

        protected Texture2D   texture;
        public Vector2        position;
        public float          width;
        public float          height;
        protected Rectangle   boundingRect;
        private bool          destroyed;

        public Sprite() {
            destroyed = false;
        }

        public void Load(string _file) {

            texture = ContentUtil.contentManger.Load<Texture2D>(_file);
            width = texture.Width;
            height = texture.Height;
        }

        public virtual void Draw(SpriteBatch spriteBatch) {
            if(!destroyed) {
                //Vector2 renderPosition = new Vector2((int)position.X, (int)position.Y);
                spriteBatch.Draw(texture, position, Color.White);

            }
        }
        public void SetPosition(Vector2 _pos) {
            position = _pos;
        }

        public void SetPosition(float _x, float _y) {
            position = new Vector2(_x, _y);
        }

        protected void SetBoudingRectPosition(Vector2 _position) {
            boundingRect.Location = _position.ToPoint();
        }

        public void Move(Vector2 _direction, float _speed, GameTime _gameTime) {

            float dt = (float)_gameTime.ElapsedGameTime.TotalSeconds;

            position += (_direction * _speed) * dt;

        }

        public void Move(float _x, float _y, float _speed, GameTime _gameTime) {

            float dt = (float)_gameTime.ElapsedGameTime.TotalSeconds;
            position.X += (_x * _speed) * dt;
            position.Y += (_y * _speed) * dt;
        }

        public void Destroy(Sprite _sprite) {
            _sprite.texture.Dispose();
            _sprite.destroyed = true;
            boundingRect = Rectangle.Empty;
        }

        public Rectangle GetBoudingRect() {
            return boundingRect;
        }

    }
}
