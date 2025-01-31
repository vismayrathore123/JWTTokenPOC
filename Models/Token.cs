using System;

public class Token
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public string JwtToken { get; set; }
    public DateTime ExpiryDate { get; set; }  
}
