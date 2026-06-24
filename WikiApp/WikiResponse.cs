using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections.Generic;

namespace WikiApp
{
    // Τα ονόματα των μεταβλητών πρέπει να ταιριάζουν ακριβώς με τα πεδία του JSON.
    public class WikiResponse
    {
        // Ο τίτλος του λήμματος
        public string title { get; set; }

        // Η σύνοψη του κειμένου (αυτό που θα διαβάσει φωνητικά)
        public string extract { get; set; }

        // Το αντικείμενο που περιέχει πληροφορίες για την εικόνα (μπορεί να είναι null αμα δεν βρει καποια)
        public WikiImage thumbnail { get; set; }

        // Το αντικείμενο που περιέχει τα URL 
        public WikiUrls content_urls { get; set; }
    }

    // Βοηθητική κλάση για την εικόνα
    public class WikiImage
    {
        // Το απευθείας URL της εικόνας (.jpg/.png)
        public string source { get; set; }
    }

    // Βοηθητική κλάση για τα URLs
    public class WikiUrls
    {
        public WikiDesktop desktop { get; set; }
    }

    // Βοηθητική κλάση για το Link της σελίδας
    public class WikiDesktop
    {
        // Το πλήρες Link για να το αποθηκεύσουμε στα αγαπημένα
        public string page { get; set; }
    }
}