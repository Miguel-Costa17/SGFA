using System;
using System.Collections.Generic;

namespace SGFA
{
    internal class Program
    {
        // Lista global para guardar a frota de veículos
        static List<Veiculo> frota = new List<Veiculo>();

        static void Main(string[] args)
        {
            // Criar dois carros de teste iniciais
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
                Console.WriteLine("    SISTEMA DE GESTÃO DE FROTA AUTOMÓVEL - MENU   ");
                Console.WriteLine("==================================================");
                Console.ResetColor();

                Console.WriteLine("1. Listar Frota de Veículos");
                Console.WriteLine("2. Registar Novo Veículo");
                Console.WriteLine("3. Realizar Viagem");
                Console.WriteLine("4. Abastecer Veículo");
                Console.WriteLine("5. Remover Veículo por Matrícula");
                Console.WriteLine("6. Consultar Veículo por Matrícula");
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
                        case 5:
                            RemoverVeiculo();
                            Pausar();
                            break;
                        case 6:
                            ConsultarVeiculo();
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

        // Resposta ao evento de reserva
        private static void TratarAlertaReserva(object sender, EventArgs e)
        {
            Veiculo v = (Veiculo)sender;
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
                Console.WriteLine($"Matrícula: {v.Matricula} | Depósito Max: {v.CapacidadeDeposito}L | Nível Atual: {v.NivelCombustivel:F2}L | Total Km: {v.Quilometragem} km");
            }
        }

        static void AdicionarVeiculo()
        {
            Console.Write("Matrícula (ex: 00-AA-00): ");
            string mat = Console.ReadLine();

            // Validação de duplicados
            for (int i = 0; i < frota.Count; i++)
            {
                if (frota[i].Matricula.ToUpper() == mat.ToUpper())
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Erro: Já existe um veículo com essa matrícula!");
                    Console.ResetColor();
                    return;
                }
            }

            Console.Write("Capacidade do Depósito (Litros): ");
            if (int.TryParse(Console.ReadLine(), out int cap) && cap > 0)
            {
                Veiculo v = new Veiculo(mat, cap);
                v.ReservaAtingida += TratarAlertaReserva;
                frota.Add(v);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Veículo guardado com sucesso!");
                Console.ResetColor();
            }
            else
            {
                Console.WriteLine("Capacidade inválida.");
            }
        }

        static void RemoverVeiculo()
        {
            ListarFrota();
            if (frota.Count == 0) return;

            Console.Write("\nIntroduza a matrícula do veículo a remover: ");
            string mat = Console.ReadLine();

            for (int i = 0; i < frota.Count; i++)
            {
                if (frota[i].Matricula.ToUpper() == mat.ToUpper())
                {
                    frota.RemoveAt(i);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"[SUCESSO] Veículo com a matrícula {mat.ToUpper()} removido da frota!");
                    Console.ResetColor();
                    return;
                }
            }

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Erro: Nenhum veículo encontrado com essa matrícula.");
            Console.ResetColor();
        }

        static void ConsultarVeiculo()
        {
            if (frota.Count == 0)
            {
                Console.WriteLine("Nenhum veículo registado.");
                return;
            }

            Console.Write("Introduza a matrícula a pesquisar: ");
            string mat = Console.ReadLine();

            for (int i = 0; i < frota.Count; i++)
            {
                if (frota[i].Matricula.ToUpper() == mat.ToUpper())
                {
                    Veiculo v = frota[i];
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\n--- VEÍCULO ENCONTRADO ---");
                    Console.ResetColor();
                    Console.WriteLine($"Matrícula: {v.Matricula}");
                    Console.WriteLine($"Capacidade do Depósito: {v.CapacidadeDeposito}L");
                    Console.WriteLine($"Nível de Combustível Atual: {v.NivelCombustivel:F2}L");
                    Console.WriteLine($"Quilometragem Total: {v.Quilometragem} km");
                    return;
                }
            }

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Erro: Nenhum veículo encontrado com essa matrícula.");
            Console.ResetColor();
        }

        // ALTERADO: Procura pela Matrícula em vez do número do índice
        static void MenuViagem()
        {
            ListarFrota();
            if (frota.Count == 0) return;

            Console.Write("\nIntroduza a matrícula do veículo para a viagem: ");
            string mat = Console.ReadLine();

            for (int i = 0; i < frota.Count; i++)
            {
                if (frota[i].Matricula.ToUpper() == mat.ToUpper())
                {
                    Console.Write("Distância a percorrer em km: ");
                    if (int.TryParse(Console.ReadLine(), out int km))
                    {
                        bool sucesso = frota[i].RealizarViagem(km);

                        if (sucesso)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine($"\n[SUCESSO] Viagem realizada! Foram somados {km} km ao total.");
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\n[ERRO] Combustível insuficiente para realizar a viagem!");
                            Console.ResetColor();
                        }
                    }
                    return;
                }
            }

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Erro: Nenhum veículo encontrado com essa matrícula.");
            Console.ResetColor();
        }

        // ALTERADO: Procura pela Matrícula em vez do número do índice
        static void MenuAbastecer()
        {
            ListarFrota();
            if (frota.Count == 0) return;

            Console.Write("\nIntroduza a matrícula do veículo a abastecer: ");
            string mat = Console.ReadLine();

            for (int i = 0; i < frota.Count; i++)
            {
                if (frota[i].Matricula.ToUpper() == mat.ToUpper())
                {
                    Console.Write("Litros a abastecer: ");
                    if (double.TryParse(Console.ReadLine(), out double litros))
                    {
                        frota[i].Abastecer(litros);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"\n[SUCESSO] Abastecimento concluído.");
                        Console.ResetColor();
                    }
                    return;
                }
            }

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Erro: Nenhum veículo encontrado com essa matrícula.");
            Console.ResetColor();
        }

        static void Pausar()
        {
            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
        }
    }
}



