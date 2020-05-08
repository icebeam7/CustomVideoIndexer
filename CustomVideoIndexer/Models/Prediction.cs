using System;
namespace CustomVideoIndexer.Models
{
    public class Prediction
    {
        public string TagId { get; set; }
        public string TagName { get; set; }
        public double Probability { get; set; }
        public BoundingBox BoundingBox { get; set; }
    }
}
