using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmaciaExercicio
{
    //CodeLens para incluir ou tirar as referencias depois
    public class Medicine
    {
        public string Name { get; set; }
        public string Laboratory { get; set; }
        public DateTime Expiration { get; set; }
        public string ProductID { get; set; }
        public double Weight { get; set; }
        public double Price { get; set; }
        public Category Category { get; set; }
    }

    public enum Category
    {
        Adulto,
        Infantil
    }








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


    public class Cupom
    {
        public double TotalPrice { get; set; }
        public double TotalWeight { get; set; }
    }



    internal class Program
    {
        static MedicineService _medicinesService; // pedir ajuda pro profe pra entender quando tem variavel com _
        enum MenuOptions
        {
            NotInformed = 0,
            MedsEntry,
            MedsList,
            MedsExit,
            SearchByName,
            SearchByID,
            UpdateMed,
        }

        static void Main(string[] args)
        {
            _medicinesService = new MedicineService();
            bool execute = true;

            do
            {
                Console.WriteLine("=== SISTEMA FARMÁCIA ===");
                Console.WriteLine("1 - Registrar novo remédio");
                Console.WriteLine("2 - Listar todos os remédios");
                Console.WriteLine("3 - Registrar venda");
                Console.WriteLine("4 - Procurar por nome");
                Console.WriteLine("5 - Procurar por código");
                Console.WriteLine("6 - Atualizar remédio");
                Console.WriteLine("0 - Sair");
                Console.Write("Entre a opçção: ");

                MenuOptions menuOption = (MenuOptions)Convert.ToInt32(Console.ReadLine());

                switch (menuOption)
                {
                    case MenuOptions.MedsEntry:
                        MenuEntryMed();
                        break;
                    case MenuOptions.MedsList:
                        MenuMedsList();
                        break;
                    case MenuOptions.MedsExit: // preciso de ajuda do profe pra criar
                        MenuMedsExit();
                        break;
                    case MenuOptions.SearchByName:
                        MenuSearchByName();
                        break;
                    case MenuOptions.SearchByID:
                        MenuSearchByID();
                        break;
                    case MenuOptions.UpdateMed:
                        UpdateMed();
                        break;
                    default:
                        Console.WriteLine("Opção inválida!");
                        break;
                }


                if (execute)
                {
                    Console.Write("\nPressione qualquer tecla para continuar...");
                    Console.ReadKey();
                    Console.Clear();
                }

            } while (execute);
        }

        static void MenuEntryMed()
        {
            Console.WriteLine("\n== NOVO REMÉDIO ==");
            var medicine = new Medicine();

            Console.Write("Nome: ");
            medicine.Name = Console.ReadLine();

            Console.Write("Laboratório: ");
            medicine.Laboratory = Console.ReadLine();

            Console.Write("ID do Produto: ");
            medicine.ProductID = Console.ReadLine();

            Console.Write("Peso (em gramas): ");
            medicine.Weight = Convert.ToDouble(Console.ReadLine());

            Console.Write("Preço: ");
            medicine.Price = Convert.ToDouble(Console.ReadLine());

            Console.Write("Categoria (1-Adulto, 2-Infantil): ");
            medicine.Category = (Category)(Convert.ToInt32(Console.ReadLine()) - 1);

            Console.Write("Data de Validade (dd/mm/aaaa): ");
            medicine.Expiration = DateTime.Parse(Console.ReadLine());

            _medicinesService.AddMedicine(medicine);
            Console.WriteLine("Remédio cadastrado com sucesso!");
        }

        static void MenuMedsList()
        {
            Console.WriteLine("\n== LISTA DE REMÉDIOS ==");
            var medicines = _medicinesService.ListMedicines();

            if (medicines.Count == 0)
            {
                Console.WriteLine("Nenhum remédio cadastrado.");
                return;
            }

            foreach (var med in medicines)
            {
                Console.WriteLine($"\nID: {med.ProductID}");
                Console.WriteLine($"Nome: {med.Name}");
                Console.WriteLine($"Laboratório: {med.Laboratory}");
                Console.WriteLine($"Preço: R${med.Price:F2}");
                Console.WriteLine($"Validade: {med.Expiration:dd/MM/yyyy}");
                Console.WriteLine($"Categoria: {med.Category}");
            }
        }

        static void MenuSearchByID()
        {
            Console.Write("\nDigite o ID do remédio: ");
            string id = Console.ReadLine();

            var medicine = _medicinesService.SearchByID(id);
            Console.WriteLine("\n== REMÉDIO ENCONTRADO ==");
            Console.WriteLine($"Nome: {medicine.Name}");
            Console.WriteLine($"Preço: R${medicine.Price:F2}");
            Console.WriteLine($"Peso: {medicine.Weight}g");

        }

        static void UpdateMed()
        {
            Console.Write("\nDigite o ID do remédio a atualizar: ");
            string id = Console.ReadLine();


            var existing = _medicinesService.SearchByID(id);
            var updated = new Medicine()
            {
                ProductID = existing.ProductID // mantém o mesmo ID conferindo
            };

            Console.Write($"Novo nome ({existing.Name}): ");
            updated.Name = Console.ReadLine();

            Console.Write($"Novo preço ({existing.Price}): ");
            updated.Price = Convert.ToDouble(Console.ReadLine());


            _medicinesService.UpdateMedicine(updated);
            Console.WriteLine("Remédio atualizado com sucesso!");
        }

        static void MenuSearchByName()
        {
            Console.Write("\nDigite parte do nome: ");
            string namePart = Console.ReadLine();

            var medicines = _medicinesService.ListMedicines()
                .Where(m => m.Name.Contains(namePart, StringComparison.OrdinalIgnoreCase))
                .ToList();

            if (medicines.Count == 0)
            {
                Console.WriteLine("Nenhum remédio encontrado.");
                return;
            }

            Console.WriteLine("\n== RESULTADOS ==");
            foreach (var med in medicines)
            {
                Console.WriteLine($"- {med.Name} (ID: {med.ProductID})");
            }
        }

        public static void MenuMedsExit()
        {
            Console.WriteLine("informe a quantidade");
            int quantidade = int.Parse(Console.ReadLine());
            Console.WriteLine("Informe o produto");
            string idProduto = Console.ReadLine();

            Cupom cupom = _medicinesService.SellMedicine(idProduto, quantidade);

            Console.WriteLine("o valor é " + cupom.TotalPrice);
            Console.WriteLine($"o peso é {cupom.TotalWeight}");
        }

        public int TentarConverter(string valor)
        {
            bool deucerto = int.TryParse(valor, out int resultado);
            if (deucerto) return resultado;

            throw new Exception("");

        }
    }
}
