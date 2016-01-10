using MedicalShop.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Configuration;
using System.Linq;
using System.Web;

namespace MedicalShop
{
    [Export]
    public class MedicineContext
    {
        public const string CONNECTION_STRING_NAME = "MedicalShop";
        public const string DATABASE_NAME = "MedicalShop";
        //public const string POSTS_COLLECTION_NAME = "posts";
        public const string MEDICINE_COLLECTION_NAME = "medicine";
        //public const string ENROLLMENT_COLLECTION_NAME = "enrollments";
        //public const string COURSE_COLLECTION_NAME = "courses";

        // This is ok... Normally, they would be put into
        // an IoC container.
        private static readonly IMongoClient _client;
        private static readonly IMongoDatabase _database;

        static MedicineContext()
        {
            var connectionString = ConfigurationManager.ConnectionStrings[CONNECTION_STRING_NAME].ConnectionString;
            _client = new MongoClient(connectionString);
            _database = _client.GetDatabase(DATABASE_NAME);
        }

        public IMongoClient Client
        {
            get { return _client; }
        }

        //public IMongoCollection<Post> Posts
        //{
        //    get { return _database.GetCollection<Post>(POSTS_COLLECTION_NAME); }
        //}

        public IMongoCollection<Medicine> Medicines
        {
            get { return _database.GetCollection<Medicine>(MEDICINE_COLLECTION_NAME); }
        }

        //public IMongoCollection<Enrollment> Enrollments
        //{
        //    get
        //    {
        //        return _database.GetCollection<Enrollment>(ENROLLMENT_COLLECTION_NAME);
        //    }
        //}

        //public IMongoCollection<Course> Courses
        //{
        //    get
        //    {
        //        return _database.GetCollection<Course>(COURSE_COLLECTION_NAME);
        //    }
        //}
    }
}