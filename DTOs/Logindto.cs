namespace Book_store_1_.DTOs
{
    public class Logindto
    {
        [Required]
        public string? Username { get; set; }
        [Required]
        public string? Email { get; set; }   
        
        [Required]
        public string? Password { get; set; }
  
    }
}