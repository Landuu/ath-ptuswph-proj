namespace ptuswph_backend.Models
{
    public class WalletTransaction
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public decimal Ammount { get; set; }

        public decimal BalanceAfter { get; set; }

        public string Description { get; set; }
    }
}
