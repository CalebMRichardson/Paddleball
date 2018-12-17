using System;
using System.Collections.Generic; 
using System.Diagnostics;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace PaddleBall {

    class LevelBuilder {

        private const string   LEVEL = "level";
        private int            currentLevel = -1;
        private const int      LEVELWIDTH = 12;
        private const int      LEVELHEIGHT = 10;
        private JObject        levelData;
        private List<Block>    blocks; 

        public LevelBuilder() {
            LoadLevelData();
            blocks = new List<Block>();
        }

        public void SetLevel(int _level) {
            currentLevel = _level;
        }

        private const int BLOCK_WIDTH  = 64;
        private const int BLOCK_HEIGHT = 32;
        private const int SIDE_BUFFER  = 16; 

        public void Build() {

            if (currentLevel == -1) {
                //TODO throw error LEVEL_NOT_SET
                return; 
            }

            StringBuilder levelName = new StringBuilder(LEVEL);
            levelName.Append(currentLevel.ToString());

            if (!levelData.ContainsKey(levelName.ToString())) {
                Debug.WriteLine("No level by name: " + levelName.ToString());
                return; 
            }

            JArray jArr = (JArray)JsonConvert.DeserializeObject(levelData[levelName.ToString()].ToString());
            for(int i = 0; i < LEVELHEIGHT; i++) {
                for(int j = 0; j < LEVELWIDTH; j++) {

                    float xPos = SIDE_BUFFER + (j * BLOCK_WIDTH);
                    float yPos = SIDE_BUFFER + (i * BLOCK_HEIGHT);

                    if (jArr[i][j].ToString() == "1") {
                        Block block = new Block();
                        block.SetPosition(xPos, yPos);
                        blocks.Add(block);
                    }

                }
            }
        }

        private void LoadLevelData() {

            string levelDataPath = ContentUtil.contentManger.RootDirectory + "/assets/levels.json";
            if (File.Exists(levelDataPath)) {
                using(StreamReader r = new StreamReader(levelDataPath)) {
                    string s = r.ReadToEnd();
                    levelData = JObject.Parse(s);
                    Debug.WriteLine("Level data found at: " + levelDataPath);
                }
            } else {
                Debug.WriteLine("Cannot find levels.json at path: " + levelDataPath);
            }
        }

        public List<Block> GetLevelBlocks() {
            return blocks; 
        }

    }
}
