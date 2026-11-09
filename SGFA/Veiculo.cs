using System;

namespace SGFA
{
    public class Veiculo
    {
        // Atributos privados
        private int _quilometragem;
        private double _nivelCombustivel;
        private bool _avisoReservaDisparado = false;

        // Evento de reserva
        public event EventHandler ReservaAtingida;

        // Propriedades públicas
        public string Matricula { get; set; }
        public int CapacidadeDeposito { get; set; }

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

                // Cálculo do limite dos 10%
                double limiteReserva = CapacidadeDeposito * 0.10;

                if (_nivelCombustivel >= limiteReserva)
                {
                    _avisoReservaDisparado = false;
                }
                else if (_nivelCombustivel < limiteReserva && !_avisoReservaDisparado)
                {
                    _avisoReservaDisparado = true;
                    if (ReservaAtingida != null)
                    {
                        ReservaAtingida(this, EventArgs.Empty);
                    }
                }
            }
        }

        // Construtor
        public Veiculo(string matricula, int capacidadeDeposito)
        {
            Matricula = matricula;
            CapacidadeDeposito = capacidadeDeposito;
            _quilometragem = 0;
            _nivelCombustivel = capacidadeDeposito;
        }

        // Métodos
        public void Abastecer(double litros)
        {
            if (litros > 0)
            {
                NivelCombustivel = NivelCombustivel + litros;
            }
        }

        public bool RealizarViagem(int kmDistancia)
        {
            if (kmDistancia <= 0)
            {
                return false;
            }

            double combustivelNecessario = kmDistancia * 0.07;

            if (_nivelCombustivel >= combustivelNecessario)
            {
                Quilometragem = Quilometragem + kmDistancia;
                NivelCombustivel = NivelCombustivel - combustivelNecessario;
                return true;
            }

            return false;
        }
    }
}

