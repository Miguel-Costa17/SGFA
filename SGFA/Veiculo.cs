using System;

namespace SGFA
{
    public class Veiculo
    {
        // Atributos privados para proteger os dados (Encapsulamento)
        private int _quilometragem;
        private double _nivelCombustivel;
        private bool _avisoReservaDisparado = false; // Controlo para o aviso não repetir várias vezes

        // Evento que avisa quando o combustível atinge os 10% da reserva
        public event EventHandler ReservaAtingida;

        // Propriedades públicas simples
        public string Matricula { get; set; }
        public int CapacidadeDeposito { get; set; }

        // Propriedade da Quilometragem com validação (não permite voltar a trás nos km)
        public int Quilometragem
        {
            get { return _quilometragem; }
            set
            {
                if (value >= _quilometragem)
                {
                    _quilometragem = value;
                }
            }
        }

        // Propriedade do Combustível com validações e disparo do evento
        public double NivelCombustivel
        {
            get { return _nivelCombustivel; }
            set
            {
                // Impede que o combustível fique negativo
                if (value < 0)
                {
                    _nivelCombustivel = 0;
                }
                // Impede que ultrapasse a capacidade máxima do depósito
                else if (value > CapacidadeDeposito)
                {
                    _nivelCombustivel = CapacidadeDeposito;
                }
                else
                {
                    _nivelCombustivel = value;
                }

                // Cálculo do limite dos 10% da capacidade total
                double limiteReserva = CapacidadeDeposito * 0.10;

                // Se o combustível subir acima dos 10%, prepara o aviso para poder ser disparado no futuro
                if (_nivelCombustivel >= limiteReserva)
                {
                    _avisoReservaDisparado = false;
                }
                // Se descer abaixo dos 10% e o aviso ainda não tiver disparado
                else if (_nivelCombustivel < limiteReserva && !_avisoReservaDisparado)
                {
                    _avisoReservaDisparado = true; // Marca que o aviso já foi dado
                    if (ReservaAtingida != null)
                    {
                        ReservaAtingida(this, EventArgs.Empty); // Dispara o evento
                    }
                }
            }
        }

        // Construtor: inicializa o veículo com a matrícula, capacidade e depósito cheio
        public Veiculo(string matricula, int capacidadeDeposito)
        {
            Matricula = matricula;
            CapacidadeDeposito = capacidadeDeposito;
            _quilometragem = 0;
            _nivelCombustivel = capacidadeDeposito; // Começa com o depósito cheio
        }

        // Método para abastecer o veículo (soma os litros ao nível atual)
        public void Abastecer(double litros)
        {
            if (litros > 0)
            {
                NivelCombustivel = NivelCombustivel + litros;
            }
        }

        // Método para realizar viagem (calcula o consumo a 0.07L por km)
        public bool RealizarViagem(int kmDistancia)
        {
            if (kmDistancia <= 0)
            {
                return false;
            }

            // Consumo fixo de 0.07 litros por cada quilómetro percorrido
            double combustivelNecessario = kmDistancia * 0.07;

            // Só realiza a viagem se houver combustível suficiente no depósito
            if (_nivelCombustivel >= combustivelNecessario)
            {
                Quilometragem = Quilometragem + kmDistancia;
                NivelCombustivel = NivelCombustivel - combustivelNecessario;
                return true;
            }

            return false; // Retorna falso se não tiver combustível suficiente
        }
    }
}

