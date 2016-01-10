using MedicalShop.Entities;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalShop
{
    public class StartUpActions
    {
        public static async void LoadMedicinesList()
        {
            var obj = await StartUpActions.MedicineContext.Medicines.Find<Medicine>(new BsonDocument()).ToListAsync();
            Globals.MedicinesList = new System.Collections.ObjectModel.ObservableCollection<Medicine>(obj);

        }

        [Import]
        private static MedicineContext MedicineContext
        {
            get;set;
        }
    }
}
