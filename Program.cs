using System;
using System.Collections.Generic;

namespace CalculadoraConsole
{
    class EntradaHistorico
    {
        public double Numero1 { get; set; }
        public double Numero2 { get; set; }
        public string Operacao { get; set; } = string.Empty;
        public double Resultado { get; set; }

        public override string ToString()
        {
            return $"{Numero1} {Operacao} {Numero2} = {Resultado}";
        }
    }

    class Calculadora
    {
        public double Somar(double a, double b) => a + b;
        public double Subtrair(double a, double b) => a - b;
        public double Multiplicar(double a, double b) => a * b;
        public double Dividir(double a, double b)
        {
            if (b == 0)
                throw new DivideByZeroException("Não é possível dividir por zero.");
            return a / b;
        }

        public double Calcular(double a, double b, string operacao)
        {
            return operacao switch
            {"+" => Somar(a, b),
             "-" => Subtrair(a, b),
             "*" => Multiplicar(a, b),
             "/" => Dividir(a, b),
             _ => throw new InvalidOperationException("Operação inválida.")
            };
        }
    }

    class Program
    {
        static List<EntradaHistorico> historico = new List<EntradaHistorico>();

        static void Main(string[] args)
        {
            var calculadora = new Calculadora();
                Console.WriteLine("╔══════════════════════════════╗");
                Console.WriteLine("║      Calculadora em C#       ║");
                Console.WriteLine("╚══════════════════════════════╝");

                bool continuar = true;

                while (continuar)
            {
                Console.WriteLine("\n---Menu---");
                Console.WriteLine(" 1. Fazer um cáculo");
                Console.WriteLine(" 2. Ver histórico");
                Console.WriteLine(" 3. Limpar histórico");
                Console.WriteLine(" 0. Sair");
                Console.Write("\nEscolha: ");
                
                string opcao = Console.ReadLine() ?? string.Empty;
                
                switch (opcao)
                {
                    case "1":
                        FazerCalculo(calculadora);
                        break;
                    case "2":
                        ExibirHistorico();
                        break;
                    case "3":
                        historico.Clear();
                        Console.WriteLine("Histórico apagado.");
                        break;
                    case "0":
                        continuar = false;
                        Console.WriteLine("Até mais!");
                        break;
                    default:
                        Console.WriteLine("Opção inválida. Tente novamente.");
                        break;
                }
            }
        }

        static void FazerCalculo(Calculadora calculadora)
        {
            double num1 = LerNumero("Digite o primeiro número: ");

            Console.Write("Operação (+, -, *, /): ");
            string operacao = (Console.ReadLine() ?? string.Empty).Trim();
            if (operacao != "+" && operacao != "-" && operacao != "*" && operacao != "/")
            {
                Console.WriteLine($"Operação '{operacao}'inválida.");
                return;
            }

            double num2 = LerNumero("Digite o segundo número: ");
            
            try
            {
                double resultado = calculadora.Calcular(num1, num2, operacao);
                Console.WriteLine($"Resultado: {num1} {operacao} {num2} = {resultado}");

                historico.Add(new EntradaHistorico
                {
                    Numero1 = num1,
                    Numero2 = num2,
                    Operacao = operacao,
                    Resultado = resultado
                });
            }
            catch (DivideByZeroException ex)
            {
                Console.WriteLine($"\n {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n Ocorreu um erro: {ex.Message}");
            }

        }

        static double LerNumero(string mensagem)
        {
            double numero;
            while (true)
            {
                Console.Write(mensagem);
                string entrada = (Console.ReadLine() ?? string.Empty).Trim();
            
                if (double.TryParse(entrada, out numero))
                    return numero;

            Console.WriteLine(" Valor inválido. Digite um número (ex: 3.14).");

            }
        }

        static void ExibirHistorico()
        {
            if (historico.Count == 0)
            {
                Console.WriteLine("\nNenhum cálculo realizado ainda.");
                return;
            }

            Console.WriteLine("$\n-------Histórico ({historico.Count} cáculado(s))-------");
            for (int i = 0; i < historico.Count; i++)
            {
                Console.WriteLine($"  {i + 1}. {historico[i]}");
            }
        }
    }
}