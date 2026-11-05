using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGFA
{
    internal class Program
    {
        // Lista para guardar todos os veículos da empresa
        static List<Veiculo> frota = new List<Veiculo>();

        static void Main(string[] args)
        {
            // Criar dois carros para testes
            Veiculo v1 = new Veiculo("AA-11-AA", 80);
            v1.ReservaAtingida += TratarAlertaReserva;
            frota.Add(v1);

            Veiculo v2 = new Veiculo("BB-22-BB", 150);
            v2.ReservaAtingida += TratarAlertaReserva;
            frota.Add(v2);

            int opcao = -1;

            do
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("==================================================");
                Console.WriteLine("   SISTEMA DE GESTÃO DE FROTA AUTOMÓVEL - MENU   ");
                Console.WriteLine("==================================================");
                Console.ResetColor();

                Console.WriteLine("1. Listar Frota de Veículos");
                Console.WriteLine("2. Registar Novo Veículo");
                Console.WriteLine("3. Realizar Viagem");
                Console.WriteLine("4. Abastecer Veículo");
                Console.WriteLine("0. Sair");
                Console.WriteLine("--------------------------------------------------");
                Console.Write("Escolha uma opção: ");

                if (int.TryParse(Console.ReadLine(), out opcao))
                {
                    Console.WriteLine();
                    switch (opcao)
                    {
                        case 1:
                            ListarFrota();
                            Pausar();
                            break;
                        case 2:
                            AdicionarVeiculo();
                            Pausar();
                            break;
                        case 3:
                            MenuViagem();
                            Pausar();
                            break;
                        case 4:
                            MenuAbastecer();
                            Pausar();
                            break;
                        case 0:
                            Console.WriteLine("A fechar o programa...");
                            break;
                        default:
                            Console.WriteLine("Opção inválida!");
                            Pausar();
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Por favor introduza um número!");
                    Pausar();
                }

            } while (opcao != 0);
        }

        // Evento: Mostra o aviso em VERMELHO na consola
        private static void TratarAlertaReserva(object sender, EventArgs e)
        {
            Veiculo v = sender as Veiculo;
            if (v != null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\n[ALERTA DE RESERVA] O veículo {v.Matricula} entrou na reserva!");
                Console.WriteLine($"Combustível atual: {v.NivelCombustivel:F2}L (Menos de 10% da capacidade total de {v.CapacidadeDeposito}L)");
                Console.ResetColor();
            }
        }

        static void ListarFrota()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("--- LISTA DE VEÍCULOS ---");
            Console.ResetColor();

            if (frota.Count == 0)
            {
                Console.WriteLine("Nenhum veículo registado.");
                return;
            }

            for (int i = 0; i < frota.Count; i++)
            {
                Veiculo v = frota[i];
                Console.WriteLine($"[{i + 1}] Matrícula: {v.Matricula} | Depósito Max: {v.CapacidadeDeposito}L | Nível Atual: {v.NivelCombustivel:F2}L | Total Km: {v.Quilometragem} km");
            }
        }

        static void AdicionarVeiculo()
        {
            Console.Write("Matrícula (ex: 00-AA-00): ");
            string mat = Console.ReadLine();

            Console.Write("Capacidade do Depósito (Litros): ");
            if (int.TryParse(Console.ReadLine(), out int cap) && cap > 0)
            {
                Veiculo v = new Veiculo(mat, cap);
                v.ReservaAtingida += TratarAlertaReserva;
                frota.Add(v);
                Console.WriteLine("Veículo guardado com sucesso!");
            }
            else
            {
                Console.WriteLine("Capacidade inválida.");
            }
        }

        static void MenuViagem()
        {
            ListarFrota();
            if (frota.Count == 0) return;

            Console.Write("\nEscolha o número do veículo para a viagem: ");
            if (int.TryParse(Console.ReadLine(), out int idx) && idx >= 1 && idx <= frota.Count)
            {
                Console.Write("Distância a percorrer em km: ");
                if (int.TryParse(Console.ReadLine(), out int km))
                {
                    frota[idx - 1].RealizarViagem(km);
                }
            }
        }

        static void MenuAbastecer()
        {
            ListarFrota();
            if (frota.Count == 0) return;

            Console.Write("\nEscolha o número do veículo a abastecer: ");
            if (int.TryParse(Console.ReadLine(), out int idx) && idx >= 1 && idx <= frota.Count)
            {
                Console.Write("Litros a abastecer: ");
                if (double.TryParse(Console.ReadLine(), out double litros))
                {
                    frota[idx - 1].Abastecer(litros);
                }
            }
        }

        static void Pausar()
        {
            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
        }
    }
}
        
    

