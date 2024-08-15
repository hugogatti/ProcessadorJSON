using ProcessadorJSON.Models;
using ProcessadorJSON.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessadorJSON.Services
{
    public class ProcessamentoService
    {
        public async Task<List<DepartamentoModel>> ProcessarArquivos(string pastaCaminho)
        {
            var arquivos = Directory.GetFiles(pastaCaminho, "*.csv");
            var departamentos = new List<DepartamentoModel>();

            foreach (var arquivo in arquivos)
            {
                // Chama o ProcessadorJSON.Utils.CSVHelper Para ler o arquivo CSV
                var funcionarios = CSVHelper.LerArquivoCSV(arquivo);

                // Processa o arquivo CSV e criar os objetos DepartamentoModel e FuncionarioModel
                var departamento = new DepartamentoModel
                {
                    Departamento = ExtrairNomeDepartamento(arquivo),
                    MesVigencia = ExtrairMesVigencia(arquivo),
                    AnoVigencia = ExtrairAnoVigencia(arquivo),
                    Funcionarios = funcionarios,
                    TotalPagar = CalcularTotalPagar(funcionarios),
                    TotalDescontos = CalcularTotalDescontos(funcionarios),
                    TotalExtras = CalcularTotalExtras(funcionarios)
                };

                departamentos.Add(departamento);
            }

            return departamentos;
        }

        private string ExtrairNomeDepartamento(string caminhoArquivo)
        {
            //Retirar nome do departamento do nome do arquivo
            return Path.GetFileNameWithoutExtension(caminhoArquivo).Split('-')[0];
        }

        private string ExtrairMesVigencia(string caminhoArquivo)
        {
            //Retirar mês de vigência do nome do arquivo
            return Path.GetFileNameWithoutExtension(caminhoArquivo).Split('-')[1];
        }

        private int ExtrairAnoVigencia(string caminhoArquivo)
        {
            //Retirar ano de vigência do nome do arquivo
            return int.Parse(Path.GetFileNameWithoutExtension(caminhoArquivo).Split('-')[2]);
        }

        private decimal CalcularTotalPagar(List<FuncionarioModel> funcionarios)
        {
            //Calcula o total a ser pago para o departamento
            decimal totalPagar = 0;
            foreach (var funcionario in funcionarios)
            {
                totalPagar += funcionario.TotalReceber;
            }
            return totalPagar;
        }

        private decimal CalcularTotalDescontos(List<FuncionarioModel> funcionarios)
        {
            //Calcula o total de descontos para o departamento
            decimal totalDescontos = 0;
            foreach (var funcionario in funcionarios)
            {
                totalDescontos += funcionario.HorasDebito * funcionario.ValorHora;
            }
            return totalDescontos;
        }

        private decimal CalcularTotalExtras(List<FuncionarioModel> funcionarios)
        {
            //Calcula o total de horas extras para o departamento
            decimal totalExtras = 0;
            foreach (var funcionario in funcionarios)
            {
                totalExtras += funcionario.HorasExtras * funcionario.ValorHora;
            }
            return totalExtras;
        }
    }
}
