using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp10
{

    class Program
    {
        static void Main(string[] args)
        {
            Cliente cliente = new Cliente("Edurado", "84612789091");
            Console.WriteLine(cliente._nome + " " + " " + cliente._cpf);

            Conta conta = new Conta("455455455", 1000, "corrente");
            Console.WriteLine(conta.NumeroConta + " " + conta.Saldo + " " + conta.Tipo);

            conta.Depositar(200);
            Console.WriteLine(conta.NumeroConta + " " + conta.Saldo + " " + conta.Tipo);

            conta.Sacar(100);
            Console.WriteLine(conta.NumeroConta + " " + conta.Saldo + " " + conta.Tipo);

            Conta conta2 = new Conta("4454444444", 0, "poupança");


        }
    }

    public class Cliente
    {
        public List<Conta> contas = new List<Conta>();
        public string _nome { get; private set; }
        public string _cpf { get; private set; }


        public Cliente(string nome, string cpf)
        {
            _nome = nome;
            _cpf = cpf;
        }

        public void AdicionarConta(Conta conta)
        {
            contas.Add(conta);
        }
    }


    public class Conta
    {
        public string NumeroConta;
        public decimal Saldo;
        public string Tipo;

        public Conta(string numeroConta, decimal saldo, string tipo)
        {
            NumeroConta = numeroConta;
            Saldo = saldo;
            Tipo = tipo;
        }

        public void Depositar(decimal deposito)
        {
            Saldo += deposito;

        }

        public void Sacar(decimal saque)
        {

            if (saque > Saldo)
            {
                Console.WriteLine("saque não realizado(valor maior que o saldo)");
                return;
            }

            Saldo -= saque;

        }




    }

    public class Banco
    {
        List<Cliente> clientes = new List<Cliente>();

        public Banco()
        {

        }

        public void CadastrarNovoCliente(string nome, string cpf)
        {
            Cliente cliente = new Cliente(nome, cpf);
            clientes.Add(cliente);

        }

        public void CadastrarNovaConta(string cpf)
        {
            bool encontrei = false;


            foreach (Cliente cliente in clientes)
            {
                if (cliente._cpf == cpf)
                {
                    encontrei = true;


                    if (encontrei)
                    {
                        Console.WriteLine("qual tipo de conte 1 - poupança \n 2 - corrente");
                        int op = Convert.ToInt32(Console.ReadLine());
                        string tipo;

                        if (op == 1)
                        {
                            tipo = "Poupança";
                        }
                        else
                        {
                            tipo = "Corrente";
                        }

                        Conta conta = new Conta("123", 1000, tipo);
                        cliente.AdicionarConta(conta);

                    }
                }

            }

        }

        public void ListarClientesContas()
        {

            foreach (Cliente cliente in clientes)
            {
                Console.WriteLine(cliente);
            }
        }

        public void ConsultarSaldo(string cpf, string numeroConta)
        {


            foreach (Cliente cliente in clientes)
            {
                if (cliente._cpf == cpf)
                {
                    foreach (Conta conta in cliente.contas)
                    {
                        if (conta.NumeroConta == numeroConta)
                        {
                            Console.WriteLine(conta.Saldo);
                            break;
                        }
                    }

                }

            }

        }

        public void RealizarDeposito(string cpf, string numeroConta, decimal valorDeposito)
        {
            //preciso do numero da conta que recebera o valor e somar no saldo teoricamente todos os clientes teram numeros de contas diferentes
            foreach (Cliente cliente in clientes)
            {
                if (cliente._cpf == cpf)
                {
                    foreach (Conta conta in cliente.contas)
                    {
                        if (conta.NumeroConta == numeroConta)
                        {
                            if (valorDeposito < 0)
                            {
                                Console.WriteLine("valor a depositar não pode ser negativo");
                            }
                            else
                            {
                                conta.Saldo += valorDeposito;
                                break;
                            }
                        }

                    }
                    Console.WriteLine("deposito nao realizado cliente nao encontrado");
                    break;
                }

            }

        }

        public void RealizarSaque(string cpf, string numeroConta, decimal valorSaque)
        {
            foreach (Cliente cliente in clientes)
            {
                if (cliente._cpf == cpf)
                {
                    foreach (Conta conta in cliente.contas)
                    {
                        if (conta.NumeroConta == numeroConta)
                        {
                            conta.Saldo -= valorSaque;
                            break;
                        }
                    }
                    Console.WriteLine("cliente nao encontrado");
                    break;
                }
            }


        }

        public void RealizarTransferencia(string cpfTransfere, string cpfRecebe, string numeroContaTransfere, string numeroContaRecebe, decimal valorTransferido)
        {

            foreach (Cliente clienteTransfere in clientes)
            {
                if (clienteTransfere._cpf == cpfTransfere)
                {
                    foreach (Conta contaTransfere in clienteTransfere.contas)
                    {
                        if (contaTransfere.NumeroConta == numeroContaTransfere)
                        {
                            if (contaTransfere.Saldo < valorTransferido)
                            {
                                Console.WriteLine("saldo indisponivel");
                                break;
                            }
                            else
                            {
                                contaTransfere.Saldo -= valorTransferido;
                                break;
                            }
                        }

                    }
                    Console.WriteLine("cliente nao encontrado");
                    break;
                }
            }
            foreach (Cliente clienteRecebe in clientes)
            {
                if (clienteRecebe._cpf == cpfRecebe)
                {
                    foreach (Conta contaRecebe in clienteRecebe.contas)
                    {
                        if (contaRecebe.NumeroConta == numeroContaRecebe)
                        {
                            contaRecebe.Saldo += valorTransferido;
                            break;
                        }
                    }
                    Console.WriteLine("conta nao encontrata");
                    break;
                }
            }


        }

    }

}
