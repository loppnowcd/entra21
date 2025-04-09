using meu_segundo_projeto.Enum;

namespace FarmaciaExercicio.Entities
{
    public class Medicine
    {
        public string Name { get; set; }
        public string Laboratory { get; set; }
        public DateTime Expiration { get; set; }
        public string ProductID { get; set; }
        public double Weight { get; set; }
        public double Price { get; set; }
        public Category Category { get; set; }

    }
}
