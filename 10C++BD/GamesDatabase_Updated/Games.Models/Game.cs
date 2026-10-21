namespace Games.Models;

public class Game
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string Studio { get; set; } = "";
    public string Genre { get; set; } = "";
    public DateTime ReleaseDate { get; set; }

    // Добавлено в задании 4
    public string Mode { get; set; } = "Однопользовательский";
    public int SoldCopies { get; set; }
}
