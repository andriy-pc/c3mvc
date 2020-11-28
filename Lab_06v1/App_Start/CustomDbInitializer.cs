using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Lab_06v1.App_Start
{
    public class CustomDbInitializer : DropCreateDatabaseAlways<EntitiesContext>
    {

        protected override void Seed(EntitiesContext context)
        {
            context.Students.Add(new Models.Student("Student#1", "SecondName#1", 1));
            context.Students.Add(new Models.Student("Student#2", "SecondName#2", 2));
            context.Students.Add(new Models.Student("Student#3", "SecondName#3", 3));
        }

    }
}