using System.ComponentModel.DataAnnotations;

public class Katilimci
{
    [Key]
    public int KatilimciID { get; set; }
    public int CekilisID { get; set; }
    public string AdSoyad { get; set; }
    public Cekilis? Cekilis { get; set; }
}
