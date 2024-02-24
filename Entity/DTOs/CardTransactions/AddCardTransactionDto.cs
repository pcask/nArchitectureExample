namespace Entity.DTOs.CardTransactions;

public class AddCardTransactionDto
{
    public Guid CardId { get; set; }
    public decimal Balance { get; set; }
}
