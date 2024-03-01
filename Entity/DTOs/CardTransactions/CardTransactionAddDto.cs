namespace Entity.DTOs.CardTransactions;

public class CardTransactionAddDto
{
    public Guid CardId { get; set; }
    public decimal Balance { get; set; }
}
