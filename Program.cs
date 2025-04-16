using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FarmaciaExercicio.Service;
using FarmaciaExercicio.Entities;
using FarmaciaExercicio.Enum;
using System.Globalization;

namespace FarmaciaExercicio
{

    internal class Program
    {
        static MedicineService _medicinesService;
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

                MenuOptions menuOption = ReadMenuOption();

                switch (menuOption)
                {
                    case MenuOptions.MedsEntry:
                        MenuEntryMed();
                        break;
                    case MenuOptions.MedsList:
                        MenuMedsList();
                        break;
                    case MenuOptions.MedsExit:
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



                if (execute && menuOption != MenuOptions.NotInformed)
                {
                    Console.Write("\nPressione qualquer tecla para continuar...");
                    Console.ReadKey();
                    Console.Clear();
                }

            } while (execute);
        }

        private static MenuOptions ReadMenuOption()
        {
            while (true)
            {
                Console.Write("Entre a opção: ");
                string input = Console.ReadLine();

                // Verifica se é número
                if (!int.TryParse(input, out int opcao))
                {
                    Console.WriteLine("Erro: Digite apenas números de 0 a 6!");
                    continue;
                }

                // Verifica se está no range do enum
                if (opcao < 0 || opcao > 6)
                {
                    Console.WriteLine("Erro: Opção deve ser entre 0 e 6!");
                    continue;
                }

                return (MenuOptions)opcao;
            }
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

            int categoryInt;
            while (true)
            {
                Console.Write("Categoria (1-Adulto, 2-Infantil): ");
                if (int.TryParse(Console.ReadLine(), out categoryInt) && (categoryInt == 1 || categoryInt == 2))
                {
                    medicine.Category = (Category)(categoryInt - 1);
                    break;
                }
                Console.WriteLine("Valor inválido! Digite 1 ou 2.");
            }


            Console.Write("Data de Validade (dd/mm/aaaa): ");
            string inputDate = Console.ReadLine();
            DateTime inputDateTemporary;
            while (!DateTime.TryParseExact(
            inputDate,
            "dd/MM/yyyy",
            CultureInfo.InvariantCulture,
            DateTimeStyles.None,
            out inputDateTemporary))
            {
                Console.WriteLine("Formato inválido! Digite novamente (dd/mm/aaaa): ");
                inputDate = Console.ReadLine();
            }
            medicine.Expiration = inputDateTemporary;

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
                Console.WriteLine($"Preço: R${med.Price}");
                Console.WriteLine($"Peso: {med.Weight}g");
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
            Console.WriteLine("\nDigite o ID do remédio a atualizar: ");
            string id = Console.ReadLine();


            var existing = _medicinesService.SearchByID(id);
            var updated = new Medicine()
            {
                ProductID = existing.ProductID // mantém o mesmo ID conferindo
            };

            Console.WriteLine($"NOME do remédio: {existing.Name}");
            Console.WriteLine($"Laboratório: {existing.Laboratory}");

            Console.WriteLine($"Novo PESO ({existing.Weight}): ");
            updated.Weight = Convert.ToDouble(Console.ReadLine());

            Console.WriteLine($"Novo PREÇO ({existing.Price}): ");
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
