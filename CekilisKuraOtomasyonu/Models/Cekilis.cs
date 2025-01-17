using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

public class Cekilis
{
    [Key]
    public int CekilisID { get; set; }
    public int? KullaniciID { get; set; }
    public string CekilisAdi { get; set; }
    public DateTime? OlusturmaTarihi { get; set; } = DateTime.Now;
    public int KazananSayisi { get; set; }
    public int YedekSayisi { get; set; }
    public Kullanici? Kullanici { get; set; }
    public ICollection<Katilimci>? Katilimcilar { get; set; } 
    public ICollection<Sonuc>? Sonuclar { get; set; }
}
