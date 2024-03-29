namespace maturita_rpg
{
    class Map
    {
        public Coord[,] walls;
        public List<GameObject> gameObjects;
        public int mapHeight;
        public int mapWidth;

        public Map(Coord[,] walls)
        {
            this.walls = walls;
            gameObjects = new List<GameObject>();
            mapHeight = walls.GetLength(0);
            mapWidth = walls.GetLength(1);
        }
    }
}
