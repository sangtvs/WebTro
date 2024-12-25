using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;

namespace testt.Models
{
    public class Tro
    {
        [Key] public int Id { get; set; }
        public string ten { get; set; }
        public string dientich { get; set; }
        public string giatien { get; set; }
        public string diachi { get; set; }
        public byte[] anh { get; set; }
        public string sdt { get; set; }
        public string mota { get; set; }
        public IFormFile AnhPhong { get; set; }
    }

}
