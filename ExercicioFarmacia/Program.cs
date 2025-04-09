using System.Data;
using System.Text;


using meu_primeiro_projeto.Estacionamento.Entidade;
using meu_primeiro_projeto.Estacionamento.Interfaces;
using meu_primeiro_projeto.Estacionamento.Repositorio;
using meu_primeiro_projeto.Estacionamento.Servico;

namespace meu_primeiro_projeto
{
    internal class Program
    {
        static MedicinesService _medicinesService;
        enum MenuOptions
        {
            NotInformed = 0,
            MedsEntry,
            MedsList,
            MedsExit,
            SearchByName,
            SearchByID,
        }

        static void Main(string[] args)
        {
            _medicinesService = new MedicinesService();

            bool execute = true;
            MenuOptions menuOption = MenuOptions.NotInformed;
            do
            {
                Console.WriteLine("Qual ação você deseja realizar?");
                Console.WriteLine("1 para registrar entrada de um novo remédio,");
                Console.WriteLine("2 para exibir a lista completa dos remédios");
                Console.WriteLine("3 para registrar a venda de um remédio");
                Console.WriteLine("4 para procurar pelo nome");
                Console.WriteLine("5 para procurar pelo código");



                Console.WriteLine();
                menuOption = (MenuOptions)Convert.ToInt32(Console.ReadLine());


                switch (menuOption)
                {
                    case MenuOptions.MedsEntry:
                        {
                            MenuRegistrar();
                            break;
                        }
                    case MenuOptions.MedsList:
                        {
                            MenuListarTodos();

                            break;
                        }
                    case MenuOptions.MedsExit:
                        {
                            MenuAtivos();
                            break;
                        }
                    case MenuOptions.SearchByName:
                        {
                            MenuNaoAtivos();
                            break;
                        }
                    case MenuOptions.SearchByID:
                        {
                            MenuBuscarPelaPlaca();
                            break;
                        }
                    
                    default:
                        Console.WriteLine("Opção inválida mané");
                        break;
                }

                Console.WriteLine("Deseja executar outra operação?" +
                    " sim para executar e enter para encerrar!");

                executar = Console.ReadLine().ToLower() == "sim" ? true : false;

                Console.Clear();

            } while (executar);
        }


        public static void MenuBuscarPelaPlaca()
        {
            Console.WriteLine("Informe uma placa para visualizar as informações");
            string placa = Console.ReadLine();
            Veiculo veiculo = _veiculoServico.BuscarVeiculoPelaPlaca(placa);

            Console.WriteLine(veiculo.ApresentarInformacoes());
        }

        public static void MenuBuscarPelaId()
        {
            Console.WriteLine("Informe um id para visualizar as informações");
            Guid id = Guid.Parse(Console.ReadLine());
            Veiculo veiculo = _veiculoServico.BuscarVeiculoPeloID(id);

            Console.WriteLine(veiculo.ApresentarInformacoes());
        }


        public static void MenuAtivos()
        {
            List<Veiculo> veiculos = _veiculoServico.BuscarVeiculosEstacionados();

            foreach (Veiculo veiculo in veiculos)
            {
                Console.WriteLine(veiculo);
            }

        }
        public static void MenuNaoAtivos()
        {
            List<Veiculo> veiculos = _veiculoServico.BuscarHistorico();

            foreach (Veiculo veiculo in veiculos)
            {
                Console.WriteLine(veiculo.ApresentarInformacoes());
            }

        }
        public static void MenuRegistrar()
        {
            Veiculo veiculo = new Veiculo();

            Console.WriteLine("Informe a placa");
            veiculo.Placa = Console.ReadLine();

            Console.WriteLine("Informe a marca");
            veiculo.Marca = Console.ReadLine();

            Console.WriteLine("Informe a cor");
            veiculo.Cor = Console.ReadLine();

            Console.WriteLine("Informe o modelo");
            veiculo.Modelo = Console.ReadLine();

            Guid id = _veiculoServico.RegistrarEntrada(veiculo);

            Console.WriteLine("O Id do veiculo registrado é " + id.ToString());
        }

        public static void MenuRegistrarSaida(string placa)
        {
            _veiculoServico.RegistrarSaida(placa);
        }

        public static void MenuRegistrarSaida(Guid id)
        {
            _veiculoServico.RegistrarSaida(id);

            Console.WriteLine("Veiculo foi alterado com sucesso");
        }

        public static void MenuListarTodos()
        {
            List<Veiculo> veiculos = _veiculoServico.BuscarTodos();

            foreach (Veiculo veiculo in veiculos)
            {
                Console.WriteLine(veiculo.ApresentarInformacoes());
            }
        }
    }
}