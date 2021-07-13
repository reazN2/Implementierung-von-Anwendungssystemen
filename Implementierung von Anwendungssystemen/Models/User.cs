using System;
using System.Collections.Generic;
using System.Text;
namespace Implementierung_von_Anwendungssystemen.Models
{
    public class User
    {
        public User()
        {
            UserDistances = new List<UserDistance>();
        }
        public int Id { get; set; }

        //Validierung der Emaileingabe
        //[RegularExpression(@"^[a-zA-Z0-9._%+-]+(@student.uni-siegen.de|@unicusano.it|@unicusano.com|@student.um.si|@um.si|@hmu.gr|@vgtu.lt|@stud.vgtu.lt|@vilniustech.lt|@ipp.pt|@etu.univ-orleans.fr)$", ErrorMessage = "University mail from participating universities required")]
        public string Email { get; set; }

        public string University { get; set; }

        public string Password { get; set; }

        public string Name { get; set; }

        public string Roles { get; set; }
        public List<UserDistance> UserDistances { get; set; }

    }
}