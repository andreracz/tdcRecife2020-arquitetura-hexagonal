using System;
using hexagonal_ddd.Core.Ports;
using hexagonal_ddd.Core.Domain;
using System.Collections.Generic;

namespace hexagonal_ddd.Core.Service
{
    public class ContaCorrenteService: IContaCorrenteService
    {
		private IContaCorrenteRepository Repository;

		public ContaCorrenteService(IContaCorrenteRepository repository) {
			this.Repository = repository;
		}

		public void Depositar(Guid idConta, int valor, string descricao) {
			var conta = this.Repository.Load(idConta);
			if (conta == null) {
				throw new Exception("Conta não encontrada");
			}
			conta.Depositar(valor, descricao);
			this.Repository.Save(conta);
		}

		public void Sacar(Guid idConta, int valor, string descricao) {
			var conta = this.Repository.Load(idConta);
			if (conta == null) {
				throw new Exception("Conta não encontrada");
			}
			conta.Sacar(valor, descricao);
			this.Repository.Save(conta);
		}

		public void Transferir(Guid idContaOrigem, Guid idContaDestino, int valor, string descricao) {
			var contaOrigem = this.Repository.Load(idContaOrigem);
			if (contaOrigem == null) {
				throw new Exception("Conta Origem não encontrada");
			}
			var contaDestino = this.Repository.Load(idContaDestino);
			if (contaDestino == null) {
				throw new Exception("Conta Destino não encontrada");
			}
			contaOrigem.TransferirPara(contaDestino, valor, descricao);			
			this.Repository.Save(contaOrigem);
			this.Repository.Save(contaDestino);
		

		}
		
		public List<ContaCorrente> TodasContas() {
			return this.Repository.List();
		}

		public ContaCorrente CriarConta(Guid idCliente) {
			ContaCorrente conta = new ContaCorrente(Guid.NewGuid(), idCliente, new List<Transacao>());
			this.Repository.Save(conta);
			return conta;
		}

	}
}