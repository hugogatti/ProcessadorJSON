using ProcessadorJSON.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessadorJSON.Utils
{
    public static class CSVHelper
    {
        //Método para ler o arquivo CSV
        public static List<FuncionarioModel> LerArquivoCSV(string caminhoArquivo)
        {
            var funcionarios = new List<FuncionarioModel>();
            var linhas = File.ReadAllLines(caminhoArquivo);

            foreach (var linha in linhas)
            {
                var colunas = linha.Split(';');

                if (int.TryParse(colunas[0], out int codigo))
                {
                    try
                    {
                        var funcionario = new FuncionarioModel
                        {
                            Codigo = codigo,
                            Nome = colunas[1],
                            ValorHora = decimal.Parse(colunas[2], CultureInfo.InvariantCulture),
                            Data = DateTime.ParseExact(colunas[3], "dd/MM/yyyy", CultureInfo.InvariantCulture),
                            Entrada = TimeSpan.Parse(colunas[4]),
                            Saida = TimeSpan.Parse(colunas[5]),
                            Almoco = TimeSpan.Parse(colunas[6]),
                        };

                        //Calcular o total a receber, horas extras, horas débito, etc.
                        CalcularHoras(funcionario);

                        funcionarios.Add(funcionario);
                    }
                    catch (FormatException ex)
                    {
                        Console.WriteLine($"Erro de formatação ao processar a linha: {linha}. Erro: {ex.Message}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Erro ao processar a linha: {linha}. Erro: {ex.Message}");
                    }
                }
                else
                {
                    Console.WriteLine($"Erro ao converter o código: {colunas[0]} na linha: {linha}");
                }
            }

            return funcionarios;
        }

        private static void CalcularHoras(FuncionarioModel funcionario)
        {
            //Implementa lógica para calcular
            var horasTrabalhadas = (funcionario.Saida - funcionario.Entrada) - funcionario.Almoco;

            if (horasTrabalhadas > TimeSpan.FromHours(8))
            {
                funcionario.HorasExtras = (decimal)(horasTrabalhadas.TotalHours - 8);
            }
            else if (horasTrabalhadas < TimeSpan.FromHours(8))
            {
                funcionario.HorasDebito = (decimal)(8 - horasTrabalhadas.TotalHours);
            }

            funcionario.TotalReceber = (decimal)horasTrabalhadas.TotalHours * funcionario.ValorHora;
            funcionario.DiasTrabalhados = 1;
        }
    }

}
