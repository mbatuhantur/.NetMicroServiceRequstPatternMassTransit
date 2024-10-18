namespace Messaging.CustomerCredit
{
    // hesap için bir miktar kredi çekmek için onay var mı
    public record GetCreditScoreRequest(string accountNumber, decimal requestAmount);
    
}
