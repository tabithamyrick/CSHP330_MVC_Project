using MiniStructorDB;
using System;

namespace MiniStructorRepository
{
    public class DatabaseManager
    {

        private static readonly minicstructorContext entities;

        // Initialize and open the database connection
        static DatabaseManager()
        {
            entities = new minicstructorContext();
        }

        // Provide an accessor to the database
        public static minicstructorContext Instance
        {
            get
            {
                return entities;
            }
        }
    }
}

