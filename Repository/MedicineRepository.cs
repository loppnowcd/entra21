using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FarmaciaExercicio.Entities;

namespace FarmaciaExercicio.Repository
{
    public class MedicineRepository //sao os metodos que salvam dados na lista do repositorio. nao é a regra do negocio exatamente
    {
        private List<Medicine> _medicines = new List<Medicine>();

        public void AddMedicine(Medicine medicine)
        {
            _medicines.Add(medicine);
        }

        // Atualiza um medicamento procurando pelo ID - ProductID
        public Medicine UpdateMedicine(Medicine updatedMedicine)
        {
            var medicine = _medicines.FirstOrDefault(m => m.ProductID == updatedMedicine.ProductID);

            if (medicine != null)
            {
                medicine.Name = updatedMedicine.Name;
                medicine.Price = updatedMedicine.Price;
                medicine.Expiration = updatedMedicine.Expiration;
                medicine.Category = updatedMedicine.Category;
                medicine.Weight = updatedMedicine.Weight;
                medicine.Laboratory = updatedMedicine.Laboratory;

                return medicine;
            }

            else
            {
                throw new Exception("Medicamento não encontrado.");
            }

        }

        public List<Medicine> ListMedicines()
        {
            return _medicines;
        }

        public Medicine SearchByID(string productId)
        {
            return _medicines.FirstOrDefault(m => m.ProductID == productId);
        }
    }
}
