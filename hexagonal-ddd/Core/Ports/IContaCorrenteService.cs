using System;
using hexagonal_ddd.Core.Domain;
using System.Collections.Generic;

namespace hexagonal_ddd.Core.Ports
{
    public interface IContaCorrenteService
    {
	
		public void Depositar(Guid idConta, int valor, string descricao);

		public void Sacar(Guid idConta, int valor, string descricao);

		public void Transferir(Guid idContaOrigem, Guid idContaDestino, int valor, string descricao);

		public List<ContaCorrente> TodasContas();

		public ContaCorrente CriarConta(Guid idCliente);

	}
}