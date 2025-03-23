using System.ComponentModel.DataAnnotations;

namespace my_test_net.Models;

public class Employee
{
    public string Id { get; set; }
    
    [Required(ErrorMessage = "Nama karyawan harus diisi")]
    public string Name { get; set; }
    
    [Required(ErrorMessage = "Tanggal masuk kerja harus diisi")]
    [Display(Name = "Tanggal Masuk Kerja")]
    [DataType(DataType.Date)]
    public DateTime JoinDate { get; set; }
    
    [Required(ErrorMessage = "Usia harus diisi")]
    [Range(18, 65, ErrorMessage = "Usia harus antara 18 dan 65 tahun")]
    public int Age { get; set; }
}
