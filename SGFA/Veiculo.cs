using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGFA
{
    public class Veiculo
    {
        // -------------------------------------------------------------
        // A. CAMPOS PRIVADOS (Guardam a informação em segurança)
        // -------------------------------------------------------------
        private int _quilometragem;
        private double _nivelCombustivel;

        // -------------------------------------------------------------
        // E. EVENTO (Avisa quando o combustível está no fim)
        // -------------------------------------------------------------
        public event EventHandler ReservaAtingida;

        // -------------------------------------------------------------
        // PROPRIEDADES PÚBLICAS
        // -------------------------------------------------------------

        // Propriedades automáticas simples
        public string Matricula { get; set; }
        public int CapacidadeDeposito { get; set; }

        // C. Quilometragem: Validação para nunca diminuir
        public int Quilometragem
        {
            get { return _quilometragem; }
            set
            {
                if (value < _quilometragem)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Erro: A quilometragem não pode diminuir!");
                    Console.ResetColor();
                }
                else
                {
                    _quilometragem = value;
                }
            }
        }

        // C. Nível de Combustível: Validação e Evento de Reserva
        public double NivelCombustivel
        {
            get { return _nivelCombustivel; }
            set
            {
                if (value < 0)
                {
                    _nivelCombustivel = 0;
                }
                else if (value > CapacidadeDeposito)
                {
                    _nivelCombustivel = CapacidadeDeposito;
                }
                else
                {
                    _nivelCombustivel = value;
                }

                // E. Se estiver abaixo de 10%, dispara o evento
                double limiteReserva = CapacidadeDeposito * 0.10;
                if (_nivelCombustivel < limiteReserva)
                {
                    ReservaAtingida?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        // -------------------------------------------------------------
        // B. CONSTRUTOR SEGURO
        // -------------------------------------------------------------
        public Veiculo(string matricula, int capacidadeDeposito)
        {
            Matricula = matricula;
            CapacidadeDeposito = capacidadeDeposito; _quilometragem = 0; // Começa a 0 km
            _nivelCombustivel = capacidadeDeposito; // Começa com depósito cheio
        }

        // -------------------------------------------------------------
        // D. MÉTODOS (Ações do veículo)
        // -------------------------------------------------------------

        // Abastecer o veículo
        public void Abastecer(double litros)
        {
            if (litros <= 0)
            {
                Console.WriteLine("Aviso: Insira uma quantidade superior a 0 litros.");
                return;
            }

            if (_nivelCombustivel + litros > CapacidadeDeposito)
            {
                Console.WriteLine($" Aviso: Ultrapassou o limite! Depósito atestado no máximo ({CapacidadeDeposito}L).");
                NivelCombustivel = CapacidadeDeposito;
            }
            else
            {
                NivelCombustivel += litros;
                Console.WriteLine($"[SUCESSO] Abastecidos {litros}L com sucesso.");
            }
        }

        // Realizar viagem (0.07L por km)
        public bool RealizarViagem(int kmDistancia)
        {
            if (kmDistancia <= 0)
            {
                Console.WriteLine("A distância da viagem tem de ser maior que 0.");
                return false;
            }

            double combustivelNecessario = kmDistancia * 0.07;

            if (_nivelCombustivel >= combustivelNecessario)
            {
                Quilometragem += kmDistancia;
                NivelCombustivel -= combustivelNecessario;
                Console.WriteLine($"[VIAGEM OK] Viagem de {kmDistancia} km feita! Consumo: {combustivelNecessario:F2}L.");
                return true;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"[RECUSADO] Sem combustível suficiente para {kmDistancia} km.");
                Console.WriteLine($"Necessário: {combustivelNecessario:F2}L | Disponível: {_nivelCombustivel:F2}L.");
                Console.ResetColor();
                return false;
            }
        }
    }
}
    

