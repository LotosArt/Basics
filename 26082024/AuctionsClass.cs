using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace LibLesson._26082024;

public class AuctionsClass
{
    static string? _connectionString = "";
    
    public static void Main(string[] args)
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json");
        IConfiguration configuration = builder.Build();
        _connectionString = configuration.GetConnectionString("DefaultConnectionAuction");
        
        int auctionID = 1;   
        int bidderID = 2;    
        decimal bidAmount = 250.00m; 

        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            SqlTransaction transaction = connection.BeginTransaction();

            SqlCommand command = connection.CreateCommand();
            command.Transaction = transaction;
            
            try
            {
                string checkAuctionSql = "SELECT CurrentBid, AuctionEndTime FROM Auctions WHERE AuctionID = @AuctionID";
                SqlCommand checkAuctionCmd = new SqlCommand(checkAuctionSql, connection, transaction);
                checkAuctionCmd.Parameters.AddWithValue("@AuctionID", auctionID);

                SqlDataReader reader = checkAuctionCmd.ExecuteReader();
                if (reader.Read())
                {
                    decimal currentBid = reader.GetDecimal(0);
                    DateTime auctionEndTime = reader.GetDateTime(1);

                    reader.Close();

                    if (bidAmount > currentBid && DateTime.Now < auctionEndTime)
                    {
                        string updateAuctionSql = "UPDATE Auctions SET CurrentBid = @BidAmount WHERE AuctionID = @AuctionID";
                        SqlCommand updateAuctionCmd = new SqlCommand(updateAuctionSql, connection, transaction);
                        updateAuctionCmd.Parameters.AddWithValue("@BidAmount", bidAmount);
                        updateAuctionCmd.Parameters.AddWithValue("@AuctionID", auctionID);

                        updateAuctionCmd.ExecuteNonQuery();

                        string insertBidSql = "INSERT INTO Bids (AuctionID, BidderID, BidAmount, BidTime) VALUES (@AuctionID, @BidderID, @BidAmount, @BidTime)";
                        SqlCommand insertBidCmd = new SqlCommand(insertBidSql, connection, transaction);
                        insertBidCmd.Parameters.AddWithValue("@AuctionID", auctionID);
                        insertBidCmd.Parameters.AddWithValue("@BidderID", bidderID);
                        insertBidCmd.Parameters.AddWithValue("@BidAmount", bidAmount);
                        insertBidCmd.Parameters.AddWithValue("@BidTime", DateTime.Now);

                        insertBidCmd.ExecuteNonQuery();
                    }
                    else
                    {
                        Console.WriteLine("Bid cannot be placed. Current bid is lower or auction is over.");
                    }
                }
                else
                {
                    Console.WriteLine("Auction not found.");
                }
            }
            catch (Exception e)
            {
                transaction.Rollback();
                Console.WriteLine("Error executing transaction: " + e.Message);
            }
            finally
            {
                transaction.Commit();
                Console.WriteLine("The bet was successfully placed.");
            }
        }

    }
}