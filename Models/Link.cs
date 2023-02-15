using System.ComponentModel.DataAnnotations;

namespace WebShortCut.Models; 

public class Link {
    public int Id { get; set; }

    [Required]
    public string Short { get; set; }

    public string Url { get; set; }
    
    public Guid UserId { get; set; }
        
    public int Count { get; set; }


    // This is Sms
}
