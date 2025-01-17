using System.ComponentModel.DataAnnotations;
public class Sonuc
{
    [Key]
    public int SonucID { get; set; }
    public int CekilisID { get; set; }
    public int KatilimciID { get; set; }
    public string SonucTipi { get; set; }
    public Cekilis Cekilis { get; set; } 
    public Katilimci Katilimci { get; set; } 
}
