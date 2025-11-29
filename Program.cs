using System;
using System.Collections.Generic;
using System.Linq;

namespace HealthTrackApp
{
    class Program
    {
        class Atividade
        {
            public string Tipo { get; set; }
            public string Data { get; set; }
            public double Valor { get; set; }
        }

        static List<Atividade> registros = new List<Atividade>();
        
        static double metaAgua = 2000;   
        static double metaPassos = 10000; 
        static double metaSono = 8;      

        static void Main(string[] args)
        {
            bool executando = true;

            while (executando)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("========================================");
                Console.WriteLine("    HEALTH TRACK PRO - SYSTEM v2.0      ");
                Console.WriteLine("========================================");
                Console.ResetColor();
                
                Console.WriteLine("1. Adicionar Registro");
                Console.WriteLine("2. Histórico ");
                Console.WriteLine("3. Dashboard (Estatísticas)");
                Console.WriteLine("4. Configurar Metas Pessoais");
                Console.WriteLine("5. Sair");
                Console.WriteLine("----------------------------------------");
                Console.Write("Digite sua opção: ");

                string opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1": AdicionarRegistro(); break;
                    case "2": ListarRegistros(); break;
                    case "3": ExibirDashboard(); break;
                    case "4": ConfigurarMetas(); break; 
                    case "5":
                        executando = false;
                        MensagemSucesso("Encerrando o sistema... Até logo!");
                        break;
                    default:
                        MensagemErro("Opção inválida!");
                        break;
                }
            }
        }

        static void ConfigurarMetas()
        {
            Cabecalho("CONFIGURAÇÃO DE METAS");
            
            Console.WriteLine($"Meta atual de ÁGUA:   {metaAgua} ml");
            Console.WriteLine($"Meta atual de PASSOS: {metaPassos}");
            Console.WriteLine($"Meta atual de SONO:   {metaSono} horas");
            Console.WriteLine("-----------------------------------");
            
            Console.Write("Deseja alterar as metas? (S/N): ");
            string resp = Console.ReadLine().ToUpper();

            if (resp == "S")
            {
                Console.WriteLine("\n(Pressione ENTER vazio para manter o valor atual)\n");

                metaAgua = LerDoubleOpcional("Nova meta de ÁGUA (ml): ", metaAgua);
                metaPassos = LerDoubleOpcional("Nova meta de PASSOS: ", metaPassos);
                metaSono = LerDoubleOpcional("Nova meta de SONO (horas): ", metaSono);

                MensagemSucesso("Metas atualizadas com sucesso!");
            }
        }

        static void AdicionarRegistro()
        {
            Cabecalho("NOVO REGISTRO");

            Console.Write("Tipo (Água, Sono, Treino, Passos): ");
            string tipo = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(tipo)) tipo = "Geral";

            Console.Write($"Data [Enter para {DateTime.Now:dd/MM/yyyy}]: ");
            string data = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(data)) data = DateTime.Now.ToString("dd/MM/yyyy");

            double valor = LerDouble("Valor (litros, horas, passos): ");

            registros.Add(new Atividade { Tipo = tipo, Data = data, Valor = valor });
            MensagemSucesso("Registro adicionado com sucesso!");
        }

        static void ListarRegistros()
        {
            Cabecalho("HISTÓRICO DE ATIVIDADES");

            if (registros.Count == 0)
            {
                Console.WriteLine("Nenhum registro encontrado nesta sessão.");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine($"{"DATA",-12} | {"TIPO",-15} | {"VALOR",-10}");
                Console.WriteLine(new string('-', 45));
                Console.ResetColor();

                foreach (var item in registros)
                {
                    Console.WriteLine($"{item.Data,-12} | {item.Tipo,-15} | {item.Valor,-10}");
                }
            }
            Pausar();
        }

        static void ExibirDashboard()
        {
            Cabecalho("DASHBOARD E METAS");

            if (registros.Count == 0)
            {
                Console.WriteLine("Sem dados para análise.");
            }
            else
            {
                var grupos = registros.GroupBy(x => x.Tipo.ToUpper());

                foreach (var grupo in grupos)
                {
                    double total = grupo.Sum(x => x.Valor);
                    double media = grupo.Average(x => x.Valor);
                    
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($" ATIVIDADE: {grupo.Key}");
                    Console.ResetColor();
                    Console.WriteLine($"   Total Acumulado: {total}");
                    Console.WriteLine($"   Média Diária:    {media:F1}");

                    if (grupo.Key.Contains("ÁGUA"))
                    {
                        ExibirBarraDeProgresso(total, metaAgua, "ml");
                    }
                    else if (grupo.Key.Contains("PASSOS"))
                    {
                        ExibirBarraDeProgresso(total, metaPassos, "passos");
                    }
                    else if (grupo.Key.Contains("SONO"))
                    {
                        ExibirBarraDeProgresso(total, metaSono, "horas");
                    }

                    Console.WriteLine("----------------------------------------");
                }
            }
            Pausar();
        }


        static void ExibirBarraDeProgresso(double atual, double meta, string unidade)
        {
            double pct = (atual / meta) * 100;
            Console.Write($"   Meta ({meta} {unidade}):   ");
            
            if (pct >= 100)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"{pct:F0}% - META BATIDA! ");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"{pct:F0}% - Falta pouco!");
            }
            Console.ResetColor();
        }

        static void Cabecalho(string titulo)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"=== {titulo} ===");
            Console.ResetColor();
            Console.WriteLine();
        }

        static double LerDouble(string mensagem)
        {
            while (true)
            {
                Console.Write(mensagem);
                if (double.TryParse(Console.ReadLine(), out double valor) && valor >= 0)
                    return valor;
                MensagemErro("Valor inválido! Digite um número positivo.");
            }
        }
        
        static double LerDoubleOpcional(string mensagem, double valorAtual)
        {
            Console.Write(mensagem);
            string input = Console.ReadLine();
            
            if (string.IsNullOrWhiteSpace(input)) return valorAtual; 
            
            if (double.TryParse(input, out double novoValor) && novoValor >= 0)
                return novoValor;
            
            Console.WriteLine("Valor invalido! Mantendo meta anterior.");
            return valorAtual;
        }

        static void MensagemSucesso(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n{msg}");
            Console.ResetColor();
            if (!msg.Contains("Encerrando")) Pausar();
        }

        static void MensagemErro(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n{msg}");
            Console.ResetColor();
            if(!msg.Contains("Valor")) Pausar();
        }

        static void Pausar()
        {
            Console.WriteLine("\nPressione qualquer tecla...");
            Console.ReadKey();
        }
    }
}