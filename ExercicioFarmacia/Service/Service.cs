using meu_primeiro_projeto.Estacionamento.Entidade;
using meu_primeiro_projeto.Estacionamento.Interfaces;
using meu_primeiro_projeto.Estacionamento.Repositorio;

namespace meu_primeiro_projeto.Estacionamento.Servico
{
    public class MedicinesService : IMedicinesService
    {
        private MedicinesService _medicinesService = new MedicinesService();

        public List<Medicine> BuscarHistorico()
        {
            return _veiculoRepositorio.BuscarHistorico();
        }

        public List<Veiculo> BuscarVeiculosEstacionados()
        {
            return _veiculoRepositorio.BuscarVeiculosEstacionados();
        }

        public Guid RegistrarEntrada(Veiculo veiculo)
        {
            veiculo.DataHoraEntrada = DateTime.Now;
            veiculo.Ativo = true;
            return _veiculoRepositorio.RegistrarEntrada(veiculo);
        }

        public Veiculo BuscarVeiculoPelaPlaca(string placa)
        {
            Veiculo veiculo = _veiculoRepositorio.BuscarVeiculoPelaPlaca(placa);

            if (veiculo is not null)
                return veiculo;

            throw new Exception("Veículo não encontrado");
        }

        public Veiculo BuscarVeiculoPeloID(Guid id)
        {
            Veiculo veiculo = _veiculoRepositorio.BuscarVeiculoPeloId(id);

            if (veiculo is not null)
                return veiculo;

            throw new Exception("Veículo não encontrado");
        }

        public void RegistrarSaida(string placa)
        {
            Veiculo veiculo = _veiculoRepositorio.BuscarVeiculoPelaPlaca(placa);
            veiculo.DataHoraSaida = DateTime.Now;
            veiculo.Ativo = false;
        }

        public void RegistrarSaida(Guid id)
        {
            Veiculo veiculo = _veiculoRepositorio.BuscarVeiculoPeloId(id);
            veiculo.DataHoraSaida = DateTime.Now;
            veiculo.Ativo = false;
        }

        public List<Veiculo> BuscarTodos()
        {
            return _veiculoRepositorio.BuscarTodos();
        }
    }
}
