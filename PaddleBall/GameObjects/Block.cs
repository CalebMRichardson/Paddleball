using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PaddleBall {
    class Block : Sprite {

        public Block() {
            Load("assets/block_64x32_px");

            SetPosition(PaddleBall.WIDTH / 2, PaddleBall.HEIGHT / 2);

        }

        public void Update(GameTime _gameTime) {

            if (Keyboard.GetState().IsKeyDown(Keys.I)) {
                Destroy();
            }

        }

    }
}
