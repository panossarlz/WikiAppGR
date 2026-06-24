
using System;
using System.Drawing; // Για τη διαχείριση εικόνων
using System.Net.Http; // Για τις κλήσεις στο Internet (API)
using System.Speech.Synthesis; // Για την ομιλία
using System.Windows.Forms;
using Newtonsoft.Json; // Για την ανάγνωση του JSON

namespace WikiApp
{
    public partial class Form1 : Form
    {
        // Δηλώνουμε τα εργαλεία μας
        private readonly DatabaseHelper _dbHelper;
        private SpeechSynthesizer _synthesizer;
        private string _currentUrl = ""; // Εδώ θα κρατάμε προσωρινά το Link του άρθρου

        public Form1()
        {
            InitializeComponent(); // Απαραίτητο: Φτιάχνει τα κουμπιά στην οθόνη

            // Σημαντική λεπτομέρεια 
            // Ενεργοποιούμε το πρωτόκολλο ασφαλείας TLS 1.2, αλλιώς η Wikipedia θα μας μπλοκάρει.
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;

            // Αρχικοποιούμε τη βάση και τη φωνή
            _dbHelper = new DatabaseHelper();
            _synthesizer = new SpeechSynthesizer();

            // Φορτώνουμε τα αγαπημένα μόλις ανοίξει η εφαρμογή
            LoadFavorites();
        }

        //  Αναζήτηση (API) 
        private async void btnSearch_Click(object sender, EventArgs e)
        {
            // 1. Παίρνουμε το κείμενο και καθαρίζουμε τα κενά
            string term = txtSearch.Text.Trim();
            if (string.IsNullOrEmpty(term)) return; // Αν είναι κενό, δεν κάνουμε τίποτα

            // 2. Καθαρισμός UI: Σβήνουμε παλιά εικόνα/κείμενο για να μην μπερδευτεί ο χρήστης
            pictureBox1.Image = null;
            lblTitle.Text = "Searching...";
            rtbExtract.Text = "";

            // 3. Δημιουργία του URL για το API
            string url = "https://el.wikipedia.org/api/rest_v1/page/summary/" + term;

            using (HttpClient client = new HttpClient())
            {
                // Προσθήκη User-Agent (Ταυτότητα) γιατί το απαιτεί η Wikipedia
                client.DefaultRequestHeaders.Add("User-Agent", "MyWikiApp/1.0");

                try
                {
                    // 4. Κλήση API (Async = δεν παγώνει η οθόνη)
                    HttpResponseMessage response = await client.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        // 5. Αν όλα πήγαν καλά, διαβάζουμε το JSON
                        string json = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<WikiResponse>(json);

                        // 6. Ενημέρωση Οθόνης
                        lblTitle.Text = result.title;
                        rtbExtract.Text = result.extract;
                        _currentUrl = result.content_urls.desktop.page; // Κρατάμε το Link

                        // 7. Διαχείριση Εικόνας
                        if (result.thumbnail != null)
                        {
                            try
                            {
                                // Κατεβάζουμε την εικόνα με τον ίδιο Client (ασφαλής σύνδεση)
                                var imageResponse = await client.GetAsync(result.thumbnail.source);
                                using (var stream = await imageResponse.Content.ReadAsStreamAsync())
                                {
                                    pictureBox1.Image = Image.FromStream(stream);
                                }
                            }
                            catch
                            {
                                pictureBox1.Image = null; // Αν αποτύχει η λήψη εικόνας
                            }
                        }
                    }
                    else
                    {
                        lblTitle.Text = "Not Found";
                        MessageBox.Show("The article was not found.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Connection Error: {ex.Message}");
                }
            }
        }

        // Ομιλία (TTS) 
        private void btnListen_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(rtbExtract.Text)) return;

            // Λειτουργία Toggle: Αν μιλάει ήδη, το σταματάμε
            if (_synthesizer.State == SynthesizerState.Speaking)
            {
                _synthesizer.SpeakAsyncCancelAll();
                return;
            }

            // Επιλογή Ελληνικής Φωνής
            bool voiceFound = false;
            foreach (var voice in _synthesizer.GetInstalledVoices())
            {
                // Ψάχνουμε για "Stefanos" ή γενικά Ελληνικά "el-GR"
                if (voice.VoiceInfo.Name.Contains("Stefanos") || voice.VoiceInfo.Culture.Name == "el-GR")
                {
                    _synthesizer.SelectVoice(voice.VoiceInfo.Name);
                    voiceFound = true;
                    break;
                }
            }

            if (!voiceFound)
            {
                MessageBox.Show("No greek voice option found. Default option will be selected");
            }

            // Έναρξη ομιλίας (Async)
            _synthesizer.SpeakAsync(rtbExtract.Text);
        }

        // Αποθήκευση (DB) 
        private void btnSave_Click(object sender, EventArgs e)
        {
            // Ελέγχουμε αν έχουμε κάνει αναζήτηση πρώτα
            if (!string.IsNullOrEmpty(lblTitle.Text) && !string.IsNullOrEmpty(_currentUrl))
            {
                _dbHelper.AddFavorite(lblTitle.Text, _currentUrl);
                LoadFavorites(); // Ανανεώνουμε τον πίνακα
                MessageBox.Show("Added to Favorites.");
            }
            else
            {
                MessageBox.Show("No article found to save.");
            }
        }

        // Βοηθητικές Μεθόδοι 

        // Φόρτωση δεδομένων από τη βάση στο Grid
        private void LoadFavorites()
        {
            dgvFavorites.DataSource = _dbHelper.GetFavorites();

            // Κρύβουμε τη στήλη ID (είναι χρήσιμη για τη βάση, αλλά άχρηστη για τον χρήστη)
            if (dgvFavorites.Columns["Id"] != null)
                dgvFavorites.Columns["Id"].Visible = false;

            // Βελτίωση εμφάνισης στηλών
            if (dgvFavorites.Columns["Title"] != null)
                dgvFavorites.Columns["Title"].HeaderText = "Title";
        }

        // Προαιρετικό: Διαγραφή με κουμπί
        private void btnDelete_Click(object sender, EventArgs e)
        {
            // Ελέγχουμε αν ο χρήστης έχει επιλέξει γραμμή
            if (dgvFavorites.SelectedRows.Count > 0)
            {
                // Παίρνουμε το κρυφό ID της επιλεγμένης γραμμής
                int id = Convert.ToInt32(dgvFavorites.SelectedRows[0].Cells["Id"].Value);
                _dbHelper.DeleteFavorite(id); // Διαγραφή από βάση
                LoadFavorites(); // Ανανέωση πίνακα
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {

        }
    }
}