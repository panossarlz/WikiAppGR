using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WikiApp
{
    using System;
    using System.Data;
    using System.Data.SQLite;
    using System.IO;


    public class DatabaseHelper
    {
        // Η συμβολοσειρά σύνδεσης. Ορίζει το όνομα και την έκδοση της βάσης.
        private const string ConnectionString = "Data Source=favorites.db;Version=3;";

        // Constructor: Καλείται αυτόματα μόλις δημιουργηθεί αντικείμενο αυτής της κλάσης.
        public DatabaseHelper()
        {
            InitializeDatabase();
        }

        // Δημιουργία αρχείου και πίνακα (αν δεν υπάρχουν)
        private void InitializeDatabase()
        {
            // Βήμα 1: Αν δεν υπάρχει το αρχείο .db στον δίσκο, το δημιουργούμε.
            if (!File.Exists("favorites.db"))
            {
                SQLiteConnection.CreateFile("favorites.db");
            }

            // Βήμα 2: Συνδεόμαστε για να φτιάξουμε τον πίνακα.
            // Το 'using' διασφαλίζει ότι η σύνδεση θα κλείσει αυτόματα, ακόμα και αν γίνει σφάλμα.
            using (var conn = new SQLiteConnection(ConnectionString))
            {
                conn.Open(); // Άνοιγμα σύνδεσης

                // SQL Εντολή: Φτιάξε τον πίνακα Favorites ΜΟΝΟ ΑΝ ΔΕΝ ΥΠΑΡΧΕΙ ΗΔΗ.
                // ID: Αυτόματος αριθμός, Title: Κείμενο, Url: Κείμενο.
                string sql = "CREATE TABLE IF NOT EXISTS Favorites (Id INTEGER PRIMARY KEY AUTOINCREMENT, Title TEXT, Url TEXT)";

                using (var cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.ExecuteNonQuery(); // Εκτέλεση εντολής (δεν επιστρέφει δεδομένα)
                }
            }
        }

        // Προσθήκη νέου αγαπημένου
        public void AddFavorite(string title, string url)
        {
            using (var conn = new SQLiteConnection(ConnectionString))
            {
                conn.Open();
                // Χρησιμοποιούμε παραμέτρους (@title, @url) για ασφάλεια (SQL Injection protection).
                string sql = "INSERT INTO Favorites (Title, Url) VALUES (@title, @url)";
                using (var cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@title", title);
                    cmd.Parameters.AddWithValue("@url", url);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Ανάκτηση όλων των αγαπημένων για εμφάνιση στο Grid
        public DataTable GetFavorites()
        {
            using (var conn = new SQLiteConnection(ConnectionString))
            {
                conn.Open();
                string sql = "SELECT * FROM Favorites";

                // Ο Adapter λειτουργεί σαν γέφυρα: Παίρνει τα δεδομένα από τη βάση και γεμίζει έναν πίνακα (DataTable).
                using (var adapter = new SQLiteDataAdapter(sql, conn))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt); // Γέμισμα του πίνακα
                    return dt; // Επιστροφή των δεδομένων στη φόρμα
                }
            }
        }

        // Διαγραφή αγαπημένου βάσει ID
        public void DeleteFavorite(int id)
        {
            using (var conn = new SQLiteConnection(ConnectionString))
            {
                conn.Open();
                string sql = "DELETE FROM Favorites WHERE Id = @id";
                using (var cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}