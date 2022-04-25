namespace USTAPG.Models
{
    using SQLite;

    public class Usuario
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string User { get; set; }
        public string Clave { get; set; }
        public string MAC { get; set; }
    }
}
