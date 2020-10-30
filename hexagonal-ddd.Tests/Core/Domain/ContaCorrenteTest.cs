using System;
using Xunit;
using hexagonal_ddd.Core.Domain;
using System.Collections.Generic;

namespace hexagonal_ddd.Tests.Core.Domain
{
    public class ContaCorrenteTest
    {
        [Fact]
        public void SemTransacoes_SaldoZero()
        {
			List<Transacao> transacoes = new List<Transacao>();
			ContaCorrente conta = new ContaCorrente(Guid.NewGuid(), Guid.NewGuid(), transacoes);
			Assert.Equal(0, conta.Saldo);
        }

		[Fact]
        public void TransacaoCredito_SaldoSoma()
        {
			List<Transacao> transacoes = new List<Transacao>();
			transacoes.Add(new Transacao(Guid.NewGuid(),TipoTransacao.Credito, 10, "primeira transacao"));
			transacoes.Add(new Transacao(Guid.NewGuid(),TipoTransacao.Credito, 11, "segunda transacao"));
			ContaCorrente conta = new ContaCorrente(Guid.NewGuid(), Guid.NewGuid(), transacoes);
			Assert.Equal(21, conta.Saldo);
        }

		[Fact]
        public void TransacaoCredito_Debito_OK()
        {
			List<Transacao> transacoes = new List<Transacao>();
			transacoes.Add(new Transacao(Guid.NewGuid(),TipoTransacao.Credito, 10, "primeira transacao"));
			transacoes.Add(new Transacao(Guid.NewGuid(),TipoTransacao.Debito, 9, "segunda transacao"));
			ContaCorrente conta = new ContaCorrente(Guid.NewGuid(), Guid.NewGuid(), transacoes);
			Assert.Equal(1, conta.Saldo);
        }

		[Fact]
        public void TransacaoCredito_Debito_SaldoNegativoErro()
        {
			List<Transacao> transacoes = new List<Transacao>();
			transacoes.Add(new Transacao(Guid.NewGuid(),TipoTransacao.Credito, 10, "primeira transacao"));
			transacoes.Add(new Transacao(Guid.NewGuid(),TipoTransacao.Debito, 11, "segunda transacao"));
			Exception e = Assert.Throws<Exception>(() => new ContaCorrente(Guid.NewGuid(), Guid.NewGuid(), transacoes));
			Assert.Equal("Erro, saldo não pode ser negativo", e.Message);
        }

		[Fact]
        public void Deposito_OK()
        {
			List<Transacao> transacoes = new List<Transacao>();
			transacoes.Add(new Transacao(Guid.NewGuid(),TipoTransacao.Credito, 10, "primeira transacao"));
			ContaCorrente conta = new ContaCorrente(Guid.NewGuid(), Guid.NewGuid(), transacoes);
			Assert.Equal(10, conta.Saldo);
			conta.Depositar(2, "deposito");
			Assert.Equal(12, conta.Saldo);
        }

		[Fact]
        public void Credito_OK()
        {
			List<Transacao> transacoes = new List<Transacao>();
			transacoes.Add(new Transacao(Guid.NewGuid(),TipoTransacao.Credito, 10, "primeira transacao"));
			ContaCorrente conta = new ContaCorrente(Guid.NewGuid(), Guid.NewGuid(), transacoes);
			Assert.Equal(10, conta.Saldo);
			conta.Sacar(2, "saque");
			Assert.Equal(8, conta.Saldo);
        }

		[Fact]
        public void Credito_SemSaldo()
        {
			List<Transacao> transacoes = new List<Transacao>();
			transacoes.Add(new Transacao(Guid.NewGuid(),TipoTransacao.Credito, 10, "primeira transacao"));
			ContaCorrente conta = new ContaCorrente(Guid.NewGuid(), Guid.NewGuid(), transacoes);
			Assert.Equal(10, conta.Saldo);
			Exception e = Assert.Throws<Exception>(() => conta.Sacar(11, "saque"));
			Assert.Equal("Erro, saldo insuficiente", e.Message);
			
        }

		[Fact]
        public void Transferencia_OK()
        {
			List<Transacao> transacoes = new List<Transacao>();
			transacoes.Add(new Transacao(Guid.NewGuid(),TipoTransacao.Credito, 10, "primeira transacao"));
			ContaCorrente contaOrigem = new ContaCorrente(Guid.NewGuid(), Guid.NewGuid(), transacoes);
			Assert.Equal(10, contaOrigem.Saldo);
			ContaCorrente contaDestino = new ContaCorrente(Guid.NewGuid(), Guid.NewGuid(), new List<Transacao>());
			Assert.Equal(0, contaDestino.Saldo);
			contaOrigem.TransferirPara(contaDestino, 2, "transferencia");
			Assert.Equal(8, contaOrigem.Saldo);
			Assert.Equal(2, contaDestino.Saldo);
        }

		[Fact]
        public void Transferencia_Erro()
        {
			List<Transacao> transacoes = new List<Transacao>();
			transacoes.Add(new Transacao(Guid.NewGuid(), TipoTransacao.Credito, 10, "primeira transacao"));
			ContaCorrente contaOrigem = new ContaCorrente(Guid.NewGuid(), Guid.NewGuid(), transacoes);
			Assert.Equal(10, contaOrigem.Saldo);
			ContaCorrente contaDestino = new ContaCorrente(Guid.NewGuid(), Guid.NewGuid(), new List<Transacao>());
			Assert.Equal(0, contaDestino.Saldo);
			Exception e = Assert.Throws<Exception>(() => contaOrigem.TransferirPara(contaDestino, 11, "transferencia"));
			Assert.Equal("Erro, saldo insuficiente", e.Message);
        }
    }
}
