using System;
using System.Collections.Generic;

namespace hexagonal_ddd.Core.Domain
{
    public class ContaCorrente

    {

		private List<Transacao> Transacoes;
		private int _Saldo;

		public ContaCorrente(Guid idConta, Guid idCliente, List<Transacao> transacoes) {
			this.IdCliente = idCliente;
			this.IdConta = idConta;
			this.Transacoes = transacoes;
			int saldo = 0;
			foreach(Transacao transacao in transacoes) {
				if (transacao.TipoTransacao == TipoTransacao.Credito) {
					saldo = saldo + transacao.Valor;
				} else if (transacao.TipoTransacao == TipoTransacao.Debito) {
					saldo = saldo - transacao.Valor;
				} else {
					throw new Exception("Erro, tipo de transação inválido: " + transacao.TipoTransacao);
				}
			}
			if (saldo < 0) {
				throw new Exception("Erro, saldo não pode ser negativo");
			}

			this._Saldo = saldo;
		}

		public void Depositar(int valor, string descricao) {
			Transacao transacao = new Transacao(Guid.NewGuid(), TipoTransacao.Credito, valor, descricao);
			this.Transacoes.Add(transacao);
			this._Saldo += valor;
		}

		public void Sacar(int valor, string descricao) {
			if (valor > Saldo) {
				throw new Exception("Erro, saldo insuficiente");
			}
			Transacao transacao = new Transacao(Guid.NewGuid(), TipoTransacao.Debito, valor, descricao);
			this.Transacoes.Add(transacao);
			this._Saldo -= valor;
		}

		public void TransferirPara(ContaCorrente destino, int valor, string descricao) {
			this.Sacar(valor, descricao);
			destino.Depositar(valor, descricao);
		}

		public List<Transacao> Extrato {
			get { return new List<Transacao>(this.Transacoes); }
		}

		public int Saldo {
			get { return this._Saldo; }
		}

		public Guid IdCliente {
			get;
		}

		public Guid IdConta {
			get;
		}

    }
}
