using FarmaciaExercicio.Entities;

namespace meu_segundo_projeto.Entidade
{
    public static class PessoaExtensao
    {
        public static string MedicinesInfo(this Medicine medicine)
        {
            string medicinesInfo = "";

            medicinesInfo += "~Informações dos remédios \n" +
                $"Nome do remédio: {medicine.Name}\n" +
                $"Laboratório: {medicine.Laboratory}\n" +
                $"Código: {medicine.ProductID}\n" +
                $"Peso: {medicine.Weight}\n" +
                $"Preço: {medicine.Price}\n" +
                $"Categoria: {medicine.Category}\n" +
                $"Validade: {medicine.Expiration:dd/MM/yyyy}\n" +

            return medicinesInfo;
        }
    }
}
