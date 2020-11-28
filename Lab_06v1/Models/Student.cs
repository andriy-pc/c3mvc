using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Lab_06v1.Models
{
    public class Student
    {
        public Student(string firstName, string secondName, int possitionInQueue)
        {
            this.possitionInQueue = possitionInQueue;
            this.firstName = firstName;
            this.secondName = secondName;
        }

        public Student() { }

        public Student(int possitionInQueue) {
            this.possitionInQueue = possitionInQueue;
        }

        public Student(string firstName, string secondName)
        {
            this.firstName = firstName;
            this.secondName = secondName;
        }

        [Key]
        public int possitionInQueue { get; set; }
        public string firstName { get; set; }
        public string secondName { get; set; }

    }
}