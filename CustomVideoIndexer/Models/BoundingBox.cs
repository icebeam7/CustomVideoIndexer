namespace CustomVideoIndexer.Models
{
    public class BoundingBox
    {
        private int v1;
        private int v2;
        private int v3;
        private int v4;

        public BoundingBox(int v1, int v2, int v3, int v4)
        {
            this.v1 = v1;
            this.v2 = v2;
            this.v3 = v3;
            this.v4 = v4;
        }

        public double Left { get; set; }
        public double Top { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
    }
}
