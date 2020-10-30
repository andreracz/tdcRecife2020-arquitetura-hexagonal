using System;
using System.Collections.Generic;

namespace hexagonal_ddd.Adapter.Repository.Entities
{
    public class ContaCorrenteEntity

    {

		public ContaCorrenteEntity() {
			
		}

		

		public Guid Id {
			get; set;
		}

		public Guid IdCliente {
			get; set;
		}

		public List<TransacaoEntity> Transacoes {
			get; set;
		}
		

    }
}
