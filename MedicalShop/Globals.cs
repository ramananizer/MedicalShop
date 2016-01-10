using MedicalShop.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalShop
{
   public class Globals
    {
        public static ObservableCollection<Medicine> MedicinesList { get; set; }
    }
}
