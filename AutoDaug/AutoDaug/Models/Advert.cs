namespace AutoDaug.Models
{
    public class Advert
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int Advert_Id { get; set; }
        public int User_Id { get; set; }
    }
}
