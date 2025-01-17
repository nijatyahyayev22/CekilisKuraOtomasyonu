using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

public class Kullanici
{
    [Key]
    public int KullaniciID { get; set; }
    public string AdSoyad { get; set; }
    public string Email { get; set; }
    public string Sifre { get; set; }
    public ICollection<Cekilis>? Cekilisler { get; set; }
}
