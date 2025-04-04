// Lista de Exercícios 02 - Programação Orientada a Objetos
// Exercício 1: Sistema Bancário (Encapsulamento)

using System;
using System.Collections.Generic;

namespace Exercicio01
{
    public enum TipoTransacao { Deposito, Saque, Transferencia }

    public class Transacao
    {
        public DateTime DataHora { get; private set; }
        public TipoTransacao Tipo { get; private set; }
        public decimal Valor { get; private set; }
        public string Descricao { get; private set; }

        public Transacao(TipoTransacao tipo, decimal valor, string descricao)
        {
            DataHora = DateTime.Now;
            Tipo = tipo;
            Valor = valor;
            Descricao = descricao;
        }

        public override string ToString() => $"[{DataHora}] {Tipo} - R$ {Valor:F2} - {Descricao}";
    }

    public class ContaBancaria
    {
        private decimal _saldo;
        private string _numeroConta;
        private string _titular;
        private List<Transacao> _transacoes = new List<Transacao>();

        public string NumeroConta => _numeroConta;
        public string Titular => _titular;
        public decimal Saldo => _saldo;

        public ContaBancaria(string numeroConta, string titular)
        {
            _numeroConta = numeroConta;
            _titular = titular;
            _saldo = 0;
        }

        public void Depositar(decimal valor)
        {
            if (valor <= 0)
                throw new ArgumentException("Valor deve ser positivo.");

            _saldo += valor;
            _transacoes.Add(new Transacao(TipoTransacao.Deposito, valor, "Depósito realizado"));
        }

        public void Sacar(decimal valor)
        {
            if (valor <= 0)
                throw new ArgumentException("Valor deve ser positivo.");

            if (valor > _saldo)
                throw new InvalidOperationException("Saldo insuficiente.");

            _saldo -= valor;
            _transacoes.Add(new Transacao(TipoTransacao.Saque, valor, "Saque realizado"));
        }

        public List<Transacao> VerExtrato() => new List<Transacao>(_transacoes);
    }

    public class Program
    {
        public static void Main()
        {
            ContaBancaria conta = new ContaBancaria("12345-6", "Maria Silva");
            conta.Depositar(1000);
            conta.Sacar(200);

            Console.WriteLine($"Titular: {conta.Titular}");
            Console.WriteLine($"Número da Conta: {conta.NumeroConta}");
            Console.WriteLine($"Saldo Atual: R$ {conta.Saldo:F2}");

            Console.WriteLine("Extrato:");
            foreach (var transacao in conta.VerExtrato())
                Console.WriteLine(transacao);
        }
    }
}
