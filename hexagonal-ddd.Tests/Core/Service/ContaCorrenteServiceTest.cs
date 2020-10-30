using System;
using Xunit;
using hexagonal_ddd.Core.Domain;
using hexagonal_ddd.Core.Service;
using hexagonal_ddd.Core.Ports;
using System.Collections.Generic;
using Moq;

namespace hexagonal_ddd.Tests.Core.Service {

 	public class ContaCorrenteServiceTest
    {
        [Fact]
        public void DepositoConta_OK()
        {
			Mock<IContaCorrenteRepository> fakeRepo = new Mock<IContaCorrenteRepository>();
			Guid guid = Guid.NewGuid();
			List<Transacao> transacoes = new List<Transacao>();
			ContaCorrente conta = new ContaCorrente(Guid.NewGuid(), Guid.NewGuid(), transacoes);
			fakeRepo.Setup(mock => mock.Load(guid)).Returns(conta);
			
			ContaCorrenteService service = new ContaCorrenteService(fakeRepo.Object);
			
			service.Depositar(guid, 10, "deposito");

			Assert.Equal(10, conta.Saldo);
        }
		[Fact]
		public void DepositoConta_Inexistente()
        {
			Mock<IContaCorrenteRepository> fakeRepo = new Mock<IContaCorrenteRepository>();
			Guid guid = Guid.NewGuid();
			List<Transacao> transacoes = new List<Transacao>();
			ContaCorrente conta = new ContaCorrente(Guid.NewGuid(), Guid.NewGuid(), transacoes);
			fakeRepo.Setup(mock => mock.Load(guid)).Returns(conta);
			
			ContaCorrenteService service = new ContaCorrenteService(fakeRepo.Object);
			
			Exception e = Assert.Throws<Exception>(() => service.Depositar(Guid.NewGuid(), 10, "deposito"));
			Assert.Equal("Conta não encontrada", e.Message);
			
        }
	}
}