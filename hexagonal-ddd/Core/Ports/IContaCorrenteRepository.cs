using System;
using System.Collections.Generic;
using hexagonal_ddd.Core.Domain;


namespace hexagonal_ddd.Core.Ports
{
    public interface IContaCorrenteRepository

    {
		public ContaCorrente Load(Guid id);

		public List<ContaCorrente> List();

		public void Save(ContaCorrente conta);

		public void Delete(ContaCorrente conta);
		
		public List<ContaCorrente> ListByCustomer(Guid customerId);
	}
}