using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FarmaciaExercicio.Entities;
using FarmaciaExercicio.Repository;

namespace FarmaciaExercicio.Service
{
    public class MedicineService
    {
        private readonly MedicineRepository _repository = new MedicineRepository();

        // adicionando remedio com validação basiquinha
        public void AddMedicine(Medicine medicine)
        {
            if (medicine == null)
                throw new Exception("Medicamento não pode ser nulo.");

            if (string.IsNullOrEmpty(medicine.ProductID))
                throw new Exception("ID do medicamento é obrigatório.");

            _repository.AddMedicine(medicine);
        }

        // atualizando remedio com validação simples se existe ou nao
        public Medicine UpdateMedicine(Medicine updatedMedicine)
        {
            if (updatedMedicine == null)
                throw new Exception("Dados do medicamento são inválidos.");

            // aqiu valida se o medicamento existe antes de atualizar
            var existingMedicine = _repository.SearchByID(updatedMedicine.ProductID);
            if (existingMedicine == null)
                throw new Exception("Medicamento não encontrado para atualização.");

            return _repository.UpdateMedicine(updatedMedicine);
        }

        // listar todos de forma simples
        public List<Medicine> ListMedicines()
        {
            return _repository.ListMedicines();
        }

        // procurando por ID com validação simples se existe e nao encontrado
        public Medicine SearchByID(string productId)
        {
            if (string.IsNullOrEmpty(productId))
                throw new Exception("ID não pode ser vazio.");

            var medicine = _repository.SearchByID(productId);

            if (medicine == null)
                throw new Exception($"Medicamento com ID {productId} não encontrado.");

            return medicine;
        }
        // metodo pra fazer a "venda", sem mexer em quantidade de estoque, por enquanto
        // aguardando profe pra recomendar como seguir nao sei fazer sozinho

        /// <summary>
        /// Este método busca um produto, calcula o valor e o peso
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="quantity"></param>
        /// <returns>Retorna um Cupom, contendo o valor total e o peso total</returns>
        public Cupom SellMedicine(string productId, int quantity)
        {
            // buscando o remedioo no repositório pra puxar os preços e os valors
            Medicine medicine = _repository.SearchByID(productId);

            // calculando os totais
            double totalPrice = medicine.Price * quantity;
            double totalWeight = medicine.Weight * quantity;

            Cupom cupom = new Cupom();
            cupom.TotalPrice = totalPrice;
            cupom.TotalWeight = totalWeight;

            return cupom;// retornando os totais
        }


    }
}
