using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace my_test_net.Models
{
    [Table("Karyawan")]
    public class Karyawan
    {
        [Key]
        [Column("IdKaryawan")]
        public string Id { get; set; }

        [Required]
        [Column("NmKaryawan")]
        public string Name { get; set; }

        [Column("TglMasukKerja", TypeName = "datetime")]
        [DataType(DataType.Date)]
        public DateTime JoinDate { get; set; }

        [Column("Usia")]
        public int Age { get; set; }
    }
}
