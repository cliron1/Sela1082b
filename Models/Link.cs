using System.ComponentModel.DataAnnotations;

namespace WebShortCut.Models; 

public class Link {
    public int Id { get; set; }

    [Required]
    public string Url { get; set; }

    public string Short { get; set; }
    
    public Guid UserId { get; set; }
        
    public int Count { get; set; }
}
