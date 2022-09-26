namespace AutoDaug.Models
{
    public class Car
    {
        public int Id { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public DateTime ManufactureDate { get; set; }
        public string Milage { get; set; }
        public string GasType { get; set; }
        public string Engine { get; set; }
        public string Color { get; set; }
        public string Gearbox { get; set; }
        public int Advert_Id { get; set; }
        public int User_Id { get; set; }
    }
}
