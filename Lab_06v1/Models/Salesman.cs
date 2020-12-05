
namespace Lab_06v1.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class Salesman
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Column("fist_name")]
        [MaxLength(20, ErrorMessage ="First name of salseman can not be longer than 20 symbols")]
        public string firstName { get; set; }

        [MaxLength(20, ErrorMessage = "Second name of salseman can not be longer than 20 symbols")]
        [Column("second_name")]
        public string secondName { get; set; }

        [Column("birth_date")]
        public Nullable<System.DateTime> birthDate { get; set; }
        public string gender { get; set; }
        public Nullable<int> store_id { get; set; }

        public Salesman(int id, string firstName, string secondName) : this(id)
        {
            this.firstName = firstName;
            this.secondName = secondName;
        }

        public Salesman(string firstName, string secondName)
        {
            this.firstName = firstName;
            this.secondName = secondName;
        }

        public Salesman(int id)
        {
            this.id = id;
        }

        public Salesman()
        {
        }

        public override string ToString()
        {
            return "Salesman {id: \"" + id + "\", first name: \""
                + firstName + "\", second name: " + secondName
                + "\", gender: \"" + gender + "\", birth date: \""
                + birthDate + "\"}";
        }

    }
}
