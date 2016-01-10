using MedicalShop.Entities;
using Microsoft.Practices.Prism.Commands;
//using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace MedicalShop
{
   public class MainWindowViewModel 
    {
        public ObservableCollection<Medicine> MedicineList { get; set; }

        private ICommand importMedicinesCommand;
        public ICommand ImportMedicinesCommand
        {
            get
            {
                importMedicinesCommand = importMedicinesCommand ?? new DelegateCommand(OnImportMedicineCommand);
                return importMedicinesCommand;
            }
        }

        private void OnImportMedicineCommand()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            DialogResult result = ofd.ShowDialog();
            string fileName = ofd.FileName;
            Task.Factory.StartNew(new Action<object>(ImportFileIntoDatabase), fileName);
        }

        private async void ImportFileIntoDatabase(object fileName)
        {
            FileStream fs = null;
            StreamReader sr = null;
            try
            {
                fs = File.Open((string)fileName, FileMode.Open);
                sr = new StreamReader(fs);
                string contents = sr.ReadToEnd();
                string[] contentLines = contents.Split(new char[] { '\n' });
                MedicineContext context = new MedicineContext();
                if (ValidateFile(contentLines[0]))
                {
                    for (int i = 1; i < contentLines.Length; i++)
                    {
                        string[] medicineInfos = contentLines[i].Split(',');
                        Medicine obj = new Entities.Medicine
                        {
                            Id = medicineInfos[0],
                            Name = medicineInfos[1],
                            ManufacturingDate = DateTime.Parse(medicineInfos[2]),
                            ExpirationDate = DateTime.Parse(medicineInfos[3])
                        };
                        MedicineList.Add(obj);
                        await context.Medicines.InsertOneAsync(obj);

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                fs.Close();
                sr.Close();
            }
        }

        private bool ValidateFile(string firstLineOfContent)
        {
            if (firstLineOfContent.Split(',').Length < 4)
            {
                return false;
            }
            return true;
        }
    }
}
